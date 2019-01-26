using UnityEngine;
using System.Collections.Generic;

namespace Gameplay {
    // Class for actual ingame instance of a tenant
    public class Tenant: MonoBehaviour {
        public TenantData data;

        // Kick the tenant out of the house
        public void Kick() {
            // TODO: animate and then destroy
            Destroy(gameObject);
        }
    }

    // Class for tenant data, e.g. for not-yet-rendered tenant proposals
    [System.Serializable]
    public class TenantData {
        // Note: class is hardcoded for only a single trait
        public List<Trait> traits;

        public TenantData(Trait trait) {
            traits = new List<Trait>();
            traits.Add(trait);
        }

        public bool Conflicts(TenantData other) {
            return traits[0].data.title == other.traits[0].data.title
                && traits[0].like != other.traits[0].like;
        }

        public override string ToString() {
            var trait = traits[0];
            return (trait.like ? "Likes" : "Hates")
                + " " + trait.data.title;
        }
    }
}
