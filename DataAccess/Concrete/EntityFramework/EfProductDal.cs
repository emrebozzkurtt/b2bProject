﻿using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, DBContext>, IProductDal
    {
        public List<ProductDetail> GetProductDetail(int id)
        {
            using (DBContext db = new DBContext())
            {
                var result = from p in db.Products
                             where p.ProductId == id
                             join c in db.Categories on p.ProductCategoryId equals c.Id
                             join s in db.Suppliers on p.ProductSupplierId equals s.SupplierId

                             select new ProductDetail
                             {
                                 ProductName = p.ProductName,
                                 ProductPrice = p.ProductPrice,
                                 SupplierName = s.SupplierName,
                                 CategoryName = c.Name,
                                 ProductCountry = p.ProductCountry,
                                 ProductDescription = p.ProductDescription,
                                 ProductionDate = p.ProductionDate,
                                 ProductSupplierId = s.SupplierId,
                                 ProductInStock = p.ProductInStock,
                                 ProductCategoryId = c.Id,
                                 ProductImage = p.ProductImage,
                                 ProductSalesAmount = p.ProductSalesAmount
                             };
                return result.ToList();
            }
        }
    }
}
