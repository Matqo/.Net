using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyWeb.Models
{
    // an azure storage account
    public class AzureStorage
    {
        const double GeoCostPerGBLevel1 = 0.125;               // up to 1 TB, geographically redundant
        const double GeoCostPerGBLevel2 = 0.11;                 // for excess of I TB, geographically redundant
        const double LocalCostPerGBLevel1 = 0.093;              // up to 1 TB, locally redundant
        const double LocalCostPerGBLevel2 = 0.083;              // for excess of I TB, locally redundant

        // instance size descriptions for a cloud service
        public static String[] RedundancyDescriptions
        {
            get
            {
                return new String[] { "Geographical", "Local" };
            }
        }


        // GB storage
        [Required(ErrorMessage = "Required field!")]
        [Range(1, Double.MaxValue, ErrorMessage = "Must be > 0")]
        [DisplayName("Average GB usage per month")]
        public double Storage { get; set; }             // GB per month on average

        // redundancy type i.e. geographical or redundant
        [Required(ErrorMessage = "Required field!")]
        public String Redundancy { get; set; }

        // get the cost of storage
        public double Cost
        {
            get
            {
                double cost = 0;
                if (Redundancy == "Geographical")
                {
                    if (Storage > 1000)
                    {
                        cost = (1000 * GeoCostPerGBLevel1) + ((Storage - 1000) * GeoCostPerGBLevel2);
                    }
                    else
                    {
                        cost = Storage * GeoCostPerGBLevel1;
                    }
                }
                else
                {
                    if (Storage > 1000)
                    {
                        cost = (1000 * LocalCostPerGBLevel1) + ((Storage - 1000) * LocalCostPerGBLevel2);
                    }
                    else
                    {
                        cost = Storage * LocalCostPerGBLevel1;
                    }
                }
                return cost;
            }
        }

    }
}