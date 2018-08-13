using System;

namespace ODMS.Models.ViewModel
{
    
        public  class BuyerByDbSummary
        {
            public string RegionName { get; set; }
            public string AreaName { get; set; }
            public string CeareaName { get; set; }
            public string DbName { get; set; }
            public int DbId { get; set; }
            public int TotalOutlet { get; set; }
            public int BuyerOutlet { get; set; }
            public int NonBuyer { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime Enddate { get; set; }
            public string Skuid { get; set; }
        }

   
    
}