using System;
using System.Collections.Generic;

namespace LinqG1Day1
{
    public class EmployeeDTo
    {
        public EmployeeDTo(int iD, string fullName)
        {
            ID = iD;
            FullName = fullName;
        }

        public int ID { get; set; }
        public string FullName { get; set; }
    }

    public static class MyLinqExtensions
    {
        public static List<T> MyWhere<T>(this List<T> source, Func<T, bool> predicate)
        {
            List<T> list = new List<T>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<T> MyWhereWithIndex<T>(this List<T> source, Func<T, int, bool> predicate)
        {
            List<T> list = new List<T>();
            int index = 0;
            foreach (var item in source)
            {
                if (predicate(item, index))
                {
                    list.Add(item);
                }
                index++;
            }
            return list;
        }

        public static List<TResult> MySelect<TSource, TResult>(this List<TSource> source, Func<TSource, TResult> selector)
        {
            List<TResult> list = new List<TResult>();
            foreach (var item in source)
            {
                list.Add(selector(item));
            }
            return list;
        }

        public static List<TResult> MySelectWithIndex<TSource, TResult>(this List<TSource> source, Func<TSource, int, TResult> selector)
        {
            List<TResult> list = new List<TResult>();
            int index = 0;
            foreach (var item in source)
            {
                list.Add(selector(item, index));
                index++;
            }
            return list;
        }

        public static List<T> MySkip<T>(this List<T> source, int count)
        {
            List<T> list = new List<T>();
            int i = 0;
            foreach (var item in source)
            {
                if (i >= count)
                {
                    list.Add(item);
                }
                i++;
            }
            return list;
        }

        public static List<T> MyTake<T>(this List<T> source, int count)
        {
            List<T> list = new List<T>();
            int i = 0;
            foreach (var item in source)
            {
                if (i < count)
                {
                    list.Add(item);
                }
                else
                {
                    break;
                }
                i++;
            }
            return list;
        }

        public static List<T> MyTakeLast<T>(this List<T> source, int count)
        {
            List<T> list = new List<T>();
            int skipCount = source.Count - count;
            if (skipCount < 0) skipCount = 0;

            for (int i = skipCount; i < source.Count; i++)
            {
                list.Add(source[i]);
            }
            return list;
        }

        public static List<T> MySkipLast<T>(this List<T> source, int count)
        {
            List<T> list = new List<T>();
            int takeCount = source.Count - count;
            for (int i = 0; i < takeCount && i < source.Count; i++)
            {
                if (i >= 0)
                {
                    list.Add(source[i]);
                }
            }
            return list;
        }

        public static List<T> MyTakeWhile<T>(this List<T> source, Func<T, bool> predicate)
        {
            List<T> list = new List<T>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
                else
                {
                    break;
                }
            }
            return list;
        }

        public static List<T> MySkipWhile<T>(this List<T> source, Func<T, bool> predicate)
        {
            List<T> list = new List<T>();
            bool skipping = true;
            foreach (var item in source)
            {
                if (skipping)
                {
                    if (!predicate(item))
                    {
                        skipping = false;
                        list.Add(item);
                    }
                }
                else
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<T> MyPagination<T>(this List<T> source, int pageNumber, int pageSize)
        {
            return source.MySkip((pageNumber - 1) * pageSize).MyTake(pageSize);
        }

        public static List<List<T>> MyChunk<T>(this List<T> source, int size)
        {
            List<List<T>> chunks = new List<List<T>>();
            for (int i = 0; i < source.Count; i += size)
            {
                List<T> chunk = new List<T>();
                for (int j = i; j < i + size && j < source.Count; j++)
                {
                    chunk.Add(source[j]);
                }
                chunks.Add(chunk);
            }
            return chunks;
        }

        public static List<T> MyOrderBy<T, TKey>(this List<T> source, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            List<T> list = new List<T>(source);
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (keySelector(list[i]).CompareTo(keySelector(list[j])) > 0)
                    {
                        T temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }

        public static List<T> MyOrderByDescending<T, TKey>(this List<T> source, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            List<T> list = new List<T>(source);
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (keySelector(list[i]).CompareTo(keySelector(list[j])) < 0)
                    {
                        T temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }

        public static T MyFirst<T>(this List<T> source)
        {
            if (source.Count == 0) throw new InvalidOperationException();
            return source[0];
        }

        public static T MyElementAt<T>(this List<T> source, int index)
        {
            return source[index];
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = [1, 2, 3, -5, 67, 8, -12, -20, 30];
            List<string> names = ["sara", "Doaa", "Rahaf", "wafaa", "omnia"];

            var filtered = nums.MyWhere(n => n > 0);
            foreach (var item in filtered)
            {
                Console.WriteLine(item);
            }
        }
    }
}
