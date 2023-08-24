using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    public class ActionProgressTracker : MonoBehaviour
    {
        private float progress;

        public float Progress => progress;

        private void Start()
        {
            progress = 0f;
        }

        public void AddActionProgress(float amount)
        {
            progress += amount;
        }

        public void ResetProgress()
        {
            progress = 0;
        }
    }
}