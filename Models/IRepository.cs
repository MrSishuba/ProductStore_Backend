namespace Assignment3_Backend.Models
{
    public interface IRepository
    {
        //Declaring the contract between controller and reppository i.e my methods
        Task<bool> SaveChangesAsync();

        Task<List<Product>> GetProductsAsync();
        Task<List<ProductType>> GetProductTypesAsync();
        Task<List<Brand>> GetBrandsAsync();

        void Add<T>(T entity) where T : class;


    }
}
