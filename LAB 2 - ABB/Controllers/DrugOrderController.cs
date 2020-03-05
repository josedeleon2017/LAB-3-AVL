using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAB_2___ABB.Models;
using System.IO;
using LAB_2___ABB.Helpers;
using System.Text.RegularExpressions;

namespace LAB_2___ABB.Controllers
{
    public class DrugOrderController : Controller
    {
        // GET: DrugOrder
        public ActionResult Index()
        {
            Storage.Instance.drugOrderList.Clear();
            return View(Storage.Instance.drugOrderList);
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                var drugName = collection["search"];

                Storage.Instance.drugOrderList.Clear();

                int drugPosition = DrugModel.Search(drugName) - 1;
                DrugOrderModel drugFound = Storage.Instance.drugList.ElementAt(drugPosition);
                Storage.Instance.drugOrderList.Add(drugFound);
                return View(Storage.Instance.drugOrderList);
            }
            catch
            {
                return View("ErrorMessage");
            }
        }

        // GET: DrugOrder
        public ActionResult SupplyStock()
        {
            Random random = new Random();
            for (int i=0; i<Storage.Instance.drugList.Count; i++)
            {               
                if (Storage.Instance.drugList.ElementAt(i).Stock == 0)
                {
                    int number = random.Next(1, 15);
                    Storage.Instance.drugList.ElementAt(i).Stock = number;

                    var drugPreviouslyDeleted = new DrugModel
                    {
                        Id = Storage.Instance.drugList.ElementAt(i).Id,
                        Name = Storage.Instance.drugList.ElementAt(i).DrugName,
                    };
                    DrugModel.Add(drugPreviouslyDeleted);
                }
            }      
            return View(Storage.Instance.drugList);
        }

        // GET: DrugOrder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DrugOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DrugOrder/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DrugOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DrugOrder/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DrugOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DrugOrder/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //CSV reader on Tree
        public ActionResult CSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CSV(HttpPostedFileBase postedfile)
        {
            string FilePath;
            if (postedfile != null)
            {
                string Path = Server.MapPath("~/Data/");
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                FilePath = Path + System.IO.Path.GetFileName(postedfile.FileName);
                postedfile.SaveAs(FilePath);
                string csvData = System.IO.File.ReadAllText(FilePath);
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        try
                        {
                            Regex regx = new Regex("," + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                            string[] line = regx.Split(row);  
                            
                            string price = Convert.ToString(regx.Split(row)[4]);
                            price = price.Substring(1, price.Length-1);

                            var drug = new DrugOrderModel
                            {
                                Id = Convert.ToInt32(regx.Split(row)[0]),
                                DrugName = line[1],
                                Description = line[2],
                                Producer = line[3],
                                Price = Convert.ToDouble(price),
                                Stock = Convert.ToInt32(regx.Split(row)[5]),
                            };
                            //SAVE MEDICINE ON THE LIST
                            Storage.Instance.drugList.Add(drug);                                                    
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        // GET: DrugOrder/Add/5
        public ActionResult Add(int id)
        {
            DrugOrderModel drugToAdd = Storage.Instance.drugList.ElementAt(id-1);

            return View(drugToAdd);
        }

        //GET: DrugOrder/Add/5
        [HttpPost]
        public ActionResult Add(int id , FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                int ElementsToDiscount = int.Parse(collection["add"]);

                if(ElementsToDiscount <= Storage.Instance.drugList.ElementAt(id-1).Stock)
                {
                    
                    int updateStock = Storage.Instance.drugList.ElementAt(id - 1).Stock - ElementsToDiscount;
                    Storage.Instance.drugList.ElementAt(id - 1).Stock = updateStock;

                    var drugExpended = new DrugOrderModel
                    {
                        Name = collection["name"],
                        Address = collection["address"],
                        Nit = collection["nit"],

                        Id = Storage.Instance.drugList.ElementAt(id - 1).Id,
                        DrugName = Storage.Instance.drugList.ElementAt(id - 1).DrugName,
                        Description = Storage.Instance.drugList.ElementAt(id - 1).Description,
                        Producer = Storage.Instance.drugList.ElementAt(id - 1).Producer,
                        Price = Storage.Instance.drugList.ElementAt(id - 1).Price,
                        Stock = ElementsToDiscount,
                    };

                    Storage.Instance.drugCartList.Add(drugExpended);
                   // Storage.Instance.drugTree.NoStockCheck();
                    //TESTS
                    //Storage.Instance.drugList.ElementAt(2).Stock=0;

                     DrugModel.Delete();

                    return RedirectToAction("Cart");
                }
                else
                {
                    return View("ErrorMessage");
                }

                //CALL HERE FUNCTION TO DELETE FROM TREE IF NO STOCK LEFT.
               
                
            }
            catch
            {
                return View("ErrorMessage");
            }
        }

        // GET: DrugOrder
        public ActionResult Cart()
        {
            return View(Storage.Instance.drugCartList);
        }
    }
}
