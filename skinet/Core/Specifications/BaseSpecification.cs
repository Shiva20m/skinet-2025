using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {
    public Expression<Func<T, bool>> Criteria=>criteria;
    public Expression<Func<T, object>>? OrderBy { get; protected set; }
    public Expression<Func<T, object>>? OrderByDescending { get; protected set; }

    public bool isDistinct {get; private set;}

    public int Take {get; private set;}

    public int Skip {get; private set;}

    public bool isPaginationEnable {get; protected set;}

    // protected BaseSpecification(Expression<Func<T, bool>>? criteria)
    // {
    //     Criteria = criteria ?? throw new InvalidOperationException("Criteria cannot be null.");
    // }
    protected void ApplyDistinct()
    {
        isDistinct= true;
    }
    protected void ApplyPaging(int skip, int take)
    {
        
        if (take <= 0)
        {
            take = 10; // Set default value or handle appropriately
        }
        if (skip < 0)
        {
            skip = 0; // Ensure skip is not negative
        }
        Skip=skip;
        Take=take;
        isPaginationEnable=true;
    }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if(Criteria!=null)
        {
            query= query.Where(Criteria);
        }
        return query;
    }

    protected BaseSpecification(): this(null) { }
    }

    public class BaseSpecification<T, TResult>(
    Expression<Func<T, bool>>? criteria) : BaseSpecification<T>(criteria), ISpecification<T, TResult>
    {
        public new Expression<Func<T, bool>> Criteria => criteria;
        public Expression<Func<T, TResult>>? Select { get; protected set; }

        // Constructor with primary constructor syntax
        // Removed explicit constructor to avoid conflict with primary constructor

        // Method to add a select expression
        protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
        {
            Select = selectExpression;
        }
    }



}