﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.EF;

//Add The following Namespaces to the project
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob; // Multipart

namespace WebBanHang.Controllers.Backend
{
    public class ProductsController : Controller
    {
        private QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=azurestudentsdiag959;AccountKey=yYLvvu+cHBGK/sBLIvbYkVzE/VBS+z5Tw4K5komw2ybyvFYIV7ilKtYZnfa7rYQwUvTSrmFniX9QNrLyq9y3sQ==;EndpointSuffix=core.windows.net";

        // GET: Products
        public ActionResult Index()
        {
            return View("~/Views/Backend/Products/Index.cshtml", db.products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View("~/Views/Backend/Products/Create.cshtml");
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "id,product_code,product_name,description,standard_cost,list_price,target_level,reorder_level,minimum_reorder_quantity,quantity_per_unit,discontinued,category,image")] products products,
            HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                // Xử lý file: lưu file vào thư mục UploadedFiles/ProductImages
                string _FileName = "";
                // Di chuyển file vào thư mục mong muốn
                if (image != null && image.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(image.FileName);                   // QRCode_NenTangUrl.png
                    string _FileNameExtension = Path.GetExtension(image.FileName);  // .png
                    if ((_FileNameExtension == ".png"
                        || _FileNameExtension == ".jpg"
                        || _FileNameExtension == ".jpeg"
                        ) == false)
                    {
                        return View(String.Format("File có đuôi {0} không được chấp nhận. Vui lòng kiểm tra lại!", _FileNameExtension));
                    }

                    // Upload file lên thư mục Web Server (VPS)
                    string uploadFolderPath = Server.MapPath("~/UploadedFiles/ProductImages");
                    if (Directory.Exists(uploadFolderPath) == false) // Nếu thư mục cần lưu trữ file upload không tồn tại (chưa có) => Tạo mới
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    // Copy file ảnh vào Thư mục UploadFiles/ProductImages trên Server
                    //string _path = Path.Combine(uploadFolderPath, _FileName);
                    //image.SaveAs(_path);

                    // Lưu thông tin đường dẫn vào Database
                    products.image = image.FileName;

                    // Update file lên thư mục Storage Azure
                    // Create Reference to Azure Storage Account
                    String strorageconn = this.StorageConnectionString;
                    CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

                    //Create Reference to Azure Blob
                    CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

                    //The next 2 lines create if not exists a container named "democontainer"
                    CloudBlobContainer container = blobClient.GetContainerReference("democontainer");
                    container.CreateIfNotExists();

                    //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(_FileName);
                    blockBlob.UploadFromStream(image.InputStream);
                }

                // Lưu dữ liệu
                db.products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id); // SELECT * FROM products WHERE id = 601
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product = products;
            return View("~/Views/Backend/Products/Edit.cshtml", products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_code,product_name,description,standard_cost,list_price,target_level,reorder_level,minimum_reorder_quantity,quantity_per_unit,discontinued,category")] products products, string image_oldFile, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string uploadFolderPath = Server.MapPath("~/UploadedFiles/ProductImages");

                if (image == null) // Nếu không cập nhật file (không chọn file)
                {
                    // Giữ nguyên giá trị của tên file trong cột `image`
                    products.image = image_oldFile;
                }
                else // Người ta có chọn file ảnh mới
                {
                    // 1. Xóa file ảnh cũ (để tránh rác)
                    // ~/UploadedFiles/ProductImages/ASP.NET_CodeXuLyDangKy.png -> đường dẫn file ảnh cũ
                    string filePathAnhCu = Path.Combine(uploadFolderPath, (products.image == null ? "" : products.image));
                    if (System.IO.File.Exists(filePathAnhCu))
                    {
                        System.IO.File.Delete(filePathAnhCu);
                    }

                    // 2. Upload file ảnh mới
                    // Xử lý file: lưu file vào thư mục UploadedFiles/ProductImages
                    string _FileName = "";
                    // Di chuyển file vào thư mục mong muốn
                    if (image.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(image.FileName);                   // QRCode_NenTangUrl.png
                        string _FileNameExtension = Path.GetExtension(image.FileName);  // .png
                        if ((_FileNameExtension == ".png"
                            || _FileNameExtension == ".jpg"
                            || _FileNameExtension == ".jpeg"
                            ) == false)
                        {
                            return View(String.Format("File có đuôi {0} không được chấp nhận. Vui lòng kiểm tra lại!", _FileNameExtension));
                        }

                        if (Directory.Exists(uploadFolderPath) == false) // Nếu thư mục cần lưu trữ file upload không tồn tại (chưa có) => Tạo mới
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string _path = Path.Combine(uploadFolderPath, _FileName);
                        image.SaveAs(_path);
                    }
                    // Lưu tên file vào database
                    products.image = _FileName;
                }

                /* UPDATE products
                 * SET product_code = 'P3333',
                 *      product_name = 'DELL 333'
                 * WHERE id = 603
                 */
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product = products;
            return View("~/Views/Backend/Products/Delete.cshtml", products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            products products = db.products.Find(id);
            db.products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
