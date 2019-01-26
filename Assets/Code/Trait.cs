using UnityEngine;

namespace Gameplay {
    [System.Serializable]
    public class Trait {
        public TraitData data;
        public bool like;

        public Trait(TraitData data, bool like) {
            this.data = data;
            this.like = like;
        }
    }

    [System.Serializable]
    public class TraitData {
        public string title;
        public Color debugColor = Color.white;
    }
}
