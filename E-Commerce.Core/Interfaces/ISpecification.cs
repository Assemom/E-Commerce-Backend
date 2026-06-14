using System.Linq.Expressions;

namespace E_Commerce.Core.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }

        bool IsDistinct { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagedEnabled { get; }

        IQueryable<T> AppliedCriteria(IQueryable<T> query);
    }

    public interface ISpecifacation<T, TResult> : ISpecification<T>
    {
        Expression<Func<T, TResult>>? Select { get; }
    }
}
