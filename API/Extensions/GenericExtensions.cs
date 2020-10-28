using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using static Dapper.Contrib.Extensions.SqlMapperExtensions;

namespace API.Extensions {
    public static class GenericExtensions {

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ();
        /// <summary>
        /// Returns a single entity by a single id from table "Ts".
        /// Id must be marked with [Key] attribute.
        /// Entities created from interfaces are tracked/intercepted for changes and used by the Update() extension
        /// for optimal performance.
        /// </summary>
        /// <typeparam name="T">Interface or type to create and populate</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="id">Id of the entity to get, must be marked with [Key] attribute</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Entity of T</returns>
        // public static T GetByPaginated<T> (this IDbConnection connection, dynamic id, int page = 1, int itemsPerPage = 10, IDbTransaction transaction = null, int? commandTimeout = null) where T : class {
        //     var type = typeof (T);

        //     if (!GetQueries.TryGetValue (type.TypeHandle, out string sql)) {
        //         var key = GetSingleKey<T> (nameof (Get));
        //         var name = GetTableName (type);

        //         sql = $"SELECT * FROM {name} WHERE {key.Name} = @id";
        //         GetQueries[type.TypeHandle] = sql;
        //     }

        //     var dynParms = new DynamicParameters ();
        //     dynParms.Add ("@id", id);

        //     T obj;

        //     if (type.IsInterface) {
        //         var res = connection.Query (sql, dynParms).FirstOrDefault () as IDictionary<string, object>;

        //         if (res == null)
        //             return null;

        //         obj = ProxyGenerator.GetInterfaceProxy<T> ();

        //         foreach (var property in TypePropertiesCache (type)) {
        //             var val = res[property.Name];
        //             if (val == null) continue;
        //             if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition () == typeof (Nullable<>)) {
        //                 var genericType = Nullable.GetUnderlyingType (property.PropertyType);
        //                 if (genericType != null) property.SetValue (obj, Convert.ChangeType (val, genericType), null);
        //             } else {
        //                 property.SetValue (obj, Convert.ChangeType (val, property.PropertyType), null);
        //             }
        //         }

        //         ((IProxy) obj).IsDirty = false; //reset change tracking and return
        //     } else {
        //         obj = connection.Query<T> (sql, dynParms, transaction, commandTimeout : commandTimeout).FirstOrDefault ();
        //     }
        //     return obj;
        // }

    }
}