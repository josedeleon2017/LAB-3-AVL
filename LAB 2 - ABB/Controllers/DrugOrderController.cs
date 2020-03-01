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
                            string price = Convert.ToString(regx.Split(row)[4]);
                            price = price.Substring(1, price.Length-1);
                            var drug = new DrugOrderModel
                            {
                                Id = Convert.ToInt32(regx.Split(row)[0]),
                                DrugName = regx.Split(row)[1],
                                Description = regx.Split(row)[2],
                                Producer = regx.Split(row)[3],
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
    }
}
