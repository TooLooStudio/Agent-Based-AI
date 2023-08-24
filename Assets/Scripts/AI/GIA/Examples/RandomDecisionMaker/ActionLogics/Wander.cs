using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TooLoo.AI.Examples
{
    [CreateAssetMenu(fileName = "Wander", menuName = "TooLoo/AI/General/Action Logics/Wander")]
    public class Wander : ActionLogic
    {
        public override void Init(AIAgent agent)
        {
            Debug.Log("Wander to random location");
            agent.AIMover.SetTarget(Utils.GetRandomNavMeshPosition(agent.transform.position, 10f));
        }

        public override void StartAction(AIAgent agent)
        {
            
        }

        public override void StopAction(AIAgent agent)
        {
            Debug.Log("Stopped wandering");
        }

        public override void UpdateAction(AIAgent agent)
        {
            agent.ActionRunner.StopAction();
        }
    }
}