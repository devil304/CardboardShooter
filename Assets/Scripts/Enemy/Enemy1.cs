using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : EnemyBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MyNMA.SetDestination(Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(MyNMA.pathStatus == NavMeshPathStatus.PathComplete && MyNMA.remainingDistance < MyNMA.stoppingDistance+0.1f)
        {
            MyAnimator.SetBool("Idle", false);
        }   
    }

    void HitPlayer()
    {
        HitPlayer(DMG);
    }
}
