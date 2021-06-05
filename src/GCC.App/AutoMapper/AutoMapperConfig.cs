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
            CreateMap<JornadaDeTrabalho, JornadaDeTrabalhoViewModel>().ReverseMap();
            CreateMap<Medico, MedicoViewModel>().ReverseMap();
            CreateMap<Paciente, PacienteViewModel>().ReverseMap();
            CreateMap<Exame, ExameViewModel>().ReverseMap();
        }
    }
}
