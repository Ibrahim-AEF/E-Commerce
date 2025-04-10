using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications;
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

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters)
        {
            var Products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationAsync(new ProductWithBrandAndProductSpecifications(parameters));
            var ProductResult = Mapper.Map<IEnumerable<ProductResultDto>>(Products);
            var TotalCount = ProductResult.Count();
            var Result = new PaginatedResult<ProductResultDto>
                (
                parameters.pageIndex,
                parameters.PageSize,
                TotalCount,
                ProductResult
                );
            return Result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesResult = Mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return TypesResult;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdWithSpecificationAsync(new ProductWithBrandAndProductSpecifications(id));
            var ProductResult = Mapper.Map<ProductResultDto>(Product);
            return ProductResult;
        }
    }
}
