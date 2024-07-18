using TestBackEnd.Domain;

namespace TestBackEnd.Interface
{
    public interface ICategoryRepository
    {
        #region CRUD
        Task<IEnumerable<Category>> Gets();
        Task<Category> Create(CategoryRequest request);
        Task<Category> Update(Category request);
        Task<Category> Delete(int id);
        #endregion
    }
}
