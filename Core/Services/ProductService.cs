using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper Mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var BrandsResult = Mapper.Map<IEnumerable<BrandResultDto>>(Brands);
            return BrandsResult;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var Products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var ProductsResult = Mapper.Map<IEnumerable<ProductResultDto>>(Products);
            return ProductsResult;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesResult = Mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return TypesResult;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var ProductResult = Mapper.Map<ProductResultDto>(Product);
            return ProductResult;
        }
    }
}
