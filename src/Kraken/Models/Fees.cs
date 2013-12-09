using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Models {
    public class Fees : List<Fee> {
        public Fees() {
        }

        public Fees(IEnumerable<Fee> collection)
            : base(collection) {
        }
    }
}
