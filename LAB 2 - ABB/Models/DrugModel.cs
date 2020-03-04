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
            Storage.Instance.drugTree.Comparer = NameComparison;
            Storage.Instance.drugTree.Converter = IdConverter;
            Storage.Instance.drugTree.GetValue = GetValueString;
            Storage.Instance.drugTree.GetStock = GetStock;
            Storage.Instance.drugTree.Insert(drug);
        }

        //SEARCH TREE               
        public static int Search(string drugName)
        {
            DrugModel drugToSearch = new DrugModel();
            drugToSearch.Name = drugName;

            Storage.Instance.drugTree.Comparer = NameComparison;
            Storage.Instance.drugTree.Converter = IdConverter;
            return Storage.Instance.drugTree.Find(drugToSearch);
        }

        public static string GetPreorder()
        {
            string result = Storage.Instance.drugTree.Preorder();
            return result;
        }

        public static string GetPostorder()
        {
            string result = Storage.Instance.drugTree.Postorder();
            return result;
        }

        public static string GetInorder()
        {
            string result = Storage.Instance.drugTree.Inorder();
            return result;
        }

        //DELEGATES
        public static Comparison<DrugModel> NameComparison = delegate (DrugModel drug1, DrugModel drug2)
        {
            return drug1.Name.CompareTo(drug2.Name);
        };

        public static Converter<DrugModel,Int32> IdConverter = delegate (DrugModel drug) {
            return drug.Id;
        };

        public static Func<DrugModel, string> GetValueString = delegate (DrugModel drug)
        {
            string value = drug.Id + "|" + drug.Name;
            return value;
        };

        public static Func<DrugModel, Int32> GetStock = delegate (DrugModel drug)
        {
            DrugOrderModel drugCheck = new DrugOrderModel();
            int drugPosition = DrugModel.Search(drug.Name) - 1;
            drugCheck = Storage.Instance.drugList.ElementAt(drugPosition);
            return drugCheck.Stock;
        };

    }
}