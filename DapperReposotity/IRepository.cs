using System;
using System.Collections.Generic;
using System.Data;

namespace DapperReposotity
{
    /// <summary>
    /// 通用仓储
    /// </summary>
    public partial interface IRepository
    {
        /// <summary>
        /// 执行sql返回一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        T ExecuteReader<T>(string sql, object param = null, IDbTransaction transaction = null) where T : class;
        /// <summary>
        /// 执行sql返回多个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteList<T>(string sql, object param = null, IDbTransaction transaction = null) where T : class;
        /// <summary>
        /// 执行sql，返回影响行数 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        int ExecuteSqlInt(string sql, object param = null, IDbTransaction transaction = null);
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="useWriteConn"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> ExecutePageList<T>(string sql, int page, int resultsPerPage, out long count, IDictionary<string, object> param = null) where T : class;
        /// <summary>
        /// 存储过程
        /// </summary>
        /// <param name="procname"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        dynamic ExecuteProcedure(string procname, object param);
    }
    /// <summary>
    /// 实体仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IRepository<T> : IRepository where T : class
    {
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        T GetById(dynamic id, IDbTransaction transaction = null);

        IEnumerable<T> GetList(object where, IDbTransaction transaction = null);
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        int ExecuteInsert(T item, IDbTransaction transaction = null);
        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="list"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool ExecuteInsert(IEnumerable<T> list, IDbTransaction transaction = null);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool ExecuteUpdate(T item, IDbTransaction transaction = null);
        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="list"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool ExecuteUpdate(IEnumerable<T> list, IDbTransaction transaction = null);
    }
}
