namespace Turrets
{
    public class DestroyAfterSeconds : AfterSecondsEffect
    {
        protected override void OnCoroutineCompleted()
        {
            Destroy(gameObject);
        }
    }
}