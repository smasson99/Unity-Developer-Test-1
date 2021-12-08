using System.Collections;
using UnityEngine;

namespace Turrets
{
    public abstract class AfterSecondsEffect : MonoBehaviour
    {
        public float DelayInSeconds = 0.9f;

        public bool PlayOnAwake = false;

        private Coroutine coroutine;

        protected virtual void Awake()
        {
            if (PlayOnAwake)
            {
                Play();
            }
        }

        protected virtual IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(DelayInSeconds);
            
            OnCoroutineCompleted();
        }

        protected abstract void OnCoroutineCompleted();

        public void Play()
        {
            if (coroutine != null)
            {
                Stop();
            }
            
            coroutine = StartCoroutine(Coroutine());
        }

        public void Play(float delayInSeconds)
        {
            DelayInSeconds = delayInSeconds;
            
            Play();
        }

        public void Stop()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = null;
        }
    }
}
