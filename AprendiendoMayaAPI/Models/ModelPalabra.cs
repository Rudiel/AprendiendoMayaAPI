using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AprendiendoMayaAPI.Models
{
    public class ModelPalabra
    {
        public String PalabraEspanol { get; set; }
        public String PalabraMaya { get; set; }
        public String PalabraIngles { get; set; }
        public String Audio { get; set; }
        public String Imagen { get; set; }
        public Boolean PalabraNueva { get; set; }
    }
}