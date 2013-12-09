using Kraken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox {
    class Program {
        static void Main(string[] args) {
            var kracken = new KrakenClient();

            var assets = kracken.GetAssetsAsync(x => { }).Result;

            var result = kracken.GetTickerAsync(x => x
                .Symbol("LTCXRP")
            ).Result;

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
