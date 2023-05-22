#load "Logging/Logger.csx"

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


    public class RestClient
    {

        /// <summary>
        /// The <see cref="HttpClient"/> used internally by the RestClient.
        /// </summary>
        /// <returns>The <see cref="HttpClient"/>.</returns>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// Default constructor. Takes a mandatory base address, and creates a HttpClient with this address.
        /// </summary>
        /// <param name="baseAddress">The base URL.</param>
        /// <param name="timeoutInSeconds">HTTP timeout in seconds. Default is "60".</param>
        public RestClient(string baseAddress, long timeoutInSeconds = 60)
        {
            HttpClient = new HttpClient { BaseAddress = new Uri(baseAddress), Timeout = TimeSpan.FromSeconds(timeoutInSeconds) };
        }

        /// <summary>
        /// Takes a mandatory base address and a <see cref="HttpClientHandler"/>, and creates a HttpClient with this address.
        /// </summary>
        /// <param name="httpClientHandler">A <see cref="HttpClientHandler"/>.</param>
        /// <param name="baseAddress">The base URL.</param>
        /// <param name="timeoutInSeconds">HTTP timeout in seconds. Default is "60".</param>
        public RestClient(HttpClientHandler httpClientHandler, string baseAddress, long timeoutInSeconds = 60)
        {
            HttpClient = new HttpClient(httpClientHandler) { BaseAddress = new Uri(baseAddress), Timeout = TimeSpan.FromSeconds(timeoutInSeconds) };
        }

        /// <summary>
        /// Gets the specified URL. Awaitable.
        /// </summary>
        /// <param name="relativeUrl">URL for the desired resource, relative to the base URL.</param>
        /// <param name="accept">Media type for the return data. Default is "application/json".</param>
        /// <param name="parameters">Optional parameters to be added to the URL.</param>
        /// <param name="additionalHeaders">Additional optional headers to be added to the request.</param>
        /// <param name="noLog">Set to 'true' to disable error logging.</param>
        /// <returns>An awaitable <see cref="Task"/> with the <see cref="HttpResponseMessage"/> result.</returns>
        public async Task<HttpResponseMessage> GetAsync(
            string relativeUrl,
            string accept = "application/json",
            IDictionary<string, string> parameters = null,
            IDictionary<string, string> additionalHeaders = null,
            bool noLog = false)
        {
            var requestMessage = CreateHttpRequestMessage(
                HttpMethod.Get,
                relativeUrl,
                accept: accept,
                parameters: parameters,
                additionalHeaders: additionalHeaders);

            Logger.LogDebug($"GET from '{HttpClient.BaseAddress.OriginalString}{requestMessage.RequestUri}'");

            try
            {
                var httpResponseMessage = await HttpClient.SendAsync(requestMessage);
                if (!httpResponseMessage.IsSuccessStatusCode && !noLog)
                {
                    Logger.LogError($"{httpResponseMessage.StatusCode} : GET to '{HttpClient.BaseAddress.OriginalString}{relativeUrl}' failed: {httpResponseMessage.ReasonPhrase}", false);
                }

                return httpResponseMessage;
            }
            catch (TaskCanceledException)
            {
                Logger.LogError($"GET from '{HttpClient.BaseAddress.OriginalString}{requestMessage.RequestUri}' failed", true);
                throw;
            }
        }

        /// <summary>
        /// Posts to the specified URL. Awaitable.
        /// </summary>
        /// <param name="relativeUrl">URL for the desired resource, relative to the base URL.</param>
        /// <param name="content">The data to post.</param>
        /// <param name="contentType">Media type for the post data. Default is "application/json".</param>
        /// <param name="accept">Media type for the return data. Default is "application/json".</param>
        /// <param name="parameters">Optional parameters to be added to the URL.</param>
        /// <param name="additionalHeaders">Additional optional headers to be added to the request.</param>
        /// <returns>An awaitable <see cref="Task"/> with the <see cref="HttpResponseMessage"/> result.</returns>
        public async Task<HttpResponseMessage> PostAsync(
            string relativeUrl,
            string content,
            string contentType = "application/json",
            string accept = "application/json",
            IDictionary<string, string> parameters = null,
            IDictionary<string, string> additionalHeaders = null)
        {
            var requestMessage = CreateHttpRequestMessage(
                HttpMethod.Post,
                relativeUrl,
                content: content,
                contentType: contentType,
                accept: accept,
                parameters: parameters,
                additionalHeaders: additionalHeaders);

            Logger.LogDebug($"POST to '{HttpClient.BaseAddress.OriginalString}{requestMessage.RequestUri}'");

            try
            {
                var httpResponseMessage = await HttpClient.SendAsync(requestMessage);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    Logger.LogError($"{httpResponseMessage.StatusCode} : POST to '{HttpClient.BaseAddress.OriginalString}{relativeUrl}' failed: {httpResponseMessage.ReasonPhrase}", false);
                }

                return httpResponseMessage;
            }
            catch (TaskCanceledException)
            {
                Logger.LogError($"POST to '{HttpClient.BaseAddress.OriginalString}{requestMessage.RequestUri}' failed", true);
                throw;
            }
        }

        /// <summary>
        /// Puts to the specified URL. Awaitable.
        /// </summary>
        /// <param name="relativeUrl">URL for the desired resource, relative to the base URL.</param>
        /// <param name="content">The data to put.</param>
        /// <param name="contentType">Media type for the post data. Default is "application/json".</param>
        /// <param name="accept">Media type for the return data. Default is "application/json".</param>
        /// <param name="parameters">Optional parameters to be added to the URL.</param>
        /// <param name="additionalHeaders">Additional optional headers to be added to the request.</param>
        /// <returns>An awaitable <see cref="Task"/> with the <see cref="HttpResponseMessage"/> result.</returns>
        public async Task<HttpResponseMessage> PutAsync(
            string relativeUrl,
            string content,
            string contentType = "application/json",
            string accept = "application/json",
            IDictionary<string, string> parameters = null,
            IDictionary<string, string> additionalHeaders = null)
        {
            var requestMessage = CreateHttpRequestMessage(
                HttpMethod.Put,
                relativeUrl,
                content: content,
                contentType: contentType,
                accept: accept,
                parameters: parameters,
                additionalHeaders: additionalHeaders);

            Logger.LogDebug($"PUT to '{HttpClient.BaseAddress.OriginalString}{requestMessage.RequestUri}'");

            try
            {
                var httpResponseMessage = await HttpClient.SendAsync(requestMessage);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    Logger.LogError($"{httpResponseMessage.StatusCode} : PUT to '{HttpClient.BaseAddress.OriginalString}{relativeUrl}' failed: {httpResponseMessage.ReasonPhrase}", false);
                }

                return httpResponseMessage;
            }
            catch (TaskCanceledException)
            {
                Logger.LogError($"PUT to '{HttpClient.BaseAddress.OriginalString}{requestMessage.RequestUri}' failed", true);
                throw;
            }
        }

        /// <summary>
        /// Gets the specified URL. Awaitable.
        /// </summary>
        /// <param name="relativeUrl">URL for the desired resource, relative to the base URL.</param>
        /// <param name="accept">Media type for the return data. Default is "application/json".</param>
        /// <param name="parameters">Optional parameters to be added to the URL.</param>
        /// <param name="additionalHeaders">Additional optional headers to be added to the request.</param>
        /// <returns>An awaitable <see cref="Task"/> with the <see cref="HttpResponseMessage"/> result.</returns>
        public async Task<HttpResponseMessage> DeleteAsync(
            string relativeUrl,
            string accept = "application/json",
            IDictionary<string, string> parameters = null,
            IDictionary<string, string> additionalHeaders = null)
        {
            var requestMessage = CreateHttpRequestMessage(
                HttpMethod.Delete,
                relativeUrl,
                accept: accept,
                parameters: parameters,
                additionalHeaders: additionalHeaders);

            Logger.LogDebug($"DELETE to '{HttpClient.BaseAddress.OriginalString}{requestMessage.RequestUri}'");

            try
            {
                var httpResponseMessage = await HttpClient.SendAsync(requestMessage);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    Logger.LogError($"{httpResponseMessage.StatusCode} : DELETE to '{HttpClient.BaseAddress.OriginalString}{relativeUrl}' failed: {httpResponseMessage.ReasonPhrase}", false);
                }

                return httpResponseMessage;
            }
            catch (TaskCanceledException)
            {
                Logger.LogError($"DELETE to '{HttpClient.BaseAddress.OriginalString}{requestMessage.RequestUri}' failed", true);
                throw;
            }
        }

        /// <summary>
        /// Creates a <see cref="HttpRequestMessage"/> based on the specified parameters.
        /// </summary>
        /// <param name="httpMethod">The <see cref="HttpMethod"/> for the request.</param>
        /// <param name="url">The URL for the request.</param>
        /// <param name="content">Optional string-formatted content.</param>
        /// <param name="contentType">The MediaType for the content. Default "application/json".</param>
        /// <param name="accept">The MediaType to accept. Default "application/json".</param>
        /// <param name="parameters">Additional optional parameters to be added to the URL.</param>
        /// <param name="additionalHeaders">Additional optional headers to be added to the request.</param>
        /// <returns>The <see cref="HttpRequestMessage"/>.</returns>
        public static HttpRequestMessage CreateHttpRequestMessage(
            HttpMethod httpMethod,
            string url,
            string content = null,
            string contentType = "application/json",
            string accept = "application/json",
            IDictionary<string, string> parameters = null,
            IDictionary<string, string> additionalHeaders = null)
        {
            var request = new HttpRequestMessage(httpMethod, CreateUri(url, parameters));

            if (content != null)
            {
                request.Content = new StringContent(content);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            }

            if (!string.IsNullOrWhiteSpace(accept))
            {
                request.Headers.Add("Accept", accept);
            }

            if (additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            return request;
        }

        private static Uri CreateUri(string url, IDictionary<string, string> parameters)
        {
            url = url + CreateParameterString(parameters);
            var uriKind = url.Contains("http://") || url.Contains("https://") ? UriKind.Absolute : UriKind.Relative;
            return new Uri(url, uriKind);
        }

        /// <summary>
        /// Creates a parameter string from a dictionary of parameters.
        /// </summary>
        /// <param name="parameters">The dictionary of parameters.</param>
        /// <returns>A HTTP query string".</returns>
        public static string CreateParameterString(IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                return "";
            }

            var parameterList = new List<string>();
            foreach (var parameter in parameters)
            {
                parameterList.Add(parameter.Key + "=" + parameter.Value);
            }

            return "?" + string.Join("&", parameterList);
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
