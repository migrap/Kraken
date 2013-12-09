using Kraken.Configurators;
using Kraken.Models;
using Kraken.Net.Http.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kraken {
    public class KrakenClient {         

        private HttpClient _public;
        private HttpClient _secure;

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

            var query = parameters.ToQueryString();
            var uri = "{0}AssetPairs{1}{2}".FormatWith(_public.BaseAddress, (parameters.Count == 0) ? "" : "?", query);

            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            };

            return _public.SendAsync(request)
                .ReadAsAsync<Assets>(new KrakenMediaTypeFormatter());
        }

        public Task<string> GetTradesAsync(Action<ITradesConfigurator> configure) {
            var c = new TradesConfigurator();
            configure(c);

            var parameters = c.Build();

            var query = parameters.ToQueryString();

            var uri = "{0}Trades?{1}".FormatWith(_public.BaseAddress, query);            

            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            };

            return _public.SendAsync(request)                
                .ReadAsStringAsync();
        }

        public Task<Ticker> GetTickerAsync(Action<ITickerConfigurator> configure) {
            var c = new TickerConfigurator();
            configure(c);

            var parameters = c.Build();

            var query = parameters.ToQueryString();

            var uri = "{0}Ticker?{1}".FormatWith(_public.BaseAddress, query);

            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            };

            return _public.SendAsync(request)
                .ReadAsAsync<Ticker>(new KrakenMediaTypeFormatter());
        }
    }
}