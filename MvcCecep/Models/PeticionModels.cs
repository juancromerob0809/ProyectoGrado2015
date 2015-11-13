using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCecep.Models
{
    public class PeticionVerificacion
    {
        public int ccpeticionid { get; set; }
        public int? ccpeticiondetid { get; set; }
        public int cctiposervid { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public int[] ccpetecionservid { get; set; }
        public int[] cctiposervdetid { get; set; }
        public string[] cctiposerv_codigo { get; set; }
        public string[] cctiposervdet_descrip { get; set; }
        public bool[] estado { get; set; }
    }

    public class PeticionVerf_Aux
    {
        public int cctiposervdetid { get; set; }
        public string cctiposerv_codigo { get; set; }
        public string cctiposervdet_descrip { get; set; }
        public bool estado { get; set; }
    }

    public class GraficoModel {
        public string categoria { get; set; }
        public int Aprobados { get; set; }
    }

    public class GraficoModelVista
    {
        public string[] categoria { get; set; }
        public int[] Aprobados { get; set; }
    }


}