using TestBackEnd.Domain;

namespace TestBackEnd.Interface
{
    public interface IProductRepository
    {
        #region CRUD
        Task<IEnumerable<Product>> Gets();
        Task<Product> Create(ProductRequest request);
        Task<Product> Update(Product request);
        Task<Product> Delete(int id);
        #endregion
    }
}
