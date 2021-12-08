using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            foreach (Transform children in transform)
            {
                Object.Destroy(children.gameObject);
            }
        }
    }
}
