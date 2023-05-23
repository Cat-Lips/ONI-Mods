using System;
using System.Collections.Generic;

namespace MyMods;

public static class LinqExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        var idx = -1;
        foreach (var item in source)
            action(item, ++idx);
    }
}
