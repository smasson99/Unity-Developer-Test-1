using EPOOutline;

namespace SelectableObjects
{
    public abstract class OutlinedSelectableObject : SelectableObject
    {
        private Outlinable outline;

        protected virtual void Awake()
        {
            outline = GetComponentInChildren<Outlinable>();
            
            outline.enabled = false;
        }

        protected override void OnSelected()
        {
            outline.enabled = true;
        }

        protected override void OnDeselected()
        {
            outline.enabled = false;
        }
    }
}
