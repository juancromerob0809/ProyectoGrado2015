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
    
    public partial class cctiposervdet
    {
        public cctiposervdet()
        {
            this.ccpeticionserv = new HashSet<ccpeticionserv>();
        }
    
        public int cctiposervdetid { get; set; }
        public int cctiposervid { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string url_ayuda { get; set; }
        public string observacion { get; set; }
        public string categoria { get; set; }
    
        public virtual cctiposerv cctiposerv { get; set; }
        public virtual ICollection<ccpeticionserv> ccpeticionserv { get; set; }
    }
}
