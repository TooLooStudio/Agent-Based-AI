using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    public class UIDisplay : MonoBehaviour
    {
        [SerializeField] private AIAgent agent;

        [SerializeField] private TextMeshProUGUI currentAction;
        [SerializeField] private TextMeshProUGUI currentPlan;

        private void OnEnable()
        {
            agent.DecisionMaker.OnDecideBehavior += UpdatePlan;
            agent.ActionRunner.OnLoadAction += UpdateAction;
        }

        private void OnDisable()
        {
            agent.DecisionMaker.OnDecideBehavior -= UpdatePlan;
            agent.ActionRunner.OnLoadAction -= UpdateAction;
        }

        public void UpdatePlan(string plan)
        {
            currentPlan.text = plan;
        }

        public void UpdateAction(string action)
        {
            currentAction.text = action;
        }
    }
}
