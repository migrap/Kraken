using Kraken.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Net.Http.Formatting {
    internal class KrakenMediaTypeFormatter : JsonMediaTypeFormatter {
        private static readonly Lazy<JsonSerializer> _serializer = new Lazy<JsonSerializer>(() => {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new TickerConverter());
            serializer.Converters.Add(new AssetsConverters());
            serializer.Converters.Add(new FeesConverter());
            return serializer;
        });

        public static JsonSerializer Serializer {
            get { return _serializer.Value; }
        }

        public KrakenMediaTypeFormatter() {            
        }

        public override bool CanReadType(Type type) {
            return base.CanReadType(type);
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger logger) {
            var jobject = JObject.Parse(new StreamReader(stream).ReadToEnd());
            var serializer = Serializer;
            
            var tcs = new TaskCompletionSource<object>();

            var instance = serializer.Deserialize(jobject["result"].CreateReader(), type);

            tcs.SetResult(instance);
            return tcs.Task;
        }
    }
}
