using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILogger logger)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var dataBrands = new List<ProductBrand>();
                    using (StreamReader r = new StreamReader(@"../Intrastructure/Data/SeedData/brand.json"))
                    {
                        string json = r.ReadToEnd();
                        dataBrands = JsonConvert.DeserializeObject<List<ProductBrand>>(json);
                        await context.ProductBrands.AddRangeAsync(dataBrands);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.ProductTypes.Any())
                {
                    var dataTypes = new List<ProductType>();
                    using (StreamReader r = new StreamReader(@"../Intrastructure/Data/SeedData/type.json"))
                    {
                        string json = r.ReadToEnd();
                        dataTypes = JsonConvert.DeserializeObject<List<ProductType>>(json);
                        await context.ProductTypes.AddRangeAsync(dataTypes);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.Products.Any())
                {
                    var dataProducts = new List<Product>();
                    using (StreamReader r = new StreamReader(@"../Intrastructure/Data/SeedData/product.json"))
                    {
                        string json = r.ReadToEnd();
                        dataProducts = JsonConvert.DeserializeObject<List<Product>>(json);
                        await context.Products.AddRangeAsync(dataProducts);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
