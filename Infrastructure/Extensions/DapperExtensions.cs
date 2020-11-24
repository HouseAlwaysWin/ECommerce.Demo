using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using Dapper;
using Dapper.Contrib.Extensions;
using ECommerce.Demo.API.Extensions;
using static Dapper.Contrib.Extensions.SqlMapperExtensions;

namespace ECommerce.Demo.Infrastructure.Extensions {

    public static class DapperExtensions {

        private static readonly Dictionary<string, ISqlCommand> CmdDict = new Dictionary<string, ISqlCommand> {
            ["sqlconnection"] = new SqlServerCmd (),
            ["npgsqlconnection"] = new PostgresCmd (),
            ["mysqlconnection"] = new MySqlCmd (),
        };

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ExplicitKeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ComputedProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> WhereProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> OrderByProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ();

        private static readonly ConcurrentDictionary<string, string> GetQueries = new ConcurrentDictionary<string, string> ();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName = new ConcurrentDictionary<RuntimeTypeHandle, string> ();
        private static readonly ISqlCommand DefaultCmd = new SqlServerCmd ();

        private interface ISqlCommand {
            string GetPaginated (string tableName, string orderBy, int currentPage, int itemsPerPage, bool isDesc);
        }

        private class SqlServerCmd : ISqlCommand {
            public string GetPaginated (string tableName, string orderBy, int currentPage, int itemsPerPage, bool isDesc) {
                string desc = isDesc ? "DESC" : "ASC";
                int totalItems = (currentPage - 1) * itemsPerPage;
                return $"SELECT * FROM {tableName} ORDER BY {orderBy} {desc} OFFSET {totalItems} ROWS FETCH NEXT {itemsPerPage} ROWS ONLY ";
            }
        }
        private class PostgresCmd : ISqlCommand {
            public string GetPaginated (string tableName, string orderBy, int currentPage, int itemsPerPage, bool isDesc) {
                throw new NotImplementedException ();
            }
        }

        private class MySqlCmd : ISqlCommand {
            public string GetPaginated (string tableName, string orderBy, int currentPage, int itemsPerPage, bool isDesc) {
                throw new NotImplementedException ();
            }
        }

        [AttributeUsage (AttributeTargets.Property)]
        private class OrderByAttribute : Attribute {
            public OrderByAttribute (bool isDesc = false) {
                IsDesc = isDesc;
            }
            /// <summary>
            /// Whether an order is IsDesc
            /// </summary>
            public bool IsDesc { get; }
        }

        private static ISqlCommand GetSqlCommand (IDbConnection connection) {

            var name = GetDatabaseType?.Invoke (connection).ToLower () ??
                connection.GetType ().Name.ToLower ();

            return CmdDict.TryGetValue (name, out var cmd) ? cmd : DefaultCmd;
        }

        private static bool IsWriteable (PropertyInfo pi) {
            var attributes = pi.GetCustomAttributes (typeof (WriteAttribute), false).AsList ();
            if (attributes.Count != 1) return true;

            var writeAttribute = (WriteAttribute) attributes[0];
            return writeAttribute.Write;
        }

        private static List<PropertyInfo> TypePropertiesCache (Type type) {
            if (TypeProperties.TryGetValue (type.TypeHandle, out IEnumerable<PropertyInfo> pis)) {
                return pis.ToList ();
            }

            var properties = type.GetProperties ().Where (IsWriteable).ToArray ();
            TypeProperties[type.TypeHandle] = properties;
            return properties.ToList ();
        }

        private static List<PropertyInfo> ExplicitKeyPropertiesCache (Type type) {
            if (ExplicitKeyProperties.TryGetValue (type.TypeHandle, out IEnumerable<PropertyInfo> pi)) {
                return pi.ToList ();
            }

            var explicitKeyProperties = TypePropertiesCache (type).Where (p => p.GetCustomAttributes (true).Any (a => a is ExplicitKeyAttribute)).ToList ();

            ExplicitKeyProperties[type.TypeHandle] = explicitKeyProperties;
            return explicitKeyProperties;
        }

        private static List<PropertyInfo> KeyPropertiesCache (Type type) {
            if (KeyProperties.TryGetValue (type.TypeHandle, out IEnumerable<PropertyInfo> pi)) {
                return pi.ToList ();
            }

            var allProperties = TypePropertiesCache (type);
            var keyProperties = allProperties.Where (p => p.GetCustomAttributes (true).Any (a => a is KeyAttribute)).ToList ();

            if (keyProperties.Count == 0) {
                var idProp = allProperties.Find (p => string.Equals (p.Name, "id", StringComparison.CurrentCultureIgnoreCase));
                if (idProp != null && !idProp.GetCustomAttributes (true).Any (a => a is ExplicitKeyAttribute)) {
                    keyProperties.Add (idProp);
                }
            }

            KeyProperties[type.TypeHandle] = keyProperties;
            return keyProperties;
        }

        private static List<PropertyInfo> OrderByPropertiesCache (Type type) {
            if (OrderByProperties.TryGetValue (type.TypeHandle, out IEnumerable<PropertyInfo> pi)) {
                return pi.ToList ();
            }

            var allProperties = TypePropertiesCache (type);
            var orderByProperties = allProperties.Where (p => p.GetCustomAttributes (true).Any (a => a is OrderByAttribute)).ToList ();

            OrderByProperties[type.TypeHandle] = orderByProperties;
            return orderByProperties;
        }

