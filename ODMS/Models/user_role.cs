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
    
    public partial class user_role
    {
        public int user_role_id { get; set; }
        public string user_role_name { get; set; }
        public string user_role_code { get; set; }
        public Nullable<int> user_role_status { get; set; }
        public Nullable<int> isOnlineLogin { get; set; }
        public Nullable<int> isReportView { get; set; }
        public Nullable<int> isCreate { get; set; }
        public Nullable<int> isEdit { get; set; }
        public Nullable<int> isDelete { get; set; }
    }
}