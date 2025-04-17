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

    protected BaseSpecification(Expression<Func<T, bool>>? criteria)
    {
        Criteria = criteria ?? throw new InvalidOperationException("Criteria cannot be null.");
    }
    protected void ApplyDistinct()
    {
        isDistinct= true;
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