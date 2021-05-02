using AutoMapper;
using GCC.App.ViewModels;
using GCC.Business.Modelos;

namespace GCC.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Consulta, ConsultaViewModel>().ReverseMap();
            CreateMap<CRM, CRMViewModel>().ReverseMap();
            CreateMap<DiaDeTrabalho, DiaDeTrabalhoViewModel>().ReverseMap();
            CreateMap<Especialidade, EspecialidadeViewModel>().ReverseMap();
            CreateMap<Medico, MedicoViewModel>().ReverseMap();
            CreateMap<Paciente, PacienteViewModel>().ReverseMap();
            CreateMap<Prontuario, ProntuarioViewModel>().ReverseMap();
        }
    }
}
