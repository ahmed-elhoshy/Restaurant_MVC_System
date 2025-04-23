using System.Linq.Expressions;

namespace Resturant.Models
{
    public class QueryOptions<T> where T : class
    {
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, bool>> Where { get; set; }
        private List<string> includes = new List<string>();

        public string Includes
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    includes.AddRange(value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }

        public string[] GetIncludes() => includes.ToArray();

        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;
    }
}