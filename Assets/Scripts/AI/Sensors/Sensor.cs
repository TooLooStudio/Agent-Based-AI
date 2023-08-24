using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TooLoo.AI.Examples
{
    public class Sensor : MonoBehaviour
    {
        [Range(5f, 15f)]
        [SerializeField] private float radius = 15f;
        [SerializeField] private float autoDetectedRange = 5f;
        [SerializeField] private float minDetectionAngle = -50f;
        [SerializeField] private float maxDetectionAngle = 50f;
        [SerializeField] private LayerMask enemyLayerMask;

        private readonly List<Transform> targets = new();
        private AIAgent agent;

        public UnityAction OnDetectedTargets;

        public float Radius => radius;
        public List<Transform> Targets => targets;

        private void OnEnable()
        {
            InvokeRepeating("ScanEnvironment", 0, 0.5f);
        }


        public void Init(AIAgent agent)
        {
            this.agent = agent;
        }

        public void ScanEnvironment()
        {
            DetectTargets();
        }

        private void DetectTargets()
        {
            targets.Clear();
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);

            if (colliders.Length == 0) return;

            for (int i = 0; i < colliders.Length; i++)
            {
                Critter critter = colliders[i].transform.GetComponent<Critter>();
                if (critter != null && critter != this.agent)
                {
                    if (Utils.IsInFOV(transform, critter.transform, minDetectionAngle, maxDetectionAngle) 
                        || Vector3.Distance(transform.position, critter.transform.position) <= autoDetectedRange)
                    {
                        targets.Add(critter.transform);
                    }
                }
            }

            if (targets.Count > 0) 
            {
                OnDetectedTargets?.Invoke();
            }
        }
    }
}