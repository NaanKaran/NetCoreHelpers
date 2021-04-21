using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetCoreHelpers
{
    public static class HttpHelper<T>
    {
        private static readonly HttpClientHandler SharedHandler = new HttpClientHandler(); //never dispose this
        static HttpClient GetClient()
        {
            //client code can dispose these HttpClient instances
            return new HttpClient(SharedHandler, disposeHandler: false);
        }

        //public static HttpClient httpClient = new HttpClient();
        /// <summary>
        /// HttpPost
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="jsonObject"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static T HttpPost(string httpUrl, string jsonObject, string contentType = "application/json")
        {
            using var httpClient =  GetClient();
            var content = new StringContent(jsonObject, Encoding.UTF8, contentType);
            using var response = httpClient.PostAsync(httpUrl, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// HttpPost
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="jsonObject"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static T HttpPost(string httpUrl, string jsonObject, Dictionary<string, string> headers, string contentType = "application/json")
        {
            using var httpClient = GetClient();
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            var content = new StringContent(jsonObject, Encoding.UTF8, contentType);
            using var response = httpClient.PostAsync(httpUrl, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// HttpPostAsync
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="jsonObject"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<T> HttpPostAsync(string httpUrl, string jsonObject, string contentType = "application/json")
        {
            using var httpClient = GetClient();
            var content = new StringContent(jsonObject, Encoding.UTF8, contentType);
            using var response = await httpClient.PostAsync(httpUrl, content).ConfigureAwait(false);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result ?? "");
        }
        /// <summary>
        /// HttpPostAsync
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="jsonObject"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <param name="useHttpHandler"></param>
        /// <returns></returns>
        public static async Task<T> HttpPostAsync(string httpUrl, string jsonObject, Dictionary<string, string> headers, string contentType = "application/json", bool useHttpHandler = true)
        {
            using var httpClient = useHttpHandler ? GetClient() : new HttpClient();
            var content = new StringContent(jsonObject, Encoding.UTF8, contentType);
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            using var response = await httpClient.PostAsync(httpUrl, content).ConfigureAwait(false);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// HttpPostStringAsync
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="jsonObject"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostStringAsync(string httpUrl, string jsonObject, Dictionary<string, string> headers, string contentType = "application/json")
        {
            using var httpClient = GetClient();
            var content = new StringContent(jsonObject, Encoding.UTF8, contentType);
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            using var response = await httpClient.PostAsync(httpUrl, content).ConfigureAwait(false);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        /// <summary>
        /// HttpPutAsync
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="jsonObject"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<T> HttpPutAsync(string httpUrl, string jsonObject, string contentType = "application/json")
        {
            using var httpClient = GetClient();
            var content = new StringContent(jsonObject, Encoding.UTF8, contentType);
            using (var response = await httpClient.PutAsync(httpUrl, content).ConfigureAwait(false))
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result ?? "");
            }
        }

        /// <summary>
        /// HttpPutAsync
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="jsonObject"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<T> HttpPutAsync(string httpUrl, string jsonObject, Dictionary<string, string> headers, string contentType = "application/json")
        {
            using var httpClient = GetClient();
            var content = new StringContent(jsonObject, Encoding.UTF8, contentType);
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            using var response = await httpClient.PutAsync(httpUrl, content).ConfigureAwait(false);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// HttpDeleteAsync
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="deleteData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<T> HttpDeleteAsync(string httpUrl, Dictionary<string, string> deleteData , Dictionary<string, string> headers = null)
        {

            var requestMessage =  new HttpRequestMessage(HttpMethod.Delete, httpUrl) { Content = new FormUrlEncodedContent(deleteData) };

            using var httpClient = GetClient();
            httpClient.DefaultRequestHeaders.Add("charset", "UTF-8");
            if (headers.IsNotNull())
            {
                foreach (var item in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

            }

            using var response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
        /// <summary>
        /// HttpPostReadAsStringAsync
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="jsonObject"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostReadAsStringAsync(string httpUrl, string jsonObject, Dictionary<string, string> headers, string contentType = "application/json")
        {
            using var httpClient = GetClient();
            var content = new StringContent(jsonObject, Encoding.UTF8, contentType);
            foreach (var item in headers)
            {
                httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
            }

            using var response = await httpClient.PostAsync(httpUrl, content).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// HttpGet
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public static T HttpGet(string httpUrl, Dictionary<string, string> queryData = null)
        {
            var urlGet = httpUrl;

            if (queryData != null)
            {
                urlGet = urlGet.Contains("?") ? urlGet + "&" : urlGet + "?";
                foreach (var key in queryData.Keys)
                {
                    var value = queryData[key];
                    if (value != null)
                    {
                        urlGet += key + "=" + value + "&";
                    }
                }
                urlGet = urlGet.Substring(0, urlGet.Length - 1);
            }


            using (var httpClient = GetClient())
            {
                httpClient.DefaultRequestHeaders.Add("charset", "UTF-8");

                using (var response = httpClient.GetAsync(urlGet).Result)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(result);
                }

            }
        }

        /// <summary>
        /// HttpGetAsync
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="queryData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<T> HttpGetAsync(string httpUrl, Dictionary<string, string> queryData = null, Dictionary<string, string> headers = null)
        {
            var urlGet = httpUrl;

            if (queryData.IsNotNull())
            {
                urlGet = urlGet.Contains("?") ? urlGet + "&" : urlGet + "?";
                foreach (var key in queryData.Keys)
                {
                    var value = queryData[key];
                    if (value != null)
                    {
                        urlGet += key + "=" + value + "&";
                    }
                }
                urlGet = urlGet.Substring(0, urlGet.Length - 1);
            }


            using var httpClient = GetClient();
            httpClient.DefaultRequestHeaders.Add("charset", "UTF-8");
            if (headers.IsNotNull())
            {
                foreach (var item in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
                   
            }
            using (var response = await httpClient.GetAsync(urlGet).ConfigureAwait(false))
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }
    }
}
