using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TooLoo.AI
{
    public abstract class DecisionMaker : MonoBehaviour
    {
        protected BehaviorRunner behaviorRunner;

        public UnityAction<string> OnDecideBehavior;

        public virtual void Init(BehaviorRunner behaviorRunner)
        {
            this.behaviorRunner = behaviorRunner;
        }

        public abstract void DecideBehavior();
    }
}