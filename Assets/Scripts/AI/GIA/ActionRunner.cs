using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TooLoo.AI
{
    public class ActionRunner : MonoBehaviour
    {
        public UnityAction<string> OnLoadAction;
        public UnityAction OnFinishedAction;

        private AIAgent agent;
        private ActionLogic currentAction;
        private bool isRunning;

        private void Update()
        {
            if (isRunning)
            {
                currentAction?.UpdateAction(agent);
            }            
        }

        public void Init(AIAgent agent)
        {
            this.agent = agent;
            this.agent.AIMover.OnReachedTarget += StartAction;
            isRunning = false;
        }

        private void OnDisable()
        {
            agent.AIMover.OnReachedTarget -= StartAction;
        }

        public void StartAction()
        {
            currentAction?.StartAction(agent);
            isRunning = true;
        }

        public void InterruptAction()
        {
            currentAction?.StopAction(agent);
            currentAction = null;
            isRunning = false;
        }

        public void StopAction()
        {
            currentAction?.StopAction(agent);
            currentAction = null;
            isRunning = false;

            OnFinishedAction?.Invoke();
        }

        public void LoadAction(string id)
        {
            currentAction = ActionLogic.Get(id);
            currentAction?.Init(agent);
            OnLoadAction?.Invoke(currentAction.name);
        }
    }
}