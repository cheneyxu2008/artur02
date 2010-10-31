using System;
using System.Collections;
using MapReduceCode;
using System.Threading;
using System.Threading.Tasks;

namespace MapReduce
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Generate().ForEachParallel<int, int, int, int>(
                (element, param)=> 
                {
                    var result = element * param;
                    Console.WriteLine("Map: {0}, {1}", result, Task.CurrentId);
                    Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(500));
                    return result;
                }, 
                (subresult)=>
                {
                    var sum = 0;
                    foreach (var item in subresult)
                    {
                        sum += item;
                    }
                    Console.WriteLine("Reduce: {0}", sum);
                    return sum;
                }, 
                5);

            Console.WriteLine(a.Result);
            
        }

        static IEnumerable Generate()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return i;
            }
        }
    }
}
