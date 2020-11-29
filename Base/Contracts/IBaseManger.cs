using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChattyAPI.Base.Contracts
{
    public interface IBaseManager : IDisposable
    {
        public Guid GenerateGd();
        public Task<T> PostQuery<T>(string sql, object parameters = null);
        public Task PostQuery(string sql, object parameters = null);
        public Task<List<T>> GetManyQuery<T>(string sql, object parameters = null);
        public Task<T> GetQuery<T>(string sql, object parameters = null);
        public Task<T> DeleteQuery<T>(string sql, object parameters = null);
        public Task DeleteQuery(string sql, object parameters = null);
        public Task<T> PutQuery<T>(string sql, object parameters = null);
        public Task PutQuery(string sql, object parameters = null);
    }
}