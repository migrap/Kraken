using Kraken.Net.Http.Configurators;
using Kraken.Reactive.Configurators;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kraken {
    public static partial class Extensions {
        internal static string FormatWith(this string input, params object[] formatting) {
            return string.Format(input, formatting);
        }

        internal static IEnumerable<TResult> Select<TResult>(this NameValueCollection source, Func<string, string, TResult> selector) {
            foreach(string item in source.Keys) {
                yield return selector(item, source[item]);
            }
        }

        /// <summary>
        /// Uses Uri.EscapeDataString() based on recommendations on MSDN
        /// http://blogs.msdn.com/b/yangxind/archive/2006/11/09/don-t-use-net-system-uri-unescapedatastring-in-url-decoding.aspx
        /// </summary>
        internal static string UrlEncode(this string self) {
            return Uri.EscapeDataString(self);
        }

        internal static string UrlEncode(this object self) {
            return UrlEncode(self.ToString());
        }

        internal static string Join(this IEnumerable<object> source, string seperator) {
            return string.Join(seperator, source);
        }

        internal static string ToQueryString(this IDictionary<string, object> properties) {
            return properties.Select(x => "{0}={1}".FormatWith(x.Key.UrlEncode(), x.Value.UrlEncode())).Join("&");
        }

        internal static string ToQueryString(this NameValueCollection self) {
            if(self == null) {
                return string.Empty;
            }

            return self.Select((name, value) => "{0}={1}".FormatWith(name, value)).Join("&");
        }

        public static void Foreach<TSource>(this IEnumerable<TSource> source, Action<TSource> action) {
            var items = source.ToArray();
            for(int i = 0; i < items.Length; i++) {
                action(items[i]);
            }
        }

        internal static Task<T> ReadAsAsync<T>(this Task<HttpResponseMessage> message, params MediaTypeFormatter[] formatters) {
            var response = message
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<T>(formatters);
        }

        internal static Task<T> ReadAsAsync<T>(this HttpResponseMessage message, params MediaTypeFormatter[] formatters) {
            return message.Content.ReadAsAsync<T>(formatters);
        }

        internal static Task<string> ReadAsStringAsync(this Task<HttpResponseMessage> message) {
            var response = message
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync();
        }

        internal static Task<HttpResponseMessage> SendAsync(this HttpClient client, Action<IHttpRequestMessageConfigurator> configure) {
            var c = new HttpRequestMessageConfigurator();
            configure(c);

            return client.SendAsync(c.Build());
        }
 
        internal static IHttpRequestMessageConfigurator Parameters(this IHttpRequestMessageConfigurator self, Parameters value) {
            foreach(var item in value) {
                self.Parameters(item.Key, item.Value);
            }
            return self;
        }

        internal static IHttpRequestMessageConfigurator Address(this IHttpRequestMessageConfigurator self, Uri value) {
            return self.Address(value.ToString());            
        }

        internal static IObservableHttpRequestMessageConfigurator Address(this IObservableHttpRequestMessageConfigurator self, Uri value) {
            return self.Address(value.ToString());            
        }

        public static void ContinueOrCancel(this CancellationTokenSource self) {
            self.Token.ContinueOrCancel();
        }
        public static void ContinueOrCancel(this CancellationToken self) {
            if(self.IsCancellationRequested) {
                self.ThrowIfCancellationRequested();
            }
        }
    }
}
