using Domain.Contracts;
using Domain.Entities;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;

        public DbInitializer(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                #region For Updating Database
                //Create DataBase If Doesn`t Exist And Applying Any Pending Migrations
                if (_storeContext.Database.GetPendingMigrations().Any())
                {
                    await _storeContext.Database.MigrateAsync();
                }
                #endregion
                #region ProductTypes
                if (!_storeContext.ProductTypes.Any())
                {
                    //Read Types From File As String
                    var TypesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\types.json");
                    //Transform Into C# Objects
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                    //Add To Database & Save Changes
                    if (Types is not null && Types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(Types);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                #endregion
                #region ProductBrands
                if (!_storeContext.ProductBrands.Any())
                {
                    //Read Brands From File As String
                    var BrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\brands.json");
                    //Transform Into C# Objects
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    //Add To Database & Save Changes
                    if (Brands is not null && Brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(Brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                #endregion
                #region Product
                if (!_storeContext.Products.Any())
                {
                    //Read Products From File As String
                    var ProductsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\products.json");
                    //Transform Into C# Objects
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    //Add To Database & Save Changes
                    if (Products is not null && Products.Any())
                    {
                        await _storeContext.Products.AddRangeAsync(Products);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
