namespace Kata
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static TSource MinBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer = null)
        {
            // Argument validation and defaulting for comparer
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null) comparer = Comparer<TKey>.Default;

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements.");

                var minElement = enumerator.Current;
                var minValue = keySelector(minElement);

                while (enumerator.MoveNext())
                {
                    var currentElement = enumerator.Current;
                    var currentValue = keySelector(currentElement);

                    if (comparer.Compare(currentValue, minValue) < 0)
                    {
                        minElement = currentElement;
                        minValue = currentValue;
                    }
                }

                return minElement;
            }
        }
    }
}