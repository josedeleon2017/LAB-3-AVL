﻿using System;
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
            Storage.Instance.drugTree.Root = Storage.Instance.drugTree.InsertAVL(Storage.Instance.drugTree.Root, drug);
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

        //DELETE TREE 
        public static void Delete(string drugName)
        {        
              DrugModel drugToDelete = new DrugModel();
              drugToDelete.Name = drugName;
              Storage.Instance.drugTree.DeleteAVL(Storage.Instance.drugTree.Root, drugToDelete);
        }

        //TRAVERSALS
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

        public static Converter<DrugModel,Int32> IdConverter = delegate (DrugModel drug)
        {
            return drug.Id;
        };

        public static Func<DrugModel, string> GetValueString = delegate (DrugModel drug)
        {
            string value = drug.Id + "|" + drug.Name;

            DrugModel currentDrug = new DrugModel();
            currentDrug.Id = drug.Id;
            currentDrug.Name = drug.Name;
            Storage.Instance.drugStatusList.Add(currentDrug);

            return value;
        };

    }
}