        private static bool IsOrderByDesc (PropertyInfo pi) {
            var attributes = pi.GetCustomAttributes (typeof (OrderByAttribute), false).AsList ();
            if (attributes.Count != 1) return true;

            var orderByAttribute = (OrderByAttribute) attributes[0];
            return orderByAttribute.IsDesc;
        }

        private static PropertyInfo GetSingleKey<T> (string method) {
            var type = typeof (T);
            var keys = KeyPropertiesCache (type);
            var explicitKeys = ExplicitKeyPropertiesCache (type);
            var keyCount = keys.Count + explicitKeys.Count;
            if (keyCount > 1)
                throw new DataException ($"{method}<T> only supports an entity with a single [Key] or [ExplicitKey] property. [Key] Count: {keys.Count}, [ExplicitKey] Count: {explicitKeys.Count}");
            if (keyCount == 0)
                throw new DataException ($"{method}<T> only supports an entity with a [Key] or an [ExplicitKey] property");

            return keys.Count > 0 ? keys[0] : explicitKeys[0];
        }

        /// <summary>
        /// Specify a custom table name mapper based on the POCO type name
        /// </summary>
        public static TableNameMapperDelegate TableNameMapper;

        private static string GetTableName (Type type) {
            if (TypeTableName.TryGetValue (type.TypeHandle, out string name)) return name;

            if (TableNameMapper != null) {
                name = TableNameMapper (type);
            } else {
                //NOTE: This as dynamic trick falls back to handle both our own Table-attribute as well as the one in EntityFramework 
                var tableAttrName =
                    type.GetCustomAttribute<TableAttribute> (false)?.Name ??
                    (type.GetCustomAttributes (false).FirstOrDefault (attr => attr.GetType ().Name == "TableAttribute") as dynamic)?.Name;

                if (tableAttrName != null) {
                    name = tableAttrName;
                } else {
                    name = type.Name + "s";
                    if (type.IsInterface && name.StartsWith ("I"))
                        name = name.Substring (1);
                }
            }

            TypeTableName[type.TypeHandle] = name;
            return name;
        }

        /// <summary>
        /// Returns a list of entities from table "Ts".
        /// Id of T must be marked with [Key] attribute.
        /// Entities created from interfaces are tracked/intercepted for changes and used by the Update() extension
        /// for optimal performance.
        /// </summary>
        /// <typeparam name="T">Interface or type to create and populate</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Entity of T</returns>
        public static IEnumerable<T> GetPaginated<T> (this IDbConnection connection, ref int total, int currentPage, int itemsPerPage, IDbTransaction transaction = null, int? commandTimeout = null) where T : class {
            var type = typeof (T);
            var cacheName = nameof (T) + nameof (GetPaginated);
            var countCache = nameof (GetPaginated);

            if (!GetQueries.TryGetValue (cacheName, out string sql)) {
                GetSingleKey<T> (nameof (GetPaginated));
                var name = GetTableName (type);
                var orderByProp = OrderByPropertiesCache (type).FirstOrDefault ();
                var isDesc = IsOrderByDesc (orderByProp);

                sql = GetSqlCommand (connection).GetPaginated (name, orderByProp.Name, currentPage, itemsPerPage, isDesc);
                GetQueries[cacheName] = sql;
            }

            if (!GetQueries.TryGetValue (countCache, out string sqlTotal)) {
                GetSingleKey<T> (nameof (GetPaginated));
                var name = GetTableName (type);
                var orderByProp = OrderByPropertiesCache (type).FirstOrDefault ();
                var isDesc = IsOrderByDesc (orderByProp);

                sqlTotal = "SELECT COUNT(*) FROM " + name;
                GetQueries[countCache] = sqlTotal;
            }

            total = connection.QueryFirst<int> (sqlTotal, null, transaction, commandTimeout : commandTimeout);
            if (!type.IsInterface) {
                return connection.Query<T> (sql, new { Page = currentPage, ItemsPerPage = itemsPerPage, }, transaction, commandTimeout : commandTimeout);
            }

            var result = connection.Query<T> (sql, new { Page = currentPage, ItemsPerPage = itemsPerPage, }, transaction, commandTimeout : commandTimeout);
            var list = new List<T> ();
            foreach (IDictionary<string, object> res in result) {
                var obj = ProxyGenerator.GetInterfaceProxy<T> ();
                foreach (var property in TypePropertiesCache (type)) {
                    var val = res[property.Name];
                    if (val == null) continue;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition () == typeof (Nullable<>)) {
                        var genericType = Nullable.GetUnderlyingType (property.PropertyType);
                        if (genericType != null) property.SetValue (obj, Convert.ChangeType (val, genericType), null);
                    } else {
                        property.SetValue (obj, Convert.ChangeType (val, property.PropertyType), null);
                    }
                }
                ((IProxy) obj).IsDirty = false; //reset change tracking and return
                list.Add (obj);
            }
            return list;
        }
    }
}