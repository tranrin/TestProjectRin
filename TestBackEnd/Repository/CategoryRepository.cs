using System.Data.SqlClient;
using System.Data;
using TestBackEnd.Domain;
using TestBackEnd.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;

namespace TestBackEnd.Repository
{
    public class CategoryRepository : ConnectDatabase, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        #region CRUD
        public async Task<Category> Create(CategoryRequest request)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", request.Name);
                    Category rs = await conn.QueryFirstOrDefaultAsync<Category>("INSERT INTO [Category] (Name)  output inserted.* VALUES (@Name);", parameters);
                    return rs;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<Category> Delete(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    Category list = await conn.QueryFirstOrDefaultAsync<Category>("IF NOT EXISTS(SELECT ID FROM [Product] Where CategoryId = @Id) Begin Delete from [Category] output deleted.* where Id = @Id End", parameters);
                    return list;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<IEnumerable<Category>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<Category> rs = await conn.QueryAsync<Category>("USP_Category_Gets", commandType: CommandType.StoredProcedure);
                    return rs;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public async Task<Category> Update(Category request)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", request.Id);
                    parameters.Add("@Name", request.Name);
                    Category rs = await conn.QueryFirstOrDefaultAsync<Category>("UPDATE [Category] SET Name = @Name Output inserted.* WHERE Id = @Id;", parameters);
                    return rs;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

        }
    }


    #endregion

}

