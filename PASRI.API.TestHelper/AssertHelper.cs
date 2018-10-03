using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace PASRI.API.TestHelper
{
    /// <summary>
    /// Static method provider to assist in tests
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AssertHelper
    {
        private static readonly Random Random = new Random();
        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// From an array of objects, return a random <see cref="string"/> value of a
        /// specified length that does not exist in the specified <see cref="string"/>
        /// property of any element in the array
        /// </summary>
        public static string GetValueNotInArray(object[] array, 
            string propertyName, 
            int length)
        {
            Start:
            var testValue = new string(Enumerable.Repeat(Alphabet, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());

            foreach (var o in array)
            {
                var propertyInfo = o.GetType().GetProperty(propertyName);
                var propertyValue = propertyInfo.GetValue(o, null).ToString();

                if (string.Compare(propertyValue, testValue, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    goto Start;
                }
            }

            return testValue;
        }

        /// <summary>
        /// From an array of objects, return a random <see cref="Int32"/> value of a
        /// between the specified minimum and maximum value that does not exist in
        /// the specified <see cref="Int32"/> property of any element in the array
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static int GetValueNotInArray(object[] array,
            string propertyName,
            int minValue,
            int maxValue)
        {
            Start:
            var testValue = Random.Next(minValue, maxValue);

            foreach (var o in array)
            {
                var propertyInfo = o.GetType().GetProperty(propertyName);
                if ((int)propertyInfo.GetValue(o, null) == testValue)
                {
                    goto Start;
                }
            }

            return testValue;
        }

        /// <summary>
        /// Compares the properties of two objects of the same type and returns if all properties are equal.
        /// </summary>
        /// <param name="objectA">The first object to compare.</param>
        /// <param name="objectB">The second object to compare.</param>
        /// <param name="ignoreList">A list of property names to ignore from the comparison.
        /// </param>
        /// <returns><c>true</c> if all property values
        ///           are equal, otherwise <c>false</c>.</returns>
        public static void AreObjectsEqual(object objectA, object objectB, params string[] ignoreList)
        {
            if (objectA != null && objectB != null)
            {
                var objectType = objectA.GetType();

                foreach (var propertyInfo in objectType.GetProperties(
                        BindingFlags.Public | BindingFlags.Instance)
                    .Where(
                        p => p.CanRead &&
                             !ignoreList.Contains(p.Name) &&
                             p.GetIndexParameters().Length == 0 &&
                             p.Name != "Capacity"))
                {
                    var valueA = propertyInfo.GetValue(objectA, null);
                    var valueB = propertyInfo.GetValue(objectB, null);

                    // if it is a primitive type, value type or implements
                    // IComparable, just directly try and compare the value
                    if (CanDirectlyCompare(propertyInfo.PropertyType))
                    {
                        if (!AreValuesEqual(valueA, valueB))
                        {
                            Assert.Fail("Mismatch with property '{0}.{1}' found.",
                                objectType.FullName, propertyInfo.Name);
                        }
                    }
                    // if it implements IEnumerable, then scan any items
                    else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        // null check
                        if (valueA == null && valueB != null || valueA != null && valueB == null)
                        {
                            Assert.Fail("Mismatch with property '{0}.{1}' found.",
                                objectType.FullName, propertyInfo.Name);
                        }
                        else if (valueA != null && valueB != null)
                        {
                            var collectionItems1 = ((IEnumerable) valueA).Cast<object>();
                            var collectionItems2 = ((IEnumerable) valueB).Cast<object>();
                            var collectionItemsCount1 = collectionItems1.Count();
                            var collectionItemsCount2 = collectionItems2.Count();

                            // check the counts to ensure they match
                            if (collectionItemsCount1 != collectionItemsCount2)
                            {
                                Assert.Fail("Collection counts for property '{0}.{1}' do not match.",
                                    objectType.FullName, propertyInfo.Name);
                            }
                            // and if they do, compare each item...
                            // this assumes both collections have the same order
                            else
                            {
                                for (var i = 0; i < collectionItemsCount1; i++)
                                {
                                    var collectionItem1 = collectionItems1.ElementAt(i);
                                    var collectionItem2 = collectionItems2.ElementAt(i);
                                    var collectionItemType = collectionItem1.GetType();

                                    if (CanDirectlyCompare(collectionItemType))
                                    {
                                        if (!AreValuesEqual(collectionItem1, collectionItem2))
                                        {
                                            Assert.Fail("Item {0} in property collection '{1}.{2}' does not match.",
                                                i, objectType.FullName, propertyInfo.Name);
                                        }
                                    }

                                    AreObjectsEqual(collectionItem1, collectionItem2, ignoreList);
                                }
                            }
                        }
                    }
                    else if (propertyInfo.PropertyType.IsClass)
                    {
                        AreObjectsEqual(propertyInfo.GetValue(objectA, null),
                            propertyInfo.GetValue(objectB, null), ignoreList);
                    }
                    else
                    {
                        Assert.Fail("Cannot compare property '{0}.{1}'.",
                            objectType.FullName, propertyInfo.Name);
                    }
                }

                // Handle the enumerated properties until there are no more objects
                // in the enumeration
                foreach (var propertyInfo in objectType.GetProperties(
                        BindingFlags.Public | BindingFlags.Instance)
                    .Where(
                        p => p.CanRead &&
                             !ignoreList.Contains(p.Name) &&
                             p.GetIndexParameters().Length > 0 &&
                             p.Name != "Capacity"))
                {
                    for (var i=0; i<int.MaxValue; i++)
                    {
                        try
                        {
                            var valueA = propertyInfo.GetValue(objectA, new object[] {i});
                            var valueB = propertyInfo.GetValue(objectB, new object[] {i});
                            AreObjectsEqual(valueA, valueB);
                        }
                        catch (TargetInvocationException) 
                        {
                            // No more objects in the list to compare
                            break;
                        }
                    }
                }
            }
            else
            {
                Assert.That(objectA, Is.EqualTo(objectB));
            }
        }

        /// <summary>
        /// Determines whether value instances of the specified type can be directly compared.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     <c>true</c> if this value instances of the specified
        ///           type can be directly compared; otherwise, <c>false</c>.
        /// </returns>
        private static bool CanDirectlyCompare(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }

        /// <summary>
        /// Compares two values and returns if they are the same.
        /// </summary>
        /// <param name="valueA">The first value to compare.</param>
        /// <param name="valueB">The second value to compare.</param>
        /// <returns>True if both values match, otherwise false.</returns>
        private static bool AreValuesEqual(object valueA, object valueB)
        {
            bool result;

            var selfValueComparer = valueA as IComparable;

            if (valueA == null && valueB != null || valueA != null && valueB == null)
                result = false; // one of the values is null
            else if (selfValueComparer != null && selfValueComparer.CompareTo(valueB) != 0)
                result = false; // the comparison using IComparable failed
            else if (!Equals(valueA, valueB))
                result = false; // the comparison using Equals failed
            else
                result = true; // match

            return result;
        }
    }
}
