using UnityEngine;
using UnityEngine.AI;
using System;

namespace TooLoo.AI
{
    public class SimpleAIMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navigator;
        [SerializeField] private GameObject destinationMarker;

        private Transform target;
        private bool isMoving;

        public Transform Target => target;

        public event Action OnReachedTarget;

        void Awake()
        {
            navigator = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            destinationMarker.transform.parent = null;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;

            destinationMarker.transform.position = Utils.GetRandomCirclePosition(target.position, 3f);
            MoveToDestinationMarker();
        }

        public void SetTarget(Vector3 position)
        {
            destinationMarker.transform.position = position;
            MoveToDestinationMarker();
        }

        private void MoveToDestinationMarker()
        {
            float distance = Vector3.Distance(transform.position, destinationMarker.transform.position);
            Debug.Log($"Agent Position: {transform.position} | Target Position: {destinationMarker.transform.position} | Distance from target: {distance}");

            if (distance <= 1f)
            {
                Debug.Log("Already at target");
                OnReachedTarget?.Invoke();
                return;
            }

            navigator.SetDestination(destinationMarker.transform.position);
        }

        public void Stop()
        {
            navigator.ResetPath();
            navigator.velocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == destinationMarker.transform)
            {
                Stop();
                OnReachedTarget?.Invoke();
            }
        }
    }
}