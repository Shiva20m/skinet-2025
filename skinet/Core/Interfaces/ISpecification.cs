using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>Criteria{get;}
        Expression<Func<T, Object>>? OrderBy{get;}
        Expression<Func<T, Object>>? OrderByDescending{get;}
        bool isDistinct {get;}
    }

    public interface ISpecification<T, TResult>: ISpecification<T>
    {
        Expression<Func<T, TResult>>? Select{get;}
    }
    
}