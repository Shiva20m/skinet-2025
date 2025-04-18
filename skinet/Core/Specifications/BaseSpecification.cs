using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
    public Expression<Func<T, bool>> Criteria { get; protected set; }
    public Expression<Func<T, object>>? OrderBy { get; protected set; }
    public Expression<Func<T, object>>? OrderByDescending { get; protected set; }

    public bool isDistinct {get; protected set;}

    public int Take {get; protected set;}

    public int Skip {get; protected set;}

    public bool isPaginationEnable {get; protected set;}

    protected BaseSpecification(Expression<Func<T, bool>>? criteria)
    {
        Criteria = criteria ?? throw new InvalidOperationException("Criteria cannot be null.");
    }
    protected void ApplyDistinct()
    {
        isDistinct= true;
    }
    protected void ApplyPaging(int take, int skip)
    {
        Take = take <= 0 ? 6 : take;
        Skip = skip < 0 ? 0 : skip;
        isPaginationEnable = true;
    }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if(Criteria!=null)
        {
            query= query.Where(Criteria);
        }
        return query;
    }

    protected BaseSpecification() { }
    }

    public class BaseSpecification<T, TResult> : BaseSpecification<T>, ISpecification<T, TResult>
    {
        protected BaseSpecification() {}
        public new Expression<Func<T, bool>>Criteria { get; protected set; }
        public  Expression<Func<T, TResult>>? Select{ get; protected set; }
        protected BaseSpecification(Expression<Func<T, bool>>? criteria):base(criteria)
        {
            Criteria = criteria ?? throw new InvalidOperationException("Criteria cannot be null.");
        }
        protected void AddSelect(Expression<Func<T, TResult>>selectExpression)
        {
            Select = selectExpression;
        }

        
    }

}