//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcCecep
{
    using System;
    using System.Collections.Generic;
    
    public partial class ccempresa
    {
        public ccempresa()
        {
            this.ccpeticion = new HashSet<ccpeticion>();
        }
    
        public int ccempresaid { get; set; }
        public string nit { get; set; }
        public string dv { get; set; }
        public string descripcion { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string contactnombre { get; set; }
        public string contacttelefono { get; set; }
        public string contactcelular { get; set; }
        public string mail1 { get; set; }
        public string mail2 { get; set; }
        public string loggin { get; set; }
        public string contraseña { get; set; }
        public System.DateTime fechafundacion { get; set; }
        public int ccareaid { get; set; }
        public System.DateTime fechacreacion { get; set; }
        public string contactomail { get; set; }
        public bool esadmin { get; set; }
    
        public virtual ccarea ccarea { get; set; }
        public virtual ICollection<ccpeticion> ccpeticion { get; set; }
    }
}