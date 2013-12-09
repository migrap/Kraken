using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Configurators {
    public interface IAssetPairsConfigurator {
        IAssetPairsConfigurator Symbol(string value);
    }
    internal class AssetsConfigurator : IAssetPairsConfigurator {
        private List<string> _symbols = new List<string>();
        public IAssetPairsConfigurator Symbol(string value) {
            _symbols.Add(value);
            return this;
        }

        public Parameters Build() {
            var parameters = new Parameters();
            if(_symbols.Count != 0) {
                parameters.Add("pair", _symbols.Join(","));
            }
            return parameters;
        }
    }
}
