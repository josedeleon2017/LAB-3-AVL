using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LAB_2___ABB.Helpers;

namespace LAB_2___ABB.Models
{
    public class DrugOrderModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Nit { get; set; }

        public int Id { get; set; }
        public string DrugName { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        //INSERT DRUGS ON LIST
        public static void Add(DrugOrderModel drug)
        {
            Storage.Instance.drugList.Add(drug);
        }

    }
}