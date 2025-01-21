using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adminmart.Lib.DataBase
{
    //외부에서 쓸수있게 Public으로 작업
    public class MySqlDapperHelper : IDisposable
    {
        MySqlConnection _conn;
        //MySqlTransaction _trans;

        public MySqlDapperHelper()
        {
            _conn = new MySqlConnection("Server = 127.0.0.1; Port = 3306; Database = adminmart; Uid = root; Pwd = root;");
        }

        public List<T> GetQuery<T>(string sql, object param)
        {
            return Dapper.SqlMapper.Query<T>(_conn, sql, param).ToList();
        }

        public int Execute(string sql, object param)
        {
            return Dapper.SqlMapper.Execute(_conn, sql, param);
        }
        public T QuerySingle<T>(string sql,object param)
        {
            return Dapper.SqlMapper.QuerySingleOrDefault<T>(_conn, sql, param);
        }

        #region Dispose 관련
        private bool disposedValue;
        protected virtual void Dispose(bool disposing) 
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _conn.Dispose();
                    
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
