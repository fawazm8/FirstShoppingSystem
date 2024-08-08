using FirstShopping.Models.Data;
using FirstShopping.Models.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;



namespace FirstShopping.Areas.Admin.Controllers
{

    public class ShopController : Controller
    {
        // GET: Admin/Shop
        public ActionResult Categories()
        {
            List<CategoryVM> model;
            using (Db db = new Db())
            {
                model = db.Category.ToArray().OrderBy(x => x.CategorySorting).Select(x => new CategoryVM(x)).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public string AddNewCategory(string catName, string catEnName)
        {
            string id;
            using (Db db = new Db())
            {
                if (db.Category.Any(x => x.CategoryArName == catName))
                    return "titleToken";
                if (db.Category.Any(x => x.CategoryEnName == catName))
                    return "titleToken";

                CategoryDTO dto = new CategoryDTO
                {
                    CategoryArName = catName,
                    CategoryEnName = catEnName,
                    CategorySorting = 100
                };

                db.Category.Add(dto);
                db.SaveChanges();

                id = dto.Id.ToString();
            }

            return id;
        }

        public string RenameCategory(string newCatName, string newEnCatName, int id)
        {
            using (Db db = new Db())
            {
                if (db.Category.Any(x => (x.CategoryArName == newCatName && x.Id != id) || (x.CategoryEnName == newEnCatName && x.Id != id)))
                {
                    return "titletaken";
                }
                CategoryDTO dto = db.Category.Find(id);
                dto.CategoryArName = newCatName;
                dto.CategoryEnName = newEnCatName;

                db.SaveChanges();
            }
            return "ok";
        }

        [HttpPost]
        public void ReOrderCategory(int[] id)
        {
            using (Db db = new Db())
            {
                int count = 1;
                CategoryDTO dto;
                foreach (var cateId in id)
                {
                    dto = db.Category.Find(cateId);
                    dto.CategorySorting = count;
                    db.SaveChanges();
                    count++;
                }
            }
        }

        public ActionResult DeleteCategory(int id)
        {
            using (Db db = new Db())
            {
                // Get the category
                CategoryDTO dto = db.Category.Find(id);

                // Remove the category
                db.Category.Remove(dto);

                // Save
                db.SaveChanges();
            }

            // Redirect
            return RedirectToAction("Categories");
        }

        public ActionResult AddProduct()
        {
            ProductVM model = new ProductVM();
            using (Db db = new Db())
            {
                model.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductVM model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    model.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");
                    return View(model);
                }
            }

            using (Db db = new Db())
            {
                if (db.product.Any(x => x.ProductArName == model.ProductArName))
                {
                    model.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");
                    ModelState.AddModelError("", "اسم المنتج بالعربي موجود سابقا ");
                    return View(model);
                }
                if (db.product.Any(x => x.ProductEnName == model.ProductEnName))
                {
                    model.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");
                    ModelState.AddModelError("", "اسم المنتج الانجليزي موجود سابقا ");
                    return View(model);
                }
            }

            int id;
            using (Db db = new Db())
            {
                if (file != null && file.ContentLength > 0)
                {
                    string ext = file.ContentType.ToLower();
                    if (ext != "image/jpg" &&
                       ext != "image/jpeg" &&
                       ext != "image/pjpeg" &&
                       ext != "image/gif" &&
                       ext != "image/x-png" &&
                       ext != "image/png")
                    {
                        model.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");
                        ModelState.AddModelError("", "تاكد من امتداد الصورة .. لم يتم تحميل الصورة");
                        return View(model);
                    }

                    ProductDTO productDto = new ProductDTO
                    {
                        ProductArName = model.ProductArName,
                        ProductEnName = model.ProductEnName,
                        Price = model.Price,
                        Description = model.Description,
                        CategoryID = model.CategoryID,
                    };

                    CategoryDTO catDTO = db.Category.FirstOrDefault(x => x.Id == model.CategoryID);
                    productDto.CategoryName = catDTO.CategoryArName;

                    db.product.Add(productDto);
                    db.SaveChanges();

                    id = productDto.id;

                    TempData["SM"] = "تم اضافة المنتج بنجاح";
                }
                else
                {
                    model.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");
                    ModelState.AddModelError("", "يرجى رفع صورة للمنتج");
                    return View(model);
                }
            }

            #region Upload Image

            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));
            var pathString1 = Path.Combine(originalDirectory.ToString(), "Products");
            var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());
            var pathString3 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Thumbs");

            if (!Directory.Exists(pathString1))
                Directory.CreateDirectory(pathString1);
            if (!Directory.Exists(pathString2))
                Directory.CreateDirectory(pathString2);
            if (!Directory.Exists(pathString3))
                Directory.CreateDirectory(pathString3);

            string imageName = file.FileName;
            using (Db db = new Db())
            {
                ProductDTO dto = db.product.Find(id);
                dto.ImageName = imageName;

                db.SaveChanges();
            }

            var path = string.Format("{0}\\{1}", pathString2, imageName);
            var path2 = string.Format("{0}\\{1}", pathString3, imageName);

