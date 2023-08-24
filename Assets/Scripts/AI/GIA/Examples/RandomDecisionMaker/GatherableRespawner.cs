using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.Examples
{
    public class GatherableRespawner : MonoBehaviour
    {
        [SerializeField] private float respawnTime = 5f;

        private readonly List<Gatherable> gatherables = new();

        private void Start()
        {
            foreach (Gatherable g in FindObjectsOfType<Gatherable>())
            {
                gatherables.Add(g);
            }

            StartCoroutine(Respawn(respawnTime));
        }

        IEnumerator Respawn(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                foreach (Gatherable g in gatherables)
                {
                    g.gameObject.SetActive(true);
                }
            }
        }
    }
}