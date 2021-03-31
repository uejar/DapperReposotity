using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Text;
using DapperExtensions;
using DapperExtensions.Sql;
using System.Reflection;
using DapperExtensions.Mapper;
using System.Linq;

namespace DapperReposotity
{
    public class DapperRepository<T> : IRepository<T> where T : class
    {
        protected virtual int commandTimeout { get; set; } = 30;
        protected virtual IDbConnection Connection { get; set; }
        public DapperRepository(IDbConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual int ExecuteInsert(T item, IDbTransaction transaction = null)
        {
            return Connection.Insert(item, transaction: transaction, commandTimeout: commandTimeout);

        }

        public virtual bool ExecuteInsert(IEnumerable<T> list, IDbTransaction transaction = null)
        {

            using (IDbConnection connection = Connection)
            {
                bool affected = false;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    connection.Insert(list, transaction: transaction, commandTimeout: commandTimeout);
                    transaction.Commit();
                    affected = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new DataException("插入事务失败", ex);
                }
                return affected;
            }

        }

        public virtual IEnumerable<T1> ExecuteList<T1>(string sql, object param = null, IDbTransaction transaction = null) where T1 : class
        {
            return Connection.Query<T1>(sql, param, transaction, commandTimeout: commandTimeout);
        }

        public virtual IEnumerable<T1> ExecutePageList<T1>(string sql, int page, int resultsPerPage, out long count, IDictionary<string, object> param = null) where T1 : class
        {
            param = param == null ? new Dictionary<string, object>() : param;
            string pageSQL = DapperExtensions.DapperExtensions.SqlDialect.GetPagingSql(sql, page, resultsPerPage, param);
            string countSQL = string.Format("SELECT COUNT(1)  FROM ({0}) t", sql);
            SqlMapper.GridReader reader = Connection.QueryMultiple(pageSQL + ";" + countSQL, param, commandTimeout: commandTimeout);
            IEnumerable<T1> list = reader.Read<T1>();
            count = reader.ReadFirstOrDefault<long>();
            return list;
        }

        public virtual dynamic ExecuteProcedure(string sql, object param)
        {
            return Connection.Query(sql, param, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
        }

        public virtual T1 ExecuteReader<T1>(string sql, object param = null, IDbTransaction transaction = null) where T1 : class
        {
            return Connection.QueryFirstOrDefault<T1>(sql, param, transaction, commandTimeout: commandTimeout);
        }

        public virtual int ExecuteSqlInt(string sql, object param = null, IDbTransaction transaction = null)
        {
            return Connection.QueryFirst<int>(sql, param, transaction, commandTimeout: commandTimeout);
        }

        public virtual bool ExecuteUpdate(T item, IDbTransaction transaction = null)
        {
            return Connection.Update(item, transaction, commandTimeout);
        }

        public virtual bool ExecuteUpdate(IEnumerable<T> list, IDbTransaction transaction = null)
        {
            using (IDbConnection connection = Connection)
            {
                bool affected = false;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    Connection.Update(list, transaction, commandTimeout);
                    transaction.Commit();
                    affected = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new DataException("更新事务失败", ex);
                }
                return affected;
            }
        }

        public virtual T GetById(object id, IDbTransaction transaction = null)
        {
            return Connection.Get<T>(id, transaction, commandTimeout);
        }

        public IEnumerable<T> GetList(object where, IDbTransaction transaction = null)
        {
            return Connection.GetList<T>(where, null, transaction, commandTimeout);
        }
    }
}
