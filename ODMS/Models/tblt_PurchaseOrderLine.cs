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
    using System.Collections.Generic;
    
    public partial class tblt_PurchaseOrderLine
    {
        public int id { get; set; }
        public int POId { get; set; }
        public int sku_id { get; set; }
        public int BatchId { get; set; }
        public double Price { get; set; }
        public int PackSize { get; set; }
        public int ChallanQty { get; set; }
        public int ReciveQty { get; set; }
        public int BundelItem { get; set; }
    
        public virtual tblt_PurchaseOrder tblt_PurchaseOrder { get; set; }
    }
}