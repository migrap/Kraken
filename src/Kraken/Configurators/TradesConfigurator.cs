using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Configurators {
    public interface ITradesConfigurator {
        ITradesConfigurator Symbol(string value);
    }
    internal class TradesConfigurator : ITradesConfigurator {
        private List<string> _symbols = new List<string>();
        public ITradesConfigurator Symbol(string value) {
            _symbols.Add(value);
            return this;
        }

        public Parameters Build() {
            var parameters = new Parameters();
            parameters.Add("pair", _symbols.Join(","));
            return parameters;
        }
    }
}
