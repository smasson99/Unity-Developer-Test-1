using UnityEngine;

namespace Layers
{
    public class LayerReferences : MonoBehaviour
    {
        public static LayerReferences Singleton;
        
        public LayerMask SelectableObjectLayerMask;
        public LayerMask NavigationLayerMask;

        private void Awake()
        {
            Singleton = this;
        }
    }
}
