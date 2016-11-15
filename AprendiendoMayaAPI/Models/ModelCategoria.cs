using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AprendiendoMayaAPI.Models
{
    public class ModelCategoria
    {
        public int ID_Categoria { get; set; }
        public String Nombre { get; set; }
        public String NombreIngles { get; set; }
        public String Imagen { get; set; }
        public long ID_Usuario { get; set; }

        public Boolean Bloqueado { get; set; }

        public int Puntuacion { get; set; }

    }
}