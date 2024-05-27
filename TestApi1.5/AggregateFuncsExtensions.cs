using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Specialized;
using TestApi1._5.Entity;

namespace TestApi1._5
{
    public static class AggregateFuncsExtensions
    {
        public static decimal Median<T>(this IEnumerable<T> table, Func<T, decimal> medianSelector) where T : class
        {
            var toMedianArray = table
                .Select(medianSelector)
                .OrderBy(x => x);

            var count = toMedianArray.Count();

            if(count == 0)
                return 0;

            if(count % 2 == 1)
            {
                return toMedianArray.ElementAt((count - 1) / 2);
            }
            else
            {
                int half = (count - 1) / 2;

                return (toMedianArray.ElementAt(half) + toMedianArray.ElementAt(half + 1)) / 2;
            }
        }

        //public static decimal PricesMedian(string PricesId, DbSet<Prices> prices)
        //{
        //    return 0;
        //}
    }
}
