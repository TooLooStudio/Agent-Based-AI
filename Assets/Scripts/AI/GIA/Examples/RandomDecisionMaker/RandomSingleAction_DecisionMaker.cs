using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    public class RandomSingleAction_DecisionMaker : DecisionMaker
    {
        [SerializeField] private float frequency = 1f;
        [SerializeField] private Sensor sensor;

        [Header("Flee Action")]
        [SerializeField] private ActionLogic fleeAction;

        [Header("Available Actions")]
        [SerializeField] private List<ActionLogic> actions;

        private Plan fleePlan;
        private Plan currentPlan;

        private void Start()
        {
            fleePlan = new(fleeAction);

            sensor.OnDetectedTargets += OnDetectedTargets;

            StartCoroutine(Run(frequency));
        }

        private void OnDisable()
        {
            sensor.OnDetectedTargets -= OnDetectedTargets;
        }

        IEnumerator Run(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                DecideBehavior();
            }
        }

        public override void DecideBehavior()
        {
            if (behaviorRunner.HasPlan) return;

            SelectSingleAction();
        }

        private void OnDetectedTargets()
        {
            Debug.Log("Enemy detected!");
            
            if (currentPlan == fleePlan) return;

            behaviorRunner.InterruptBehavior();
            currentPlan = fleePlan;
            behaviorRunner.LoadPlan(currentPlan.ActionQueue());

            OnDecideBehavior?.Invoke(currentPlan.ActionSequence());
        }

        private void SelectSingleAction()
        {
            currentPlan = new(actions[Random.Range(0, actions.Count)]);

            Debug.Log($"Current Behavior: {currentPlan.ActionSequence()}");
            behaviorRunner.LoadPlan(currentPlan.ActionQueue());

            OnDecideBehavior?.Invoke(currentPlan.ActionSequence());
        }
    }
}