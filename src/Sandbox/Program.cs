using Kraken;
using Kraken.Models;
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


            kracken.GetTickerObservable(x => x
                .Every(TimeSpan.FromSeconds(5))
                .Symbol("XBTEUR")
            ).Subscribe(new _<Ticker>());

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }

    class _<T> : IObserver<T> {

        public void OnCompleted() {
            
        }

        public void OnError(Exception error) {
            
        }

        public void OnNext(T value) {
            Console.WriteLine(value);
        }
    }
    
}
