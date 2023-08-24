using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    [CreateAssetMenu(fileName = "Chop Tree", menuName = "TooLoo/AI/General/Action Logics/Chop Tree")]
    public class ChopTree : ActionLogic
    {
        public override void Init(AIAgent agent)
        {
            GameObject resource = Gatherable.GetNearest(agent.transform.position, "Tree").gameObject;
            Debug.Log($"Moving to {resource.name}");
            agent.AIMover.SetTarget(resource.transform);
        }

        public override void StartAction(AIAgent agent)
        {
            Debug.Log("Started chopping tree!");
            FaceTowards(agent.AIMover.Target.position, agent.transform);
            agent.ActionProgressTracker.ResetProgress();
        }

        public override void StopAction(AIAgent agent)
        {
            Debug.Log("Stopped chopping tree!");
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

