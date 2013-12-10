using Kraken.Configurators;
using Kraken.Models;
using Kraken.Net.Http.Configurators;
using Kraken.Net.Http.Formatting;
using Kraken.Reactive.Configurators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Kraken {
    public class KrakenClient {         

        private HttpClient _public;
        //private HttpClient _secure;

        static KrakenClient() {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, error) => true;
        }

        public KrakenClient() {
            _public = new HttpClient(new HttpClientHandler()) {
                BaseAddress = new Uri("https://api.kraken.com/0/public/")
            };
        }

        public Task<Assets> GetAssetsAsync(Action<IAssetPairsConfigurator> configure) {
            var c = new AssetsConfigurator();
            configure(c);
            var parameters = c.Build();

            return _public.SendAsync(HttpMethod.Get, _public.BaseAddress, "AssetPairs", parameters)
                .ReadAsAsync<Assets>(new KrakenMediaTypeFormatter());
        }

        public Task<string> GetTradesAsync(Action<ITradesConfigurator> configure) {
            var c = new TradesConfigurator();
            configure(c);
            var parameters = c.Build();

            return _public.SendAsync(HttpMethod.Get, _public.BaseAddress, "Trades", parameters)
                .ReadAsStringAsync();
        }

        public Task<Ticker> GetTickerAsync(Action<ITickerConfigurator> configure) {
            var c = new TickerConfigurator();
            configure(c);
            var parameters = c.Build();

            return _public.SendAsync(HttpMethod.Get, _public.BaseAddress, "Ticker", parameters)
                .ReadAsAsync<Ticker>(new KrakenMediaTypeFormatter());
        }

        public IObservable<Ticker> GetTickerObservable(Action<IObservableTickerConfigurator> configure) {
            var c = new ObservableTickerConfigurator();
            configure(c);
            var settings = c.Build();

            return _public.Observe(x => x
                .Address(_public.BaseAddress)
                .Client(_public)
                .Every(settings.Every)
                .Method(HttpMethod.Get)
                .Parameters("pair", settings.Symbol)
                .Resource("Ticker")
            ).Select(x => x.ReadAsAsync<Ticker>(new KrakenMediaTypeFormatter()).Result);
        }
    }

    public static partial class Extensions {
        public static Task<Assets> GetAssetsAsync(this KrakenClient client) {
            return client.GetAssetsAsync(x => { });
        }

        internal static Task<HttpResponseMessage> SendAsync(this HttpClient client, HttpMethod method, Uri address, string resource, Parameters parameters) {
            return client.SendAsync(x => x
                .Resource(resource)
                .Method(method)
                .Address(address)
                .Parameters(parameters)
            );
        }

        internal static IObservable<HttpResponseMessage> Observe(this HttpClient client, Action<IObservableHttpRequestMessageConfigurator> configure) {
            var c = new ObservableHttpRequestMessageConfigurator();
            configure(c);
            return c.Build();
        }
    }
}