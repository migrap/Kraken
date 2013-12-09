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
        public KrakenMediaTypeFormatter() {            
        }

        public override bool CanReadType(Type type) {
            return base.CanReadType(type);
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger logger) {
            var jobject = JObject.Parse(new StreamReader(stream).ReadToEnd());
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new TickerConverter());
            var tcs = new TaskCompletionSource<object>();

            var instance = serializer.Deserialize(jobject["result"].CreateReader(), type);

            tcs.SetResult(instance);
            return tcs.Task;
        }
    }
}
