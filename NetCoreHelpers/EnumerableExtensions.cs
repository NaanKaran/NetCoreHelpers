using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace NetCoreHelpers
{
    /// <summary>
    /// Contains extension methods for <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Chunk the sequence based on chunkSize and returns sequence of chunks.
        /// </summary>
        /// <typeparam name="TResult">Type of source sequence.</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="chunkSize">Size of chunk</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// IEnumerable<int> input = new List<int>() { 1, 2, 3, 4 };
        /// int sumofFirstChunk = input.Chunk(2).First().Sum();
        /// ]]>
        /// </code>
        /// </example>
        public static IEnumerable<IEnumerable<TResult>> Chunk<TResult>(this IEnumerable<TResult> source, int chunkSize)
        {
            return source.
                Select((thread, index) => new { Index = index, Value = thread }).
                GroupBy(tuple => tuple.Index / chunkSize).
                Select(group => group.Select(tuple => tuple.Value));
        }
        /// <summary>
        /// Randomize the sequence
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source">The source of sequence</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// IEnumerable<int> input = new List<int>() { 1, 2, 3, 4 };
        /// IEnumerable<int> ramdomizedseQuence = input.Randomize();
        /// ]]>
        /// </code>
        /// </example>
        public static IEnumerable<TResult> Randomize<TResult>(this IEnumerable<TResult> source)
        {
            return source.
                Select((sourceItem, index) => new
                {
                    Item = sourceItem,
                    Id = Guid.NewGuid()
                }).
            OrderBy(t1 => t1.Id).Select(t1 => t1.Item);
        }


        /// <summary>
        /// IList to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                var columnName = prop.DisplayName ?? prop.Name;
                table.Columns.Add(columnName, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    var columnName = prop.DisplayName ?? prop.Name;
                    row[columnName] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;

        }
        /// <summary>
        /// List to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
