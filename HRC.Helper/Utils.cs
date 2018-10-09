using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace HRC.Helper
{
    [ExcludeFromCodeCoverage]
    public static class Utils
    {
        public static IEnumerable<FieldInfo> GetAllConstantsStartsWith(Type type, string startsWith)
        {
            return type.GetFields(
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name.StartsWith(startsWith));
        }

        public static IEnumerable<FieldInfo> GetAllConstantsEndsWith(Type type, string endsWith)
        {
            return type.GetFields(
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.Name.EndsWith(endsWith));
        }

        public static string ConvertListToCsv<T>(List<T> list) =>
            string.Join(",", list.ToArray());

        public static List<string> ConvertCsvToList(string csv) =>
            csv.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();

    }
}