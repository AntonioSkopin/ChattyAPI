using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ChattyAPI.Base.Contracts;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ChattyAPI.Base.Managers
{
    public class BaseManager : IBaseManager
    {
        private readonly IConfiguration _config;
        protected SqlConnection db
        {
            get
            {
                IConfigurationSection cs = _config.GetSection("ConnectionStrings");
                string s = cs["Cosial"];
                return new SqlConnection(s);
            }
        }

        public BaseManager(IConfiguration config)
        {
            _config = config;
        }

        public Guid GenerateGd()
        {
            return Guid.NewGuid();
        }

        public async Task<T> PostQuery<T>(string sql, object parameters = null)
        {
            var postedval = await db.QueryAsync<T>(sql, parameters);
            return postedval.FirstOrDefault();
        }

        public async Task PostQuery(string sql, object parameters = null)
        {
            await db.QueryAsync(sql, parameters);
        }

        public async Task<List<T>> GetManyQuery<T>(string sql, object parameters = null)
        {
            var lst = await db.QueryAsync<T>(sql, parameters);
            return lst.ToList();
        }

        public async Task<T> GetQuery<T>(string sql, object parameters = null)
        {
            var getVal = await db.QueryAsync<T>(sql, parameters);
            return getVal.FirstOrDefault();
        }

        public async Task<T> DeleteQuery<T>(string sql, object parameters = null)
        {
            var deletedVal = await db.QueryAsync<T>(sql, parameters);
            return deletedVal.FirstOrDefault();
        }

        public async Task DeleteQuery(string sql, object parameters = null)
        {
            await db.QueryAsync(sql, parameters);
        }

        public async Task<T> PutQuery<T>(string sql, object parameters = null)
        {
            var updatedVal = await db.QueryAsync<T>(sql, parameters);
            return updatedVal.FirstOrDefault();
        }

        public async Task PutQuery(string sql, object parameters = null)
        {
            await db.QueryAsync(sql, parameters);
        }

        public void Dispose() { }
    }
}