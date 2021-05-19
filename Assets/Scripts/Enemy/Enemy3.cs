using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class Enemy3 : EnemyBase
{
    [SerializeField] AudioClip BeamSFX;
    LineRenderer MyBeam;
    // Update is called once per frame
    protected override void Start()
    {
        base.Start();
        MyBeam = GetComponent<LineRenderer>();
    }
    void Update()
    {
        //Check if destination reached
        if(MyNMA.pathStatus == NavMeshPathStatus.PathComplete && MyNMA.remainingDistance < MyNMA.stoppingDistance+0.1f)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + transform.forward * 0.6f, (Camera.main.transform.position - transform.position).normalized* (MyNMA.stoppingDistance+1), Color.red);
            if(Physics.Raycast(transform.position+transform.forward*0.6f, (Camera.main.transform.position-transform.position).normalized, out hit, MyNMA.stoppingDistance+1) && hit.collider.tag == "Player")
            {
                Vector3 PlayerPosForCalc = Camera.main.transform.position;
                PlayerPosForCalc.y = transform.position.y;
                transform.LookAt(PlayerPosForCalc);
                MyAnimator.SetBool("Idle",false);
            }
            else
            {
                //If can't shoot at player, move closer
                MyAnimator.SetBool("Idle", true);
                MyNMA.stoppingDistance -= MyNMA.stoppingDistance>2?1:0;
            }
        }   
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.forward * 0.6f, (Camera.main.transform.position - transform.position).normalized, out hit, MyNMA.stoppingDistance + 1) && hit.collider.tag == "Player")
        {
            MyAudioSource.PlayOneShot(BeamSFX);
            StartCoroutine(ShootBeam(hit));
            HitPlayer();
        }
    }
    IEnumerator ShootBeam(RaycastHit hit)
    {
        MyBeam.enabled = true;
        MyBeam.SetPosition(0, transform.position + transform.forward * 0.6f);
        MyBeam.SetPosition(1, hit.point);
        yield return new WaitForSecondsRealtime(0.4f);
        MyBeam.enabled = false;
    }
}
