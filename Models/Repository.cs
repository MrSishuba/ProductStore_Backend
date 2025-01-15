using Microsoft.EntityFrameworkCore;

namespace Assignment3_Backend.Models
{
    public class Repository:IRepository
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            _appDbContext.Add(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;

        }

        //get the products by brand and eager laod the prodcut type
        public async Task<List<Product>> GetProductsAsync()
        {
            IQueryable<Product> products = _appDbContext.Products.Include(p => p.Brand).Include(p => p.ProductType);
            return await products.ToListAsync();
        }

        //just getting brands
        public async Task<List<Brand>> GetBrandsAsync()
        {
            IQueryable<Brand> brands = _appDbContext.Brands;
            return await brands.ToListAsync();
        }
        //just getting product types
        public async Task<List<ProductType>> GetProductTypesAsync()
        {
            IQueryable<ProductType> producttypes = _appDbContext.ProductTypes;
            return await producttypes.ToListAsync();
        }
    }
}

