using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooLoo.AI.Examples;

namespace TooLoo.AI
{
    public class AIAgent : MonoBehaviour
    {
        [SerializeField] private DecisionMaker decisionMaker;
        [SerializeField] private BehaviorRunner behaviorRunner;
        [SerializeField] private ActionRunner actionRunner;
        [SerializeField] private Sensor sensor;

        [SerializeField] private SimpleAIMover simpleAIMover;
        [SerializeField] private ActionProgressTracker actionProgressTracker;

        public DecisionMaker DecisionMaker => decisionMaker;
        public ActionRunner ActionRunner => actionRunner;
        public SimpleAIMover AIMover => simpleAIMover;
        public Sensor Sensor => sensor;
        public ActionProgressTracker ActionProgressTracker => actionProgressTracker;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            sensor.Init(this);
            actionRunner.Init(this);
            behaviorRunner.Init(actionRunner);
            decisionMaker.Init(behaviorRunner);
        }
    }
}