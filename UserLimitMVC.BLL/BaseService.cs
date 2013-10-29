﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLimitMVC.BLL
{
    public abstract class BaseService<T> where T : class,new()
    {
        public IDAL.IBaseRepository<T> CurrentRepository { get; set; }
        //添加构造函数
        public BaseService()
        {
            SetCurrentRepository(); //构造函数里面去调用了，此设置当前仓储的抽象方法
        }

        public abstract void SetCurrentRepository(); //子类必须实现
        //对数据库的添加
        public T AddEntity(T entity)
        {
            return CurrentRepository.AddEntity(entity);
        }
        //对数据库的修改
        public bool UpdateEntity(T entity)
        {
            return CurrentRepository.UpdateEntity(entity);
        }
        //对数据库的删除
        public bool DeleteEntity(T entity)
        {
            return CurrentRepository.DeleteEntity(entity);
        }
        //简单查询
        public IQueryable<T> LoadEntities(Func<T, bool> whereLambda)
        {
            return CurrentRepository.LoadEntities(whereLambda);
        }
        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        /// <typeparam name="S">按照某个类进行排序</typeparam>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">一页显示多少条的数据</param>
        /// <param name="total">总条数</param>
        /// <param name="whereLambda">排序条件</param>
        /// <param name="isAsc">升序？降序？</param>
        /// <param name="orderByLambda">根据那个字段排序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out  int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            return CurrentRepository.LoadPageEntities(pageIndex, pageSize, out total, whereLambda, isAsc, orderByLambda);
        }
    }
}
