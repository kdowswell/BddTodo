using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BddTodo.Tests._Infrastructure
{
    public static class Default<T>
    {
        private static readonly T _value;

        static Default()
        {

            if (typeof(T).IsArray)
            {
                if (typeof(T).GetArrayRank() > 1)
                    _value = (T)(object)Array.CreateInstance(typeof(T).GetElementType(), new int[typeof(T).GetArrayRank()]);
                else
                    _value = (T)(object)Array.CreateInstance(typeof(T).GetElementType(), 0);
                return;
            }

            if (typeof(T) == typeof(string))
            {
                // string is IEnumerable<char>, but don't want to treat it like a collection
                _value = default(T);
                return;
            }

            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {
                // check if an empty array is an instance of T
                if (typeof(T).IsAssignableFrom(typeof(object[])))
                {
                    _value = (T)(object)new object[0];
                    return;
                }

                if (typeof(T).IsGenericType && typeof(T).GetGenericArguments().Length == 1)
                {
                    var elementType = typeof(T).GetGenericArguments()[0];
                    if (typeof(T).IsAssignableFrom(elementType.MakeArrayType()))
                    {
                        _value = (T)(object)Array.CreateInstance(elementType, 0);
                        return;
                    }

                    if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                    {
                        _value = (T)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(T).GetGenericArguments()));
                        return;
                    }

                    if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
                    {
                        _value = (T)Activator.CreateInstance(typeof(Enumerable).MakeGenericType(typeof(T).GetGenericArguments()));
                        return;
                    }
                }

                throw new NotImplementedException("No default value is implemented for type " + typeof(T).FullName);
            }

            _value = default(T);
        }

        public static T Value
        {
            get
            {
                return _value;
            }
        }
    }
}