using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    [CreateAssetMenu(fileName = "Craft Item", menuName = "TooLoo/AI/General/Action Logics/Craft Item")]
    public class CraftItem : ActionLogic
    {
        public override void Init(AIAgent agent)
        {
            GameObject useable = Useable.GetNearest(agent.transform.position, "Forge").gameObject;
            Debug.Log($"Moving to {useable.name}");
            agent.AIMover.SetTarget(useable.transform);
        }

        public override void StartAction(AIAgent agent)
        {
            Debug.Log("Started crafting!");
            FaceTowards(agent.AIMover.Target.position, agent.transform);
            agent.ActionProgressTracker.ResetProgress();
        }

        public override void StopAction(AIAgent agent)
        {
            Debug.Log("Stopped crafting!");
            agent.ActionProgressTracker.ResetProgress();
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