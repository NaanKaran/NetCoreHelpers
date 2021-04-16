﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetCoreHelpers
{
    public static class ExtensionHelper
    {

        public static T ToEnum<T>(this string value)
        {
            var data = Enum.TryParse(typeof(T), value, true, out object result);
            if (result == null)
            {
                return default(T);
            }
            return (T)result;
        }

        public static int ToInt(this string value)
        {
            int.TryParse(value, out var result);
            return result;
        }

        public static long ToLong(this string value)
        {
            long.TryParse(value, out var result);
            return result;
        }
        public static string ToJsonString(this object obj)
        {
            return obj != null ? JsonConvert.SerializeObject(obj) : string.Empty;
        }

      
        public static T ConvertToModel<T>(this string obj)
        {
            try
            {
                return obj != null ? JsonConvert.DeserializeObject<T>(obj) : default(T);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // ignored
            }

            return default(T);
        }

        //public static string ToFileExtension(this string obj)
        //{
        //    var mapping = Registry.ClassesRoot.GetSubKeyNames()
        //              .Select(key => new
        //              {
        //                  Key = key,
        //                  ContentType = Registry.ClassesRoot.OpenSubKey(key).GetValue("Content Type")
        //              })
        //               .Where(x => x.ContentType != null)
        //               .ToLookup(x => x.ContentType, x => x.Key);
        //    return mapping[obj].FirstOrDefault();
        //}


        public static bool IsNullOrDefault(this int? obj)
        {
            return (obj == null || default(int) == obj);
        }

        public static bool IsNullOrDefault(this long? obj)
        {
            return (obj == null || default(long) == obj);
        }

        /// <summary>
        /// True when this has value (ie) its not equal to '0'
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotDefault(this long obj)
        {
            return (default(long) != obj);
        }

        /// <summary>
        /// True when this has value (ie) its not equal to '0'
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotDefault(this int obj)
        {
            return (default(int) != obj);
        }

        public static bool IsNullOrWhiteSpace(this string obj)
        {
            return string.IsNullOrWhiteSpace(obj);
        }
        /// <summary>
        /// indicate this string is not null, empty or whitespace
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNullOrWhiteSpace(this string obj)
        {
            return !string.IsNullOrWhiteSpace(obj);
        }


        public static Dictionary<string, string> ToDictionary(this JArray data)
        {
            if (data == null)
            {
                return new Dictionary<string, string>();
            }
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();

            foreach (JObject content in data.Children<JObject>())
            {
                foreach (JProperty prop in content.Properties())
                {
                    myDictionary.Add(prop.Name, prop.Value?.ToString());
                }
            }

            return myDictionary;
            //return data.ToDictionary(k => ((JObject) k).Properties().First().Name,
            //    v => v.Values().First().Value<string>());
        }

        public static Dictionary<string, string> ToDictionary(this JObject data)
        {
            if (data == null)
            {
                return new Dictionary<string, string>();
            }
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();

            foreach (var content in data)
            {
                myDictionary.Add(content.Key, content.Value?.ToString());
            }

            return myDictionary;
        }

        public static bool IsNullOrEmpty(this string obj)
        {
            return string.IsNullOrEmpty(obj);
        }

        public static bool IsNotNullOrEmpty(this string obj)
        {
            return !string.IsNullOrEmpty(obj);
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }

        public static XDocument RemoveCDATAFromXDocument(this XDocument xDocument)
        {
            var nodes = xDocument.DescendantNodes().OfType<XCData>().ToList();
            foreach (var node in nodes)
            {
                node.ReplaceWith(new XText(node.Value));
            }
            return xDocument;
        }

        /// <summary>
        /// Converts the timestamp(1357290913) to UTC date time(1/4/2013 9:15:13 AM)
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string timeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(Convert.ToInt64(timeStamp));
            return dtDateTime;
        }

        public static string ToTimestamp(this DateTime value)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            var elapsedTime = value - dateTime;
            return ((long)elapsedTime.TotalSeconds).ToString();
        }

        public static bool IsValidGuid(this string val)
        {
            var isValid = Guid.TryParseExact(val, "D", out var cc);

            return cc != new Guid() && isValid;
        }

        public static byte[] ToByteArray(this Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            ms.Position = 0;
            return ms.ToArray();
        }

        public static DataTable CreateDataTable<T>(this IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        //public static byte[] CreateExcelDocument<T>(this IEnumerable<T> list)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds.Tables.Add(ListToDataTable(list.ToList()));
        //        var strm = CreateExcelDocumentAsStream(ds);
        //        return strm;
        //    }
        //    catch 
        //    {
        //        //Trace.WriteLine("Failed, exception thrown: " + ex.Message);
        //        return null;
        //    }
        //}

        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                        row[info.Name] = info.GetValue(t, null);
                    else
                        row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        public static string GetLast(this string source, int tailLength)
        {
            if (source == null) return source;
            if (tailLength >= source.Length)
                return source;
            return source.Substring(source.Length - tailLength);
        }
        private static Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }
        private static bool IsNullableType(Type type)
        {
            return (type == typeof(string) ||
                    type.IsArray ||
                    (type.IsGenericType &&
                     type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }


        private static string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }



        public static string GetExcelColumnName(int columnIndex)
        {
            //  eg  (0) should return "A"
            //      (1) should return "B"
            //      (25) should return "Z"
            //      (26) should return "AA"
            //      (27) should return "AB"
            //      ..etc..
            char firstChar;
            char secondChar;
            char thirdChar;

            if (columnIndex < 26)
            {
                return ((char)('A' + columnIndex)).ToString();
            }

            if (columnIndex < 702)
            {
                firstChar = (char)('A' + (columnIndex / 26) - 1);
                secondChar = (char)('A' + (columnIndex % 26));

                return string.Format("{0}{1}", firstChar, secondChar);
            }

            int firstInt = columnIndex / 26 / 26;
            int secondInt = (columnIndex - firstInt * 26 * 26) / 26;
            if (secondInt == 0)
            {
                secondInt = 26;
                firstInt = firstInt - 1;
            }
            int thirdInt = (columnIndex - firstInt * 26 * 26 - secondInt * 26);

            firstChar = (char)('A' + firstInt - 1);
            secondChar = (char)('A' + secondInt - 1);
            thirdChar = (char)('A' + thirdInt);

            return string.Format("{0}{1}{2}", firstChar, secondChar, thirdChar);
        }

        public static string ToCSV(this DataTable table)
        {
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
                }
            }
            string s = result.ToString(); s = s.TrimEnd(new char[] { '\r', '\n' });
            return s;

        }

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
        public static byte[] ReadToEnd(this Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }


        public static StringBuilder ConvertDataTableToCsvFile(this DataTable  dtData)
        {
            StringBuilder data = new StringBuilder();

            //Taking the column names.
            for (int column = 0; column < dtData.Columns.Count; column++)
            {
                //Making sure that end of the line, shoould not have comma delimiter.
                if (column == dtData.Columns.Count - 1)
                    data.Append(dtData.Columns[column].ColumnName.ToString().Replace(",", ";"));
                else
                    data.Append(dtData.Columns[column].ColumnName.ToString().Replace(",", ";") + ',');
            }

            data.Append(Environment.NewLine);//New line after appending columns.

            for (int row = 0; row < dtData.Rows.Count; row++)
            {
                for (int column = 0; column < dtData.Columns.Count; column++)
                {
                    ////Making sure that end of the line, shoould not have comma delimiter.
                    if (column == dtData.Columns.Count - 1)
                        data.Append(dtData.Rows[row][column].ToString().Replace(",", ";"));
                    else
                        data.Append(dtData.Rows[row][column].ToString().Replace(",", ";") + ',');
                }

                //Making sure that end of the file, should not have a new line.
                if (row != dtData.Rows.Count - 1)
                    data.Append(Environment.NewLine);
            }
            return data;
        }
    }

}
