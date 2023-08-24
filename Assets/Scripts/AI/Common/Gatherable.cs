using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    public class Gatherable : MonoBehaviour
    {
        private static readonly List<Gatherable> gatherables = new();

        void Awake()
        {
            gatherables.Add(this);
        }        

        public static Gatherable GetNearest(Vector3 pos)
        {
            float dist = Mathf.Infinity;
            Gatherable nearest = null;
            foreach (Gatherable g in gatherables)
            {
                float currentDist = Vector3.Distance(g.transform.position, pos);
                if (currentDist < dist)
                {
                    nearest = g;
                    dist = currentDist;
                }
            }

            return nearest;
        }

        public static Gatherable GetNearest(Vector3 pos, string tag)
        {
            float dist = Mathf.Infinity;
            Gatherable nearest = null;
            foreach (Gatherable g in gatherables)
            {
                float currentDist = Vector3.Distance(g.transform.position, pos);
                if (g.gameObject.tag.Equals(tag) 
                    && g.gameObject.activeInHierarchy
                    && currentDist < dist)
                {
                    nearest = g;
                    dist = currentDist;
                }
            }

            return nearest;
        }

        public static void Destroy(Gatherable g)
        {
            //gatherables.Remove(g);
            g.gameObject.SetActive(false);
        }        
    }
}