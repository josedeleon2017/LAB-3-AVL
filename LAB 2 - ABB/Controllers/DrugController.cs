using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAB_2___ABB.Models;
using System.IO;
using System.Text.RegularExpressions;
using LAB_2___ABB.Helpers;

namespace LAB_2___ABB.Controllers
{
    public class DrugController : Controller
    {
        // GET: Drug
        public ActionResult Index()
        {
            return View();
        }

        // GET: Drug/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Drug/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drug/Create
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

        // GET: Drug/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Drug/Edit/5
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

        // GET: Drug/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


        // POST: Drug/Delete/5
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

        public ActionResult TreeStatus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pre()
        {
            string result = DrugModel.GetPreorder();
            //Add logic to write in txt here.
            string FilePath;
            string Path = Server.MapPath("~/Traversals/");
            if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
            FilePath = Path + "Preorder.txt";
            System.IO.File.Create(FilePath).Close();
            using (var streamWriter = new StreamWriter(FilePath, false))
                {
                    streamWriter.Write(result);
                }
            return View("TreeStatus");
        }

        

        [HttpPost]
        public ActionResult Post()
        {
            string result = DrugModel.GetPostorder();
            //Add logic to write in txt here.
            string FilePath;
            string Path = Server.MapPath("~/Traversals/");
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            FilePath = Path + "Postorder.txt";
            System.IO.File.Create(FilePath).Close();
            using (var streamWriter = new StreamWriter(FilePath, false))
            {
                streamWriter.Write(result);
            }
            return View("TreeStatus");
        }

        [HttpPost]
        public ActionResult In()
        {
            string result = DrugModel.GetInorder();
            //Add logic to write in txt here.
            string FilePath;
            string Path = Server.MapPath("~/Traversals/");
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            FilePath = Path + "Inorder.txt";
            System.IO.File.Create(FilePath).Close();
            using (var streamWriter = new StreamWriter(FilePath, false))
            {
                streamWriter.Write(result);
            }
            return View("TreeStatus");
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
                            price = price.Substring(1, price.Length - 1);

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

                using (var fileStream = new FileStream(FilePath, FileMode.Open))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var row = streamReader.ReadLine(); 

                            Regex regx = new Regex("," + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                            string[] line = regx.Split(row);

                            var drug = new DrugModel
                            {
                                Id = Convert.ToInt32(line[0]),
                                Name = line[1],
                            };
                            //SAVE MEDICINE ON THE TREE
                            DrugModel.Add(drug);
                        }
                    }
                }

            }
            return View();
        }
    }
}
