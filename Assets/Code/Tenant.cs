using UnityEngine;
using System.Collections.Generic;

namespace Gameplay {
    public class Tenant: MonoBehaviour {
        public TenantData data;
    }

    [System.Serializable]
    public class TenantData {
        public List<Trait> traits;

        // Currently hardcoded for only a single trait
        public TenantData(Trait trait) {
            traits = new List<Trait>();
            traits.Add(trait);
        }
    }
}
