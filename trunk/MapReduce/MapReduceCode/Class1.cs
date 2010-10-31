using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;

namespace MapReduceCode
{
    public static class MapReduce
    {
        public static Task<TResult> ForEachParallel<TItem, TSubResult, TResult, TParam>(this IEnumerable items, Func<TItem, TParam, TSubResult> map, Func<TSubResult[], TResult> reduce, TParam param)
        {
            //Debugger.Launch();

            if (items == null) { throw new ArgumentNullException("items"); }
            if (map == null) { throw new ArgumentNullException("map"); }
            if (reduce == null) { throw new ArgumentNullException("reduce"); }

            return Task<TResult>.Factory.StartNew(() =>
            {
                List<Task<TSubResult>> tasks = new List<Task<TSubResult>>();

                foreach (TItem item in items)
                {
                    Task<TSubResult> t = Task<TSubResult>.Factory.StartNew(item2 =>
                        {
                            var mparam = (Tuple<TItem, TParam>)item2;
                            return map(mparam.Item1, mparam.Item2);
                        },
                        new Tuple<TItem, TParam>(item, param),
                        TaskCreationOptions.None | TaskCreationOptions.AttachedToParent);
                    tasks.Add(t);
                }

                List<TSubResult> results = new List<TSubResult>();
                foreach (Task<TSubResult> task in tasks)
                {
                    results.Add(task.Result);
                }

                return reduce(results.ToArray());
            });
        }
    }
}
