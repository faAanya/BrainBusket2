using SQLite;
using System.Data;
using System.Linq.Expressions;



namespace MauiApp1.DB
{
    public class LocalDBService : IAsyncDisposable
    {
        private const string DB_NAME = "localdb.db";
        public SQLiteAsyncConnection _Connection;

        private static string DBPath => Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
        public SQLiteAsyncConnection DataBase =>
            (_Connection ??= new SQLiteAsyncConnection(DBPath,SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));


        private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
        {
            await DataBase.CreateTableAsync<TTable>();
        }

        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return DataBase.Table<TTable>();
        }

        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
           var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        } 
        
        public async Task<IEnumerable<TTable>> GetFilteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();
        }

        private async Task<TResult> Execute<TTable, TResult>(Func<Task <TResult>> action) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await action();
        }


        public async Task<TTable> GetItemByKeyItemAsunc<TTable>(object primaryKey) where TTable : class, new()
        {
           
            //await CreateTableIfNotExists<TTable>();
            //return await DataBase.GetAsync<TTable>(primaryKey);
            return await Execute<TTable, TTable> (async () => await DataBase.GetAsync<TTable>(primaryKey));
        }


        public async Task<bool> AddItemAsunc<TTable>(TTable item) where TTable : class, new()
        {
            // await CreateTableIfNotExists<TTable>();
            //return await DataBase.InsertAsync(item) >0;

            return await Execute<TTable, bool>(async () => await DataBase.InsertAsync(item) > 0);
        }

        public async Task<bool> UpdateItemAsunc<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await DataBase.UpdateAsync(item) > 0;
        }
        public async Task<bool> DeleteItemAsunc<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await DataBase.DeleteAsync(item) > 0;
        }
        public async Task<bool> DeleteByKeyItemAsunc<TTable>(object primaryKey) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await DataBase.DeleteAsync<TTable>(primaryKey) > 0;
        }

        public async ValueTask DisposeAsync()
        {
           await _Connection?.CloseAsync();
        }
        //public async Task<List<Product>> GetProduct()
        //{
        //    return await _Connection.Table<Product>().ToListAsync();
        //}

        //public async Task<Product> GetById(int id)
        //{
        //    return await _Connection.Table<Product>().Where(x => x.Id == id).FirstOrDefaultAsync();
        //}

        //public async Task Create(Product product)
        //{
        //    await _Connection.InsertAsync(product);
        //}

        //public async Task Update(Product product)
        //{
        //    await _Connection.UpdateAsync(product);
        //}


        //public async Task Delete(Product product)
        //{
        //    await _Connection.DeleteAsync(product);
        //}
    }
}
