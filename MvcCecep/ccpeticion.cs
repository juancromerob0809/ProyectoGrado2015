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
    
    public partial class ccpeticion
    {
        public ccpeticion()
        {
            this.ccpeticiondet = new HashSet<ccpeticiondet>();
        }
    
        public int ccpeticionid { get; set; }
        public int ccareaid { get; set; }
        public int ccempresaid { get; set; }
        public string descripcion { get; set; }
        public Nullable<bool> activa { get; set; }
        public Nullable<System.DateTime> fechacre { get; set; }
        public Nullable<System.DateTime> fechavenc { get; set; }
        public Nullable<System.DateTime> fechains { get; set; }
    
        public virtual ccarea ccarea { get; set; }
        public virtual ccempresa ccempresa { get; set; }
        public virtual ICollection<ccpeticiondet> ccpeticiondet { get; set; }
    }
}
