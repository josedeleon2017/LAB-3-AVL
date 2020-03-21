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
            return View(Storage.Instance.drugList.Where(x => x.Stock == 0));
        }

        // GET: DrugOrder/NewStock/5
        public ActionResult NewStock()
        {
            var currentList = Storage.Instance.drugList.Where(x => x.Stock == 0).ToList();


            Random random = new Random();
            for (int i = 0; i < currentList.Count(); i++)
            {
                int number = random.Next(1, 15);
                currentList.ElementAt(i).Stock = number;

                var drugPreviouslyDeleted = new DrugModel
                {
                    Id = currentList.ElementAt(i).Id,
                    Name = currentList.ElementAt(i).DrugName,
                };
                DrugModel.Add(drugPreviouslyDeleted);
            }

            return RedirectToAction("SupplyStock");
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
                        Total = Storage.Instance.drugList.ElementAt(id - 1).Price * ElementsToDiscount,
                    };

                    if (updateStock == 0)
                    {
                        DrugModel.Delete(Storage.Instance.drugList.ElementAt(id-1).DrugName);
                    }

                    Storage.Instance.drugCartList.Add(drugExpended);

                     

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
