using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Models {
    public class Assets : List<Asset> {
        public Assets() {
        }

        public Assets(IEnumerable<Asset> collection)
            : base(collection) {
        }
    }
}
