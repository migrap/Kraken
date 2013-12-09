using Kraken.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Models {
    public class Asset {
        [JsonProperty("altname")]
        public string Name { get; set; }
        
        [JsonProperty("base")]
        public string Base { get; set; }
        
        [JsonProperty("quote")]
        public string Quote { get; set; }
        
        [JsonProperty("unit")]
        public string Lot { get; set; }
        
        [JsonProperty("pair_decimals")]
        public int PairDecimals { get; set; }
        
        [JsonProperty("lot_decimals")]
        public int LotDecimals { get; set; }

        [JsonProperty("lot_multiplier")]
        public int LotMultipler { get; set; }

        [JsonProperty("fees")]
        [JsonConverter(typeof(FeesConverter))]
        public Fees Fees { get; set; }

        [JsonProperty("fee_volume_currency")]
        public string FeesVolumeCurrency { get; set; }

        [JsonProperty("margin_call")]
        public double MarginCall { get; set; }

        [JsonProperty("margin_stop")]
        public double MarginStop { get; set; }
    }
}
