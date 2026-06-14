using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Specifications;
using System.Linq.Expressions;

namespace E_Commerce.Core.Specifications
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? expression) : ISpecification<T>
    {
        protected BaseSpecification() : this(null) { }
        public Expression<Func<T, bool>>? Criteria => expression;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public bool IsDistinct { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagedEnabled { get; private set; }

        public IQueryable<T> AppliedCriteria(IQueryable<T> query)
        {
            if (Criteria != null)
            {
                query = query.Where(Criteria);
            }

            return query;
        }

        protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDescending = OrderByDescExpression;
        }
        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }

        protected void ApplyedPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagedEnabled = true;
        }
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria) : BaseSpecification<T>(criteria), ISpecifacation<T, TResult>
{
    protected BaseSpecification() : this(null!) { }
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> SelectExpression)
    {
        Select = SelectExpression;
    }
}