            file.SaveAs(path);
            WebImage img = new WebImage(file.InputStream);
            img.Resize(200, 200);
            img.Save(path2);

            #endregion

            return RedirectToAction("AddProduct");
        }

        public ActionResult Products(int? CatId, int? page)
        {
            List<ProductVM> listOfProductVM;
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            using (Db db = new Db())
            {
                listOfProductVM = db.product.ToArray().OrderBy(x =>x.ProductSort).Where(x => x.CategoryID == CatId || CatId == null || CatId == 0).Select(x => new ProductVM(x)).ToList();
                var onePageOfProducts = listOfProductVM.ToPagedList(pageNumber, 2); // will only contain 25 products max because of the pageSize
                ViewBag.OnePageOfProducts = onePageOfProducts;
                ViewBag.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");
                ViewBag.SelectedCat = CatId.ToString();
            }
            return View();
        }

        public ActionResult EditProduct(int id)
        {
            ProductVM model;
            using (Db db = new Db())
            {
                ProductDTO dto = db.product.Find(id);
                if (dto == null)
                {
                    return Content("المنتج غير موجود");
                }

                model = new ProductVM(dto);
                model.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");

                string thumbsPath = Server.MapPath("~/Images/Uploads/Products/" + id + "/Thumbs");
                if (!Directory.Exists(thumbsPath))
                {
                    Directory.CreateDirectory(thumbsPath);
                }

                // تسجيل المسار للتحقق منه
                System.Diagnostics.Debug.WriteLine("Thumbs Path: " + thumbsPath);

                // قراءة الصور المصغرة من الدليل
                model.GalleryImages = Directory.EnumerateFiles(thumbsPath)
                    .Select(fn => Path.GetFileName(fn))
                    .ToList();

               // System.Diagnostics.Debug.WriteLine("Number of thumbnails found: " + model.GalleryImages.Count);
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult EditProduct(ProductVM model, HttpPostedFileBase file)
        {
            int id = model.id;
            if (!ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    model.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");
                }
                return View(model);
            }

            using (Db db = new Db())
            {
                model.Categories = new SelectList(db.Category.ToList(), "Id", "CategoryArName");
                model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Products/" + id + "/Thumbs"))
                                              .Select(fn => Path.GetFileName(fn));

                if (db.product.Where(x => x.id != id).Any(x => x.ProductArName == model.ProductArName))
                {
                    ModelState.AddModelError("", "المنتج موجود سابقا!");
                    return View(model);
                }

                ProductDTO dto = db.product.Find(id);
                dto.ProductArName = model.ProductArName;
                dto.ProductEnName = model.ProductEnName;
                dto.Description = model.Description;
                dto.Price = model.Price;
                dto.CategoryID = model.CategoryID;
                dto.ImageName = model.ImageName;

                CategoryDTO catDTO = db.Category.FirstOrDefault(x => x.Id == model.CategoryID);
                dto.CategoryName = catDTO.CategoryArName;

                db.SaveChanges();
            }

            TempData["SM"] = "تم تعديل المنتج بنجاح";

            #region Image Upload
            if (file != null && file.ContentLength > 0)
            {
                var ext = file.ContentType.ToLower();
                if (ext != "image/jpg" &&
                   ext != "image/jpeg" &&
                   ext != "image/pjpeg" &&
                   ext != "image/gif" &&
                   ext != "image/x-png" &&
                   ext != "image/png")
                {
                    ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                    return View(model);
                }

                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));
                var pathString1 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());
                var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Thumbs");

                DirectoryInfo di1 = new DirectoryInfo(pathString1);
                DirectoryInfo di2 = new DirectoryInfo(pathString2);

                foreach (FileInfo file2 in di1.GetFiles())
                    file2.Delete();
                foreach (FileInfo file3 in di2.GetFiles())
                    file3.Delete();

                string imageName = file.FileName;
                using (Db db = new Db())
                {
                    ProductDTO dto = db.product.Find(id);
                    dto.ImageName = imageName;
                    db.SaveChanges();
                }

                var path = string.Format("{0}\\{1}", pathString1, imageName);
                var path2 = string.Format("{0}\\{1}", pathString2, imageName);

                file.SaveAs(path);
                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200);
                img.Save(path2);
            }
            #endregion

            return RedirectToAction("EditProduct");
        }

        [HttpPost]
        public void SaveGalleryImages(int id)
        {
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                if (file != null && file.ContentLength > 0)
                {
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));
                    string pathString1 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString());
                    string pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Thumbs");

                    var path = string.Format("{0}\\{1}", pathString1, file.FileName);
                    var path2 = string.Format("{0}\\{1}", pathString2, file.FileName);

                    file.SaveAs(path);
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(200, 200);
                    img.Save(path2);
                }
            }
        }

        [HttpPost]
        public void ReOrderProduct(int[] id)
        {
            using (Db db = new Db())
            {
                int count = 1;
                ProductDTO dto;
                foreach (var ProID in id)
                {
                    dto = db.product.Find(ProID);
                    dto.ProductSort = count;
                    db.SaveChanges();
                    count++;
                }
                }
        }
    }
}


