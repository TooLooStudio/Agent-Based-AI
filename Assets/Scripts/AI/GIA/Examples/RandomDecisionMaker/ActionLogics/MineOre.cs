using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    [CreateAssetMenu(fileName = "Mine Ore", menuName = "TooLoo/AI/General/Action Logics/Mine Ore")]
    public class MineOre : ActionLogic
    {
        public override void Init(AIAgent agent)
        {            
            GameObject resource = Gatherable.GetNearest(agent.transform.position, "OreMine").gameObject;
            Debug.Log($"Moving to {resource.name}");
            agent.AIMover.SetTarget(resource.transform);
        }

        public override void StartAction(AIAgent agent)
        {
            Debug.Log("Started mining ore!");
            FaceTowards(agent.AIMover.Target.position, agent.transform);
            agent.ActionProgressTracker.ResetProgress();
        }

        public override void StopAction(AIAgent agent)
        {
            Debug.Log("Stopped mining ore!");
            agent.ActionProgressTracker.ResetProgress();
            Gatherable.Destroy(agent.AIMover.Target.GetComponent<Gatherable>());
        }

        public override void UpdateAction(AIAgent agent)
        {
            if (agent.ActionProgressTracker.Progress < 1)
            {
                agent.ActionProgressTracker.AddActionProgress(Time.deltaTime * 0.2f);
                return;
            }
            
            agent.ActionRunner.StopAction();
        }

    }
}

