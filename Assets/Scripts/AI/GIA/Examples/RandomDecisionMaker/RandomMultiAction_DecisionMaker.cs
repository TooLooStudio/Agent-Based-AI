using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    public class RandomMultiAction_DecisionMaker : DecisionMaker
    {
        [SerializeField] private Sensor sensor;
        [SerializeField] private float frequency = 1f;
        [SerializeField] private int maxActions = 5;

        [Header("Flee Action")]
        [SerializeField] private ActionLogic fleeAction;

        [Header("Available Actions")]
        [SerializeField] private List<ActionLogic> actions;

        private Plan fleePlan;
        private Plan currentPlan;

        private void Start()
        {
            fleePlan = new(fleeAction.UID);
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

            RandomlyGeneratePlan();
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

        private void RandomlyGeneratePlan()
        {
            if (actions is null || actions.Count == 0) return;            

            System.Random rnd = new();
            List<string> selectedActions = new();
            
            for (int i = 0; i < maxActions; i++)
            {
                int randIndex = rnd.Next(actions.Count);
                selectedActions.Add(actions[randIndex].UID);
            }

            currentPlan = new Plan(selectedActions);
            behaviorRunner.LoadPlan(currentPlan.ActionQueue());
            OnDecideBehavior?.Invoke(currentPlan.ActionSequence());
        }
    }
}