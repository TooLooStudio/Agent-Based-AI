using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    [CreateAssetMenu(fileName = "Idle", menuName = "TooLoo/AI/General/Action Logics/Idle")]
    public class Idle : ActionLogic
    {
        public override void Init(AIAgent agent)
        {
            agent.AIMover.SetTarget(agent.transform.position);
        }

        public override void StartAction(AIAgent agent)
        {
            Debug.Log("I'm idling!");
            agent.ActionProgressTracker.ResetProgress();
        }

        public override void StopAction(AIAgent agent)
        {
            Debug.Log("Stopped idling");
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