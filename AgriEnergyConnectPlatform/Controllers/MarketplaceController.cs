using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriEnergyConnectPlatform.Controllers
{
    public class MarketplaceController : Controller
    {
        private AgriConnectContext db = new AgriConnectContext();

        [Authorize]
        public ActionResult Marketplace(string search, decimal? price, int? rating, string carbon)
        {
            var products = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.ProductName.Contains(search));
            }

            if (price.HasValue)
            {
                products = products.Where(p => p.Price <= price.Value);
            }

            return View(products.ToList());
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddProduct(string productName, string productDescription, string category, decimal productPrice, int productStock, DateTime productionDate, HttpPostedFileBase productImage)
        {
            if (productImage != null && productImage.ContentLength > 0)
            {
                var classLibraryPath = Server.MapPath("~/Data/ProductImages/");

                if (!Directory.Exists(classLibraryPath))
                {
                    Directory.CreateDirectory(classLibraryPath);
                }

                // Save the product image to a folder and get the file path
                var fileName = System.IO.Path.GetFileName(productImage.FileName);
                var path = Path.Combine(classLibraryPath, fileName);
                productImage.SaveAs(path);

                // Create new product
                var product = new Product
                {
                    ProductName = productName,
                    ProductDescription = productDescription,
                    Category = category,
                    Price = productPrice,
                    Stock = productStock,
                    ProductionDate = productionDate,
                    UserId = User.Identity.GetUserId(), // Set UserId
                    ProductImagePath = "/Data/ProductImages/" + fileName, // Set image path
                };

                // Add product to the database
                db.Products.Add(product);
                db.SaveChanges();
            }

            // Redirect to the marketplace page or show a success message
            return RedirectToAction("Marketplace", "Marketplace");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateProduct(int productId, string productName, string productDescription, string category, string price, int stock, DateTime productionDate, HttpPostedFileBase productImage)
        {

            // Parse the price from string to decimal using a specific culture
            
            var parsedPrice = decimal.Parse(price.Replace('.', ','));

            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                product.ProductName = productName;
                product.ProductDescription = productDescription;
                product.Category = category;
                product.Price = parsedPrice;
                product.Stock = stock;
                product.ProductionDate = productionDate;

                if (productImage != null && productImage.ContentLength > 0)
                {
                    var classLibraryPath = Server.MapPath("~/Data/ProductImages/");
                    if (!Directory.Exists(classLibraryPath))
                    {
                        Directory.CreateDirectory(classLibraryPath);
                    }

                    // Save the new product image to a folder and get the file path
                    var fileName = System.IO.Path.GetFileName(productImage.FileName);
                    var path = Path.Combine(classLibraryPath, fileName);
                    productImage.SaveAs(path);

                    // Update the product image path
                    product.ProductImagePath = "/Data/ProductImages/" + fileName;
                }

                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("MyProfile", "Account");
            }

            return View("MyProfile");
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteProduct(int productId)
        {
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("MyProfile", "Account");
        }
    }
}
