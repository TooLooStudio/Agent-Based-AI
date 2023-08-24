using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    [CreateAssetMenu(fileName = "Attend Fair", menuName = "TooLoo/AI/General/Action Logics/Attend Fair")]
    public class AttendFair : ActionLogic
    {
        public override void Init(AIAgent agent)
        {
            GameObject useable = Useable.GetNearest(agent.transform.position, "Fair").gameObject;
            Debug.Log($"Moving to {useable.name}");
            agent.AIMover.SetTarget(useable.transform);
        }

        public override void StartAction(AIAgent agent)
        {
            Debug.Log("Spending time at fair!");
            FaceTowards(agent.AIMover.Target.position, agent.transform);
            agent.ActionProgressTracker.ResetProgress();
        }

        public override void StopAction(AIAgent agent)
        {
            Debug.Log("Leaving fair!");
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