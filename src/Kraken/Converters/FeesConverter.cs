using Kraken.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Converters {
    internal class FeesConverter : JsonConverter {        
        public override bool CanConvert(Type objectType) {
            return typeof(Fees) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.StartArray) {
                return serializer.Deserialize<IEnumerable<double[]>>(reader)
                    .Select(x => new Fee { Volume = x[0], Percentage = x[1] / 100d })
                    .AsFees();
            }
            throw new JsonReaderException(string.Format("Unexcepted token {0}", reader.TokenType)); 
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }

    public static partial class Extensions {
        internal static Fees AsFees(this IEnumerable<Fee> self) {
            return new Fees(self);
        }
    }
}
