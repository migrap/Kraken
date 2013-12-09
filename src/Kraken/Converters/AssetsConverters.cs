using Kraken.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Converters {
    internal class AssetsConverters : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(Assets) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.StartObject) {
                return serializer.Deserialize<Dictionary<string, object>>(reader)
                    .Values
                    .Cast<JObject>()
                    .Select(x => x.ToObject<Asset>(serializer))
                    .AsAssets();
            }
            
            throw new JsonReaderException(string.Format("Unexcepted token {0}", reader.TokenType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }

    public static partial class Extensions {
        internal static Assets AsAssets(this IEnumerable<Asset> self) {
            return new Assets(self);
        }
    }
}
