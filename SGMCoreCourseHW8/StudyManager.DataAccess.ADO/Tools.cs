using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace StudyManager.DataAccess.ADO
{
    internal static class Tools
    {
        public static string GetTableName<T>()
        {
            var tableAttribute =
                (TableAttribute) Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));
            if (tableAttribute != null)
                return tableAttribute.Name;
            return nameof(T) + "s";
        }

        public static Dictionary<string, string> GetPropertiesWithoutKey<T>()
        {
            var prp = new Dictionary<string, string>();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var column = property.GetCustomAttribute<ColumnAttribute>();
                var key = property.GetCustomAttribute<KeyAttribute>();
                if (key == null)
                {
                    if (column != null)
                        prp.Add(property.Name, column.Name);
                    else
                        prp.Add(property.Name, property.Name);
                }
            }

            return prp;
        }

        public static Dictionary<string, string> Properties<T>()
        {
            var prp = new Dictionary<string, string>();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var column = property.GetCustomAttribute<ColumnAttribute>();
                if (column != null)
                    prp.Add(property.Name, column.Name);
                else
                    prp.Add(property.Name, property.Name);
            }

            return prp;
        }

        public static object GetPropValue<T>(T src, string propName)
        {
            return typeof(T).GetProperty(propName).GetValue(src, null);
        }

        public static void SetPropValue<T>(T src, string propName, object value)
        {
            typeof(T).GetProperty(propName).SetValue(src, value);
        }

        public static object GetKeyValue<T>(T src)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var key = property.GetCustomAttribute<KeyAttribute>();
                if (key != null) return property.GetValue(src, null);
            }

            return GetPropValue(src, "Id");
        }
    }
}