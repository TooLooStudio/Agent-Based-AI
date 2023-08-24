using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;

public class FollowAgent : MonoBehaviour
{
    [SerializeField] private AIAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = agent.transform.position;
    }
}
