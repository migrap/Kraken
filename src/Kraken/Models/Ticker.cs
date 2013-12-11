using Kraken.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Models {
    public class Ticker {

        public Value Bid { get; set; }        
        public Value Ask { get; set; }        
        public Value Last { get; set; }

        public double Volume { get; set; }

        public double Vwap { get; set; }

        public int Trades { get; set; }

        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public override string ToString() {
            return (new { Bid, Ask, Last, Open, High, Low, Volume, Vwap, Trades }).ToString();
        }

        public struct Value {
            public double Price { get; set; }
            public double Amount { get; set; }

            public override string ToString() {
                return (new { Price, Amount }).ToString();
            }
        }
    }
}
