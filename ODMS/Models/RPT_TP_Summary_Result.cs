//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ODMS.Models
{
    using System;
    
    public partial class RPT_TP_Summary_Result
    {
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string REGION_Name { get; set; }
        public string AREA_Name { get; set; }
        public string CEAREA_Name { get; set; }
        public string DB_Name { get; set; }
        public string DBCode { get; set; }
        public string OfficeAddress { get; set; }
        public string cluster { get; set; }
        public int db_id { get; set; }
        public int Promo_id { get; set; }
        public string NAME { get; set; }
        public string SKUList { get; set; }
        public string FreeSKU { get; set; }
        public double quantity_ordered { get; set; }
        public double quantity_delivered { get; set; }
        public double Free_quantity_ordered { get; set; }
        public double Free_Amount_ordered { get; set; }
        public double Free_quantity_delivered { get; set; }
        public double Free_Amount_delivered { get; set; }
    }
}
