using UnityEngine;
using System.Collections.Generic;

namespace Gameplay {
    public class Tenant: MonoBehaviour {
        public TenantData data;
    }

    [System.Serializable]
    public class TenantData {
        // Note: class is hardcoded for only a single trait
        public List<Trait> traits;

        public TenantData(Trait trait) {
            traits = new List<Trait>();
            traits.Add(trait);
        }

        public override string ToString() {
            var trait = traits[0];
            return (trait.like ? "Likes" : "Hates")
                + " " + trait.data.title;
        }
    }
}
