using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasBatchUploadCheck
{
    /// <summary>
    /// Check
    /// </summary>
    public class CheckItemStatus
    {
        public string ItemNumber{get;set;}
        public string id{get;set;}
        public bool bln_Check { get; set; }

        public bool bln_ProductType { get; set; }
        public bool bln_ProductCategory { get; set; }
        public bool bln_SubCategory { get; set; }

        public bool bln_GarmentType { get; set; }
        public bool bln_Collection { get; set; }
        public bool bln_Series { get; set; }
        public bool bln_Gender { get; set; }

        public bool bln_Cuff { get; set; }
        public bool bln_Making { get; set; }
        public bool bln_Fit { get; set; }
        public bool bln_Pocket { get; set; }


        public bool bln_Collar { get; set; }
        public bool bln_Placket { get; set; }
        public bool bln_Sleeve { get; set; }
        public bool bln_Styling { get; set; }

        public bool bln_Washing { get;set; }
        
        public CheckItemStatus()
        {
            this.ItemNumber = string.Empty;
            this.id = string.Empty;
            this.bln_Check = true;

            this.bln_ProductType = true;
            this.bln_ProductCategory = true;
            this.bln_SubCategory = true;

            this.bln_GarmentType = true;
            this.bln_Collection = true;
            this.bln_Series = true;
            this.bln_Gender = true;

            this.bln_Cuff = true;
            this.bln_Making = true;
            this.bln_Fit = true;
            this.bln_Pocket = true;

            this.bln_Collar = true;
            this.bln_Placket = true;
            this.bln_Sleeve = true;
            this.bln_Styling = true;

            this.bln_Washing = true;
        }
    }
}
