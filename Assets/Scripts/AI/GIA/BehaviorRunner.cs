using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace TooLoo.AI
{
    public class BehaviorRunner : MonoBehaviour
    {
        private ActionRunner actionRunner;
        private Queue<string> currentPlan;

        public bool HasPlan => currentPlan != null;
        
        public void Init(ActionRunner actionRunner)
        {
            this.actionRunner = actionRunner;
            this.actionRunner.OnFinishedAction += OnFinishedAction;
        }

        private void OnDisable()
        {
            actionRunner.OnFinishedAction -= OnFinishedAction;
        }

        public void StartBehavior()
        {
            actionRunner.LoadAction(currentPlan.Dequeue());
        }

        public void InterruptBehavior()
        {
            actionRunner.InterruptAction();
            currentPlan = null;
        }

        private void OnFinishedAction()
        {
            // Get the next action to perform
            if (currentPlan.Count == 0)
            {
                currentPlan = null;
                return;
            }

            actionRunner.LoadAction(currentPlan.Dequeue());
        }

        public void LoadPlan(Queue<string> plan)
        {
            currentPlan = plan;
            StartBehavior();
        }
    }
}