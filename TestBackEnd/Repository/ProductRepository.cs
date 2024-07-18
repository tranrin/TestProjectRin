using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;
using TestBackEnd.Domain;
using TestBackEnd.Interface;

namespace TestBackEnd.Repository
{
    public class ProductRepository : ConnectDatabase, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        #region CRUD
        public async Task<Product> Create(ProductRequest request)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", request.Name);
                    parameters.Add("@Price", request.Price);
                    parameters.Add("@CategoryId", request.CategoryId);
                    Product rs = await conn.QueryFirstOrDefaultAsync<Product>("IF EXISTS(SELECT ID FROM [Category] Where Id = @CategoryId) BEGIN INSERT INTO [Product] (Name, Price, CategoryId)  output inserted.* VALUES (@Name, @Price, @CategoryId) END", parameters);
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

        public async Task<Product> Delete(int id)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    Product list = await conn.QueryFirstOrDefaultAsync<Product>("Delete from [Product] output deleted.* where Id = @Id", parameters);
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

        public async Task<IEnumerable<Product>> Gets()
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    IEnumerable<Product> rs = await conn.QueryAsync<Product>("USP_Product_Gets", commandType: CommandType.StoredProcedure); ;
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
        public async Task<Product> Update(Product request)
        {
            using (SqlConnection conn = IConnectData())
            {
                try
                {
                    await conn.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", request.Id);
                    parameters.Add("@Name", request.Name);
                    parameters.Add("@Price", request.Price);
                    parameters.Add("@CategoryId", request.CategoryId);

                    Product rs = await conn.QueryFirstOrDefaultAsync<Product>("IF EXISTS(SELECT ID FROM [Category] Where Id = @CategoryId) BEGIN UPDATE [Product] SET Name = @Name, Price = @Price, CategoryId = @CategoryId output inserted.* WHERE Id = @Id END", parameters);
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

