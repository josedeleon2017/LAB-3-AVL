using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LAB_2___ABB.Helpers;

namespace LAB_2___ABB.Models
{
    public class DrugModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //INSERT TREE
        public static void Add(DrugModel drug)
        {
            Storage.Instance.DrugTree.Insert(drug, NameComparison);
        }

        //DELEGATES
        public static Comparison<DrugModel> NameComparison = delegate (DrugModel drug1, DrugModel drug2)
        {
            return drug1.Name.CompareTo(drug2.Name);
        };
    }
}