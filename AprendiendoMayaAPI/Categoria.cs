//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AprendiendoMayaAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categoria()
        {
            this.Niveles = new HashSet<Nivele>();
            this.Puntuaciones = new HashSet<Puntuacione>();
        }
    
        public int ID_Categoria { get; set; }
        public string Nombre { get; set; }
        public string NombreIngles { get; set; }
        public string Imagen { get; set; }
        public Nullable<bool> Bloqueado { get; set; }
        public Nullable<long> ID_Usuario { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Nivele> Niveles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Puntuacione> Puntuaciones { get; set; }
    }
}
