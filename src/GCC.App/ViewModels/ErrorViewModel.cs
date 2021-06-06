using System;

namespace GCC.App.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Mensagem { get; set; }
        public int ErroCode { get; set; }
        public string Titulo { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
