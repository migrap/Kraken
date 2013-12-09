using Kraken.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Converters {
    internal class TickerConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(Ticker) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            while(reader.Read()) {
                if(reader.TokenType == JsonToken.StartObject) {
                    var _ = new _();
                    serializer.Populate(reader, _);

                    var ticker = new Ticker();

                    ticker.Ask = _.a.ToValue();
                    ticker.Bid = _.b.ToValue();
                    ticker.Last = _.c.ToValue();
                    ticker.Volume = _.v[0].ToDouble();
                    ticker.Vwap = _.p[0].ToDouble();
                    ticker.Trades = _.t[0];
                    ticker.Low = _.l[0].ToDouble();
                    ticker.High = _.h[0].ToDouble();
                    ticker.Open = _.o.ToDouble();

                    return ticker;
                }
            }

            throw new JsonReaderException(string.Format("Unexcepted token {0}", reader.TokenType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        private class _ {
            public string[] a { get; set; }
            public string[] b { get; set; }
            public string[] c { get; set; }
            public string[] v { get; set; }
            public string[] p { get; set; }
            public int[] t { get; set; }
            public string[] l { get; set; }
            public string[] h { get; set; }
            public string o { get; set; }
        }
    }

    public static partial class Extensions {
        internal static Value ToValue(this string[] self) {
            return new Value {
                Price = self[0].ToDouble(),
                Amount = self[1].ToDouble()
            };
        }

        internal static double ToDouble(this string self) {
            return double.Parse(self);
        }

        internal static int ToInt32(this string self) {
            return int.Parse(self);
        }
    }
}
