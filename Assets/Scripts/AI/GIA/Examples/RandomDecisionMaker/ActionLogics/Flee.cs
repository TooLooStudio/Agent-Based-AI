using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TooLoo.AI.Examples
{
    [CreateAssetMenu(fileName = "Flee", menuName = "TooLoo/AI/General/Action Logics/Flee")]
    public class Flee : ActionLogic
    {
        public override void Init(AIAgent agent)
        {
            Debug.Log("Fleeing!");
            agent.AIMover.SetTarget(Utils.GetFleePosition(agent.transform, agent.Sensor.Targets[0], 10f));
        }

        public override void StartAction(AIAgent agent)
        {

        }

        public override void StopAction(AIAgent agent)
        {
            Debug.Log("Stopped fleeing");
        }

        public override void UpdateAction(AIAgent agent)
        {
            agent.ActionRunner.StopAction();
        }
    }
}