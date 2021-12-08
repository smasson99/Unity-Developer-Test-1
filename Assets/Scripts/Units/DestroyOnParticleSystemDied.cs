using UnityEngine;

namespace Units
{
    public class DestroyOnParticleSystemDied : MonoBehaviour
    {
        private bool wasAlive;
        private float birthTime;
        private bool isDestroyed;
        
        public ParticleSystem ParticleSystem;

        protected virtual bool IsParticleSystemAlive => ParticleSystem.IsAlive();
        protected virtual bool IsParticleLoop => ParticleSystem.main.loop;
        protected virtual float DurationInSeconds => ParticleSystem.main.duration;
        private bool CanDestroy => wasAlive && (!IsParticleSystemAlive ||
                                                          Time.time - birthTime >= DurationInSeconds &&
                                                          !IsParticleLoop);

        protected virtual void Awake()
        {
            if (ParticleSystem == null)
                ParticleSystem = GetComponent<ParticleSystem>();
        }

        protected void Update()
        {
            if (ParticleSystem == null || isDestroyed)
                return;

            if (CanDestroy)
            {
                isDestroyed = true;
                Destroy(gameObject, 0.5f);
            }
            else if (IsParticleSystemAlive && !wasAlive)
            {
                wasAlive = true;
                birthTime = Time.time;
            }
        }
    }
}