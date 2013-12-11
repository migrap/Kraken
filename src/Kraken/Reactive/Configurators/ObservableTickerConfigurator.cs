using Kraken.Configurators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Reactive.Configurators {
    public interface IObservableTickerConfigurator : ITickerConfigurator {
        IObservableTickerConfigurator Every(TimeSpan value);
    }

    internal class ObservableTickerConfigurator : IObservableTickerConfigurator {
        private ObservableTickerSettings _settings = new ObservableTickerSettings();
        public IObservableTickerConfigurator Every(TimeSpan value) {
            _settings.Every = value;
            return this;
        }

        public ITickerConfigurator Symbol(string value) {
            _settings.Symbol = value;
            return this;
        }

        public ObservableTickerSettings Build() {
            return _settings;
        }
    }

    internal class ObservableTickerSettings {
        public string Symbol { get; set; }
        public TimeSpan Every { get; set; }
    }
}
