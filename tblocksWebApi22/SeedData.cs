
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApi22;
using CoreWebApi22.Models;

namespace CoreWebApi22
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestData(
                services.GetRequiredService<CoreWebApi22.Models.CoreWebApiTestDBContext>(),
                services.GetRequiredService<CoreWebApi22.UserDbContext>(),services.GetRequiredService< UserManager < IdentityUser >>());
        }

        public static async Task AddTestData(
            CoreWebApiTestDBContext context,
            UserDbContext userDbContect, UserManager<IdentityUser> userManager)
        {
            if (context.Products.Any())
            {
                // Already has data
                return;
            }
            

            ProductCategory cat1 = new ProductCategory();
            cat1.ProductCategoryName = "Cell Phones";
           
            
            cat1.Products.Add(new Product
            {
                ProductTitle = "Android Phone V1",
                ProductDescription = "Android Phone V1 Description",
                ProductPrice = 1000,
                ProductImagePath = "",
            });
            cat1.Products.Add(new Product
            {
                ProductTitle = "iPhone Phone V1",
                ProductDescription = "iPhone Phone V1 Description",
                ProductPrice = 2000,
                ProductImagePath = "",
            });



            ProductCategory cat2 = new ProductCategory();
            cat2.ProductCategoryName = "Electronics";
            
            
            cat2.Products.Add(new Product
            {
                ProductTitle = "Samsung TV",
                ProductDescription = "Samsung TV Description",
                ProductPrice = 10000,
                ProductImagePath = "",
            });
            cat2.Products.Add(new Product
            {
                ProductTitle = "Sony TV",
                ProductDescription = "Sony TV Description",
                ProductPrice = 20000,
                ProductImagePath = "",
            });

            context.ProductCategories.Add(cat1);
            context.ProductCategories.Add(cat2);
            await context.SaveChangesAsync();

            var user = new IdentityUser { UserName = "test@email.com", Email = "test@email.com" };
            var result = await userManager.CreateAsync(user, "Test@@123");
        }
    }
}
