using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TooLoo.AI.Examples
{
    public class Critter : MonoBehaviour
    {
        public Transform patrolPoint; 
        public float radius = 10f; 
        public float timeBetweenMoves = 1f; 

        private NavMeshAgent navigator;
        private float timeSinceLastMove;

        private void Start()
        {
            navigator = GetComponent<NavMeshAgent>();
            timeSinceLastMove = timeBetweenMoves;
        }

        private void Update()
        {
            if (!navigator.pathPending 
                && navigator.remainingDistance <= navigator.stoppingDistance 
                && (!navigator.hasPath || navigator.velocity.sqrMagnitude == 0f))
            {
                timeSinceLastMove += Time.deltaTime;

                if (timeSinceLastMove >= timeBetweenMoves)
                {
                    MoveToRandomPoint();
                    timeSinceLastMove = 0f;
                }
            }
        }

        private void MoveToRandomPoint()
        {
            Vector2 randomDirection = Random.insideUnitCircle * radius;
            Vector3 destination = patrolPoint.position + new Vector3(randomDirection.x, 0f, randomDirection.y);

            if (NavMesh.SamplePosition(destination, out NavMeshHit hit, radius, NavMesh.AllAreas))
            {
                navigator.SetDestination(hit.position);
            }
        }
    }
}