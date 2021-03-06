using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GCC.App.Data;
using GCC.App.ViewModels;
using GCC.Business.Interfaces;
using AutoMapper;
using GCC.Business.Modelos;
using System.Reflection;
using GCC.App.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GCC.App.Controllers
{
    [Authorize]
    public class ConsultasController : Controller
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IExameRepository _exameRepository;
        private readonly IMapper _mapper;

        public ConsultasController(IConsultaRepository consultaRepository, IMedicoRepository medicoRepository,
                                   IPacienteRepository pacienteRepository, IExameRepository exameRepository, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
            _exameRepository = exameRepository;
            _mapper = mapper;
        }

        [ClaimsAuthorize("Consulta", "R")]
        public async Task<IActionResult> Index()
        {
            if(UsuarioLogadoEhPaciente())
            {
                var idUsuario = ObterIdUsuarioLogado();
                return RedirectToAction("Paciente", new { id = idUsuario });
            }

            return View(_mapper.Map<IEnumerable<ConsultaViewModel>>(await _consultaRepository.ObtenhaConsultasMedicoPaciente()));
        }

        [ClaimsAuthorize("Consulta", "R")]
        public async Task<IActionResult> Paciente(Guid id)
        {
            var paciente = await _pacienteRepository.ObtenhaPorIdIdentity(id);
            ViewBag.Nome = paciente.Nome;
            return View(_mapper.Map<IEnumerable<ConsultaViewModel>>(await _consultaRepository.ObtenhaConsultasPaciente(id)));
        }

        [ClaimsAuthorize("Consulta", "R")]
        public async Task<IActionResult> Details(Guid id)
        {
            var consultaViewModel = await ObterConsultaPorId(id);
            consultaViewModel = await PopularMedicos(consultaViewModel);
            consultaViewModel = await PopularPacientes(consultaViewModel);
            consultaViewModel = await PopularExames(consultaViewModel);

            if (consultaViewModel == null)
            {
                return NotFound();
            }

            return View(consultaViewModel);
        }

        [ClaimsAuthorize("Consulta", "C")]
        public async Task<IActionResult> Create()
        {
            var consultaViewModel = await PopularMedicos(new ConsultaViewModel());
            consultaViewModel = await PopularPacientes(consultaViewModel);
            consultaViewModel = await PopularExames(consultaViewModel);

            return View(consultaViewModel);
        }

        [ClaimsAuthorize("Consulta", "C")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultaViewModel consultaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(consultaViewModel);
            }

            if(consultaViewModel.Data > DateTime.Now.AddDays(30))
            {
                ModelState.AddModelError(string.Empty, "Consulta pode ser agendeda apenas com 30 dias de antecedência.");
                consultaViewModel = await PopularMedicos(consultaViewModel);
                consultaViewModel = await PopularPacientes(consultaViewModel);
                consultaViewModel = await PopularExames(consultaViewModel);
                return View(consultaViewModel);
            }

            var consulta = _mapper.Map<Consulta>(consultaViewModel);
            if (!await MedicoPossuiDisponibilidade(consulta))
            {
                ModelState.AddModelError(string.Empty, "Médico não possui disponibilidade!");
                consultaViewModel = await PopularMedicos(consultaViewModel);
                consultaViewModel = await PopularPacientes(consultaViewModel);
                consultaViewModel = await PopularExames(consultaViewModel);
                return View(consultaViewModel);
            }

            if(!await PacientePossuiDisponibilidade(consulta))
            {
                ModelState.AddModelError(string.Empty, "Paciente já possui um agendamento no horario!");
                consultaViewModel = await PopularMedicos(consultaViewModel);
                consultaViewModel = await PopularPacientes(consultaViewModel);
                consultaViewModel = await PopularExames(consultaViewModel);
                return View(consultaViewModel);
            }

            var colecaoDeExames = new List<Exame>();
            foreach (var exameId in consultaViewModel.ExamesId)
            {
                var exame = await _exameRepository.ObterPorIdTracking(exameId);
                colecaoDeExames.Add(exame);
            }

            consulta.Exame = colecaoDeExames;
            await _consultaRepository.Adicionar(consulta);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Consulta", "U")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var consultaViewModel = await ObterConsultaPorId(id);
            consultaViewModel = await PopularMedicos(consultaViewModel);
            consultaViewModel = await PopularExames(consultaViewModel);

            if (consultaViewModel == null)
            {
                return NotFound();
            }

            return View(consultaViewModel);
        }

        [ClaimsAuthorize("Consulta", "U")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ConsultaViewModel consultaViewModel)
        {
            if (id != consultaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(consultaViewModel);
            }

            var consulta = _mapper.Map<Consulta>(consultaViewModel);
            await _consultaRepository.Atualizar(consulta);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Consulta", "D")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var consultaViewModel = await ObterConsultaPorId(id);
            consultaViewModel = await PopularMedicos(consultaViewModel);
            consultaViewModel = await PopularExames(consultaViewModel);

            if (consultaViewModel == null)
            {
                return NotFound();
            }

            return View(consultaViewModel);
        }

        [ClaimsAuthorize("Consulta", "D")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var consultaViewModel = await ObterConsultaPorId(id);

            if (consultaViewModel == null)
            {
                return NotFound();
            }

            var consulta = await _consultaRepository.ObtenhaConsulta(id);

            await _consultaRepository.Remover(consulta);

            return RedirectToAction("Index");
        }

        private async Task<ConsultaViewModel> ObterConsultaPorId(Guid id)
        {
            return _mapper.Map<ConsultaViewModel>(await _consultaRepository.ObtenhaConsulta(id));
        }

        private async Task<ConsultaViewModel> PopularMedicos(ConsultaViewModel consulta)
        {
            consulta.Medicos = _mapper.Map<IEnumerable<MedicoViewModel>>(await _medicoRepository.ObterTodos());
            return consulta;
        }

        private async Task<ConsultaViewModel> PopularPacientes(ConsultaViewModel consulta)
        {
            consulta.Pacientes = _mapper.Map<IEnumerable<PacienteViewModel>>(await _pacienteRepository.ObterTodos());
            return consulta;
        }

        private async Task<ConsultaViewModel> PopularExames(ConsultaViewModel consulta)
        {
            consulta.Exames = _mapper.Map<IEnumerable<ExameViewModel>>(await _exameRepository.ObterTodos());
            return consulta;
        }

        private async Task<bool> MedicoPossuiDisponibilidade(Consulta consulta)
        {
            var consultasMedico = await _consultaRepository.ObtenhaConsultasMedico(consulta.MedicoId);
            var consultasMedicoNoDia = consultasMedico.Where(c => consulta.Data.MesmoDia(c.Data)).ToList();

            if (!ConsultaSeEncaixa(consulta, consultasMedicoNoDia))
            {
                return false;
            }

            var medico = await _medicoRepository.ObtenhaMedico(consulta.MedicoId);
            
            return ConsultaEstaNaJornadaDeTrabalho(consulta, medico.JornadaDeTrabalho); ;
        }

        private bool ConsultaEstaNaJornadaDeTrabalho(Consulta consulta, JornadaDeTrabalho jornadaDeTrabalho)
        {
            var diasDeTrabalho = ObtenhaDiasDeTrabalho(jornadaDeTrabalho);
            
            if(!diasDeTrabalho.Contains(consulta.Data.DayOfWeek))
            {
                return false;
            }

            var inicioConsulta = consulta.Data.TimeOfDay;
            var fimConsulta = inicioConsulta.Add(consulta.Duracao);

            return (inicioConsulta >= jornadaDeTrabalho.HoraInicio.TimeOfDay && fimConsulta <= jornadaDeTrabalho.HoraInicioIntervalo.TimeOfDay ||
                    inicioConsulta >= jornadaDeTrabalho.HoraFimIntervalo.TimeOfDay && fimConsulta <= jornadaDeTrabalho.HoraFim.TimeOfDay) ;
        }

        private List<DayOfWeek> ObtenhaDiasDeTrabalho(JornadaDeTrabalho jornadaDeTrabalho)
        {
            var diasDeTrabalho = new List<DayOfWeek>();

            if (jornadaDeTrabalho.Domingo)
            {
                diasDeTrabalho.Add(DayOfWeek.Sunday);
            }

            if (jornadaDeTrabalho.Segunda)
            {
                diasDeTrabalho.Add(DayOfWeek.Monday);
            }

            if (jornadaDeTrabalho.Terca)
            {
                diasDeTrabalho.Add(DayOfWeek.Tuesday);
            }

            if (jornadaDeTrabalho.Quarta)
            {
                diasDeTrabalho.Add(DayOfWeek.Wednesday);
            }

            if (jornadaDeTrabalho.Quinta)
            {
                diasDeTrabalho.Add(DayOfWeek.Thursday);
            }

            if (jornadaDeTrabalho.Sexta)
            {
                diasDeTrabalho.Add(DayOfWeek.Friday);
            }

            if (jornadaDeTrabalho.Sabado)
            {
                diasDeTrabalho.Add(DayOfWeek.Saturday);
            }

            return diasDeTrabalho;
        }

        private async Task<bool> PacientePossuiDisponibilidade(Consulta consulta)
        {
            var consultasPaciente = await _consultaRepository.ObtenhaConsultasPaciente(consulta.PacienteId);
            var consultasPacienteNoDia = consultasPaciente.Where(c => consulta.Data.MesmoDia(c.Data)).ToList();

            return ConsultaSeEncaixa(consulta, consultasPacienteNoDia);
        }

        private bool ConsultaSeEncaixa(Consulta consulta, List<Consulta> consultasAgendadas)
        {
            var inicioConsulta = consulta.Data.TimeOfDay;
            var fimConsulta = inicioConsulta.Add(consulta.Duracao);

            foreach (var consultaAgendada in consultasAgendadas)
            {
                var inicioConsultaAgendada = consultaAgendada.Data.TimeOfDay;
                var fimConsultaAgendada = inicioConsultaAgendada.Add(consultaAgendada.Duracao);

                if (inicioConsulta > inicioConsultaAgendada && inicioConsulta < fimConsultaAgendada ||
                    fimConsulta > inicioConsultaAgendada && fimConsulta < fimConsultaAgendada ||
                    inicioConsulta < inicioConsultaAgendada && fimConsulta > fimConsultaAgendada)
                {
                    return false;
                }
            }

            return true;
        }

        private bool UsuarioLogadoEhPaciente()
        {
            return Equals("P", ObterClaim("Tipo"));
        }

        private Guid ObterIdUsuarioLogado()
        {
            var idStr = ObterClaim(ClaimTypes.NameIdentifier);
            return Guid.Parse(idStr);
        }

        private string ObterClaim(string type)
        {
            return User?.Claims?.FirstOrDefault(c => c.Type == type)?.Value;
        }
    }
}
