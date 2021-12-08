using UnityEngine;

namespace Values
{
    public class TransformValueInitializer : MonoBehaviour
    {
        public TransformValue TransformValue;
        public Transform Target;

        private void Awake()
        {
            TransformValue.Value = Target == null ? transform : Target;
        }
    }
}