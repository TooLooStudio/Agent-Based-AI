using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TooLoo.AI;
using UnityEngine;
using UnityEngine.AI;

namespace TooLoo
{
    public static class Utils
    {
        /// <summary>
        /// Return a random position on a flat plane navmesh
        /// </summary>
        /// <param name="sourcePos"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        public static Vector3 GetRandomNavMeshPosition(Vector3 sourcePos, float maxDistance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxDistance + sourcePos;
            Vector3 finalPosition = sourcePos;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 4f, NavMesh.AllAreas))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }

        public static Vector3 GetRandomCirclePosition(Vector3 sourcePos, float radius)
        {
            Vector3 position = new();

            position = new Vector3(
                sourcePos.x + RandomNegOrPos() * (radius * Mathf.Cos(Random.Range(0, 2) * Mathf.PI)),
                0,
                sourcePos.z + RandomNegOrPos() * (radius * Mathf.Sin(Random.Range(0, 2) * Mathf.PI)));

            return position;
        }

        public static int RandomNegOrPos()
        {
            return (Random.Range(0, 2) * 2 - 1);
        }

        public static bool IsInFOV(Transform source, Transform target, float minAngle, float maxAngle)
        {
            Vector3 targetDirection = target.position - source.position;
            float angle = Vector3.Angle(targetDirection, source.forward);
            if (angle > minAngle && angle < maxAngle)
            {
                return true;
            }
            return false;
        }

        public static Vector3 GetFleePosition(Transform source, Transform target, float fleeDistance)
        {
            Vector3 fleePosition = Vector3.zero;
            Vector3 targetDirection = source.position - target.position;
            targetDirection.Normalize();
            fleePosition = source.position + targetDirection * fleeDistance;
            fleePosition = new Vector3(fleePosition.x, 0, fleePosition.z);
            return fleePosition;
        }
    }
}