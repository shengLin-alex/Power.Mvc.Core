using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Power.Mvc.Helper.Extensions
{
    /// <summary>
    /// HttpClient 擴充方法
    /// </summary>
    public static class HttpClientExtension
    {
        /// <summary>
        /// 取得 Http Get 要求的回應，無須驗證 Token
        /// </summary>
        /// <typeparam name="TResponse">API 回應型別</typeparam>
        /// <param name="httpClient">提供基底類別，用來傳送 HTTP 要求，以及從 URI 所識別的資源接收 HTTP 回應。</param>
        /// <param name="requestUrl">Http 要求的 Url</param>
        /// <returns>強型別 API 回應</returns>
        public static Task<TResponse> GetMethodResponseAsync<TResponse>(this HttpClient httpClient, string requestUrl)
        {
            return httpClient.GetApiResponseAsyncImpl<TResponse>(requestUrl, HttpMethodType.Get, null, false, null, null);
        }

        /// <summary>
        /// 取得 Http Get 要求的回應，須帶入驗證 Token
        /// </summary>
        /// <typeparam name="TResponse">API 回應型別</typeparam>
        /// <param name="httpClient">提供基底類別，用來傳送 HTTP 要求，以及從 URI 所識別的資源接收 HTTP 回應。</param>
        /// <param name="requestUrl">Http 要求的 Url</param>
        /// <param name="authorizeToken">驗證 Token</param>
        /// <param name="onTokenUnauthorize">重取 Token 的委派</param>
        /// <returns>強型別 API 回應</returns>
        public static Task<TResponse> GetMethodResponseAsync<TResponse>(this HttpClient httpClient, string requestUrl, string authorizeToken, Func<string> onTokenUnauthorize)
        {
            return httpClient.GetApiResponseAsyncImpl<TResponse>(requestUrl, HttpMethodType.Get, null, true, authorizeToken, onTokenUnauthorize);
        }

        /// <summary>
        /// 取得 Http Post 要求的回應，無須驗證 Token
        /// </summary>
        /// <typeparam name="TResponse">API 回應型別</typeparam>
        /// <param name="httpClient">提供基底類別，用來傳送 HTTP 要求，以及從 URI 所識別的資源接收 HTTP 回應。</param>
        /// <param name="requestUrl">Http 要求的 Url</param>
        /// <param name="requestBody">Post request body</param>
        /// <returns>強型別 API 回應</returns>
        public static Task<TResponse> PostMethodResponseAsync<TResponse>(this HttpClient httpClient, string requestUrl, object requestBody)
        {
            return httpClient.GetApiResponseAsyncImpl<TResponse>(requestUrl, HttpMethodType.Post, requestBody, false, null, null);
        }

        /// <summary>
        /// 取得 Http Post 要求的回應，須帶入驗證 Token
        /// </summary>
        /// <typeparam name="TResponse">API 回應型別</typeparam>
        /// <param name="httpClient">提供基底類別，用來傳送 HTTP 要求，以及從 URI 所識別的資源接收 HTTP 回應。</param>
        /// <param name="requestUrl">Http 要求的 Url</param>
        /// <param name="requestBody">Post request body</param>
        /// <param name="authorizeToken">驗證 Token</param>
        /// <param name="onTokenUnauthorize">重取 Token 的委派</param>
        /// <returns>強型別 API 回應</returns>
        public static Task<TResponse> PostMethodResponseAsync<TResponse>(this HttpClient httpClient, string requestUrl, object requestBody, string authorizeToken, Func<string> onTokenUnauthorize)
        {
            return httpClient.GetApiResponseAsyncImpl<TResponse>(requestUrl, HttpMethodType.Post, requestBody, true, authorizeToken, onTokenUnauthorize);
        }

        /// <summary>
        /// 取得 Http Delete 要求的回應，無須驗證 Token
        /// </summary>
        /// <typeparam name="TResponse">API 回應型別</typeparam>
        /// <param name="httpClient">提供基底類別，用來傳送 HTTP 要求，以及從 URI 所識別的資源接收 HTTP 回應。</param>
        /// <param name="requestUrl">Http 要求的 Url</param>
        /// <returns>強型別 API 回應</returns>
        public static Task<TResponse> DeleteMethodResponseAsync<TResponse>(this HttpClient httpClient, string requestUrl)
        {
            return httpClient.GetApiResponseAsyncImpl<TResponse>(requestUrl, HttpMethodType.Delete, null, false, null, null);
        }

        /// <summary>
        /// 取得 Http Delete 要求的回應，須帶入驗證 Token
        /// </summary>
        /// <typeparam name="TResponse">API 回應型別</typeparam>
        /// <param name="httpClient">提供基底類別，用來傳送 HTTP 要求，以及從 URI 所識別的資源接收 HTTP 回應。</param>
        /// <param name="requestUrl">Http 要求的 Url</param>
        /// <param name="authorizeToken">驗證 Token</param>
        /// <param name="onTokenUnauthorize">重取 Token 的委派</param>
        /// <returns>強型別 API 回應</returns>
        public static Task<TResponse> DeleteMethodResponseAsync<TResponse>(this HttpClient httpClient, string requestUrl, string authorizeToken, Func<string> onTokenUnauthorize)
        {
            return httpClient.GetApiResponseAsyncImpl<TResponse>(requestUrl, HttpMethodType.Delete, null, true, authorizeToken, onTokenUnauthorize);
        }

        /// <summary>
        /// 取得指定 Http method 要求的回應，無須驗證 Token
        /// </summary>
        /// <typeparam name="TResponse">API 回應型別</typeparam>
        /// <param name="httpClient">提供基底類別，用來傳送 HTTP 要求，以及從 URI 所識別的資源接收 HTTP 回應。</param>
        /// <param name="requestUrl">Http 要求的 Url</param>
        /// <param name="httpMethod">指定的 Http 要求方法</param>
        /// <param name="requestBody">Post request body</param>
        /// <returns>強型別 API 回應</returns>
        public static Task<TResponse> ApiResponseAsync<TResponse>(this HttpClient httpClient, string requestUrl, HttpMethodType httpMethod, object requestBody = null)
        {
            return httpClient.GetApiResponseAsyncImpl<TResponse>(requestUrl, httpMethod, requestBody, false, null, null);
        }

        /// <summary>
        /// 取得指定 Http method 要求的回應，須帶入驗證 Token
        /// </summary>
        /// <typeparam name="TResponse">API 回應型別</typeparam>
        /// <param name="httpClient">提供基底類別，用來傳送 HTTP 要求，以及從 URI 所識別的資源接收 HTTP 回應。</param>
        /// <param name="requestUrl">Http 要求的 Url</param>
        /// <param name="httpMethod">指定的 Http 要求方法</param>
        /// <param name="authorizeToken">驗證 Token</param>
        /// <param name="onTokenUnauthorize">重取 Token 的委派</param>
        /// <param name="requestBody">Post request body</param>
        /// <returns>強型別 API 回應</returns>
        public static Task<TResponse> ApiResponseAsync<TResponse>(this HttpClient httpClient, string requestUrl, HttpMethodType httpMethod, string authorizeToken, Func<string> onTokenUnauthorize, object requestBody = null)
        {
            return httpClient.GetApiResponseAsyncImpl<TResponse>(requestUrl, httpMethod, requestBody, true, authorizeToken, onTokenUnauthorize);
        }

        /// <summary>
        /// 由 Url 取得指定型別的 API 回應
        /// </summary>
        /// <typeparam name="TResponse">API 回應的型別</typeparam>
        /// <param name="httpClient">提供基底類別，用來傳送 HTTP 要求，以及從 URI 所識別的資源接收 HTTP 回應。</param>
        /// <param name="requestUrl">API 要求的 URL</param>
        /// <param name="httpMethodType">HTTP 方法，預設為 GET 方法</param>
        /// <param name="isRequireToken">是否要求驗證 TOKEN</param>
        /// <param name="requestBody">API Request body</param>
        /// <param name="authorizeToken">驗證 TOKEN</param>
        /// <param name="onTokenUnauthorize">驗證TOKEN 失效時的重取委派</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <returns>強型別 API 回應</returns>
        private static Task<TResponse> GetApiResponseAsyncImpl<TResponse>(this HttpClient httpClient, string requestUrl, HttpMethodType httpMethodType, object requestBody, bool isRequireToken, object authorizeToken, Func<object> onTokenUnauthorize)
        {
            while (true)
            {
                // API 需要 authorize token
                if (isRequireToken)
                {
                    if (authorizeToken == null)
                    {
                        throw new ArgumentNullException($"This request require token, but {nameof(authorizeToken)} is null!");
                    }

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", (string)authorizeToken);
                }

                // 判斷要求為 post 或 get
                Task<HttpResponseMessage> response;
                switch (httpMethodType)
                {
                    case HttpMethodType.Get:
                        response = httpClient.GetAsync(requestUrl);
                        break;

                    case HttpMethodType.Post:
                        if (requestBody == null)
                        {
                            throw new ArgumentNullException($"Using http post method, but { nameof(requestBody) } is null!");
                        }

                        string requestJson = requestBody.ToJson();
                        StringContent stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                        response = httpClient.PostAsync(requestUrl, stringContent);
                        break;

                    case HttpMethodType.Delete:
                        response = httpClient.DeleteAsync(requestUrl);
                        break;

                    default:
                        response = null;
                        break;
                }

                // 檢查 http response 是否為 null
                if (response is null)
                {
                    throw new HttpRequestException("Cannot get http response!");
                }

                switch (response.Result.StatusCode)
                {
                    // Token 驗證錯誤
                    case HttpStatusCode.Unauthorized when onTokenUnauthorize == null:
                        throw new ArgumentNullException($"HttpStatus = {nameof(HttpStatusCode.Unauthorized)}, but not implement {nameof(onTokenUnauthorize)}");

                    case HttpStatusCode.Unauthorized:

                        // 重取驗證 Token
                        authorizeToken = onTokenUnauthorize.Invoke();

                        // 重試
                        continue;
                }

                // API 呼叫錯誤(非預期)
                if (!response.Result.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"StatusCode: {response.Result.StatusCode}，ReasonPhrase: {response.Result.ReasonPhrase}");
                }

                Task<TResponse> executedResult = Task.Run(() => response.Result.Content.ReadAsStringAsync().Result.ToTypedObject<TResponse>());

                return executedResult;
            }
        }
    }
}