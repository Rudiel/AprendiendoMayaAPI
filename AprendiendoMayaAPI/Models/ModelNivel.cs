using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AprendiendoMayaAPI.Models
{
    public class ModelNivel
    {
        public String Nombre { get; set; }
        public Boolean Bloqueado { get; set; }
        public int PuntuacionMaxima { get; set; }
        public int ID_Nivel { get; set; }

        public String Imagen { get; set; }
    }
}