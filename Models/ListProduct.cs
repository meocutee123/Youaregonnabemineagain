using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronic_Store.Models
{
    
    public class ListProduct
    {
        ESDatabaseEntities db = null;
        public ListProduct()
        {
            db = new ESDatabaseEntities();
        }
        public List<Product> listNewProduct (int top)
        {
            return db.Products.Where(x=>x.Status == true).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> listProduct()
        {
            return db.Products.Where(x => x.Status == true).ToList();
        }
        public List<Brand> listBrand()
        {
            return db.Brands.Where(x => x.Status == true).ToList();
        }
        public List<Product> listPhone()
        {
            return db.Products.Where(x => x.Status == true && x.CategoryID == 1).ToList();
        }
        public List<Product> listHouseWare()
        {
            return db.Products.Where(x => x.Status == true && x.CategoryID == 3).ToList();
        }
    }
}