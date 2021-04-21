using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetCoreHelpers
{
    public static class StringExtension
    {
        /// <summary>
        /// IsNullOrWhiteSpace
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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


        /// <summary>
        /// string null check
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string obj)
        {
            return string.IsNullOrEmpty(obj);
        }

        /// <summary>
        /// string null or empty check
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string obj)
        {
            return !string.IsNullOrEmpty(obj);
        }
        /// <summary>
        /// string to Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            var data = Enum.TryParse(typeof(T), value, true, out object result);
            if (result == null)
            {
                return default(T);
            }
            return (T)result;
        }

        /// <summary>
        /// string to Int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            int.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// string to Long
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToLong(this string value)
        {
            long.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// ToJsonString
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            return obj != null ? JsonConvert.SerializeObject(obj) : string.Empty;
        }

        /// <summary>
        /// Json String to Model 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ConvertToModel<T>(this string obj) where T: class
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
    }
}
