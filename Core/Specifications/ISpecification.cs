using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criterial { get; }
        List<Expression<Func<T, Object>>> Includes { get; }
        Expression<Func<T, Object>> OrderBy { get;  set; }
        Expression<Func<T, Object>> OrderByDescending { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
        bool IsPagingEnable { get; set; }
    }
}
