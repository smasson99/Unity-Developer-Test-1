using System;
using SelectableObjects;
using Units;
using UnityEngine;

namespace Turrets
{
    public class Turret : OutlinedSelectableObject
    {
        private UnitSensor unitSensor;
        private Collider collider;
        private Unit target;
        private float lastTimeSinceShoot;

        [Range(0.25f, 1f)]
        public float ShootDelayInSeconds = 1f;

        public float ProjectileForce = 3f;

        [Range(1f, 10f)]
        public float DetectionRadius = 3f;

        public float ZoneVisualMult = 1.5f;

        public GameObject ProjectilePrefab;
        public Transform ShootAnchor;
        public ZoneVisual ZoneVisual;

        private bool HasTarget => target != null;

        protected override void Awake()
        {
            base.Awake();

            collider = GetComponent<Collider>();
            unitSensor = GetComponentInChildren<UnitSensor>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            unitSensor.OnUnitFound += OnUnitFound;
            unitSensor.OnUnitLoss += OnUnitLoss;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            unitSensor.OnUnitFound -= OnUnitFound;
            unitSensor.OnUnitLoss -= OnUnitLoss;
        }

        private void OnUnitFound(Unit unit)
        {
            target = unit;
        }

        private void OnUnitLoss(Unit unit)
        {
            target = null;
        }

        public override string GetName()
        {
            return gameObject.name;
        }

        private void Update()
        {
            if (HasTarget)
                ShootIfAble();

            unitSensor.transform.localScale = Vector3.one * DetectionRadius;
            ZoneVisual.MinDistanceFromPlayer = DetectionRadius;
            ZoneVisual.MaxDistanceFromPlayer = DetectionRadius * ZoneVisualMult;
        }

        private void ShootIfAble()
        {
            if (Time.time - lastTimeSinceShoot < ShootDelayInSeconds)
                return;

            var projectile = Instantiate(ProjectilePrefab, ShootAnchor.position, ShootAnchor.rotation);
            projectile.transform.LookAt(target.transform);
            projectile.GetComponent<Rigidbody>().velocity = (projectile.transform.forward * ProjectileForce);
            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), collider, true);
            lastTimeSinceShoot = Time.time;
        }
    }
}