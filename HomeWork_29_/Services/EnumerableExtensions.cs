using System.Collections.Generic;

namespace System.Collections.ObjectModel;

public static class EnumerableExtensions
{
    public static ObservableCollection<T> ToObservableCollection<T>(
        this IEnumerable<T> items)
    {
        return new ObservableCollection<T>(items);
    }

    public static ObservableCollection<T> ToObservableCollection<T>(
        this List<T> items)
    {
        return new ObservableCollection<T>(items);
    }
}