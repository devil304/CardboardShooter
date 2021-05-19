using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : EnemyBase
{

    // Update is called once per frame
    void Update()
    {
        //Check if destination reached
        if (MyNMA.pathStatus == NavMeshPathStatus.PathComplete && MyNMA.remainingDistance < MyNMA.stoppingDistance+0.1f)
        {
            MyAnimator.SetBool("Idle", false);
        }   
    }
}
