using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    public class Useable : MonoBehaviour
    {
        private static readonly List<Useable> useables = new();

        void Awake()
        {
            useables.Add(this);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static Useable GetNearest(Vector3 pos)
        {
            float dist = Mathf.Infinity;
            Useable nearest = null;
            foreach (Useable m in useables)
            {
                float currentDist = Vector3.Distance(m.transform.position, pos);
                if (currentDist < dist)
                {
                    nearest = m;
                    dist = currentDist;
                }
            }

            return nearest;
        }

        public static Useable GetNearest(Vector3 pos, string tag)
        {
            float dist = Mathf.Infinity;
            Useable nearest = null;
            foreach (Useable m in useables)
            {
                float currentDist = Vector3.Distance(m.transform.position, pos);
                if (m.gameObject.tag.Equals(tag) && currentDist < dist)
                {
                    nearest = m;
                    dist = currentDist;
                }
            }

            return nearest;
        }
    }
}