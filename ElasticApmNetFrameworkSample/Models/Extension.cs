using System.Collections.Generic;
using System.Linq;

namespace ElasticApmNetFrameworkSample.Models
{
    public static class Extension
    {
        public static IEnumerable<Out> ConvertAll<In, Out>(this IEnumerable<In> data) where In : IMappable<Out>
        {
            Out[] outArray = new Out[data.Count()];
            for (int i = 0; i < outArray.Length; i++)
            {
                outArray.SetValue(data.ElementAt(i).Convert(), i);
            }
            return outArray;
        }
    }
}