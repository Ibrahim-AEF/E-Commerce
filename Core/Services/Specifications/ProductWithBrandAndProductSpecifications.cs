using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndProductSpecifications:Specifications<Product>
    {
        //Used To Retrive Product By Id:
        public ProductWithBrandAndProductSpecifications(int id):base(product=>product.Id==id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }
        //Used For Get All Products
        public ProductWithBrandAndProductSpecifications():base(null)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }
    }
}
