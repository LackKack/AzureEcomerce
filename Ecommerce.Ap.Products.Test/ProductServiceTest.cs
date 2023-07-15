using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profiles;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Ap.Products.Test
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProduct(dbContext);
            var productProfile = new ProductProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfiguration);
            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productProvider.GetProductsAsync();

            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Empty(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnProductUsingValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProduct(dbContext);
            var productProfile = new ProductProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfiguration);
            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productProvider.GetProductAsync(1);

            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product.Id == 1);
            Assert.Empty(product.ErrorMessage);
        }
        [Fact]
        public async Task GetProductsReturnProductUsingInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnProductUsingInValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProduct(dbContext);
            var productProfile = new ProductProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfiguration);
            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productProvider.GetProductAsync(-1);

            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotEmpty(product.ErrorMessage);
        }


        private void CreateProduct(ProductsDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i * 10,
                    Price = (decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}