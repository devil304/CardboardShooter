using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gun script script.
/// </summary>
public class Gun :MonoBehaviour, WeaponInterface
{
    [Tooltip("Set shooting range")]
    [Range(0,100)]
    [SerializeField] protected float maxDistance = 25f;
    [Tooltip("Shooting damage value")]
    [Range(0, int.MaxValue / 1.1f)]
    [SerializeField] protected float DMG = 5f;
    RaycastHit hit;
    [SerializeField] LineRenderer MyLine;
    [SerializeField] Transform TipOfTheGun;
    [SerializeField] GameObject LaserPoint;

    public void Shoot()
    {
        if (Physics.Raycast(TipOfTheGun.position, TipOfTheGun.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Enemy")
        {
            hit.collider.gameObject.SendMessage("Hit", DMG);
        }
    }

    public void Update()
    {
        float distance = maxDistance;
        if (Physics.Raycast(TipOfTheGun.position, TipOfTheGun.forward, out hit, maxDistance))
        {
            distance = Vector3.Distance(hit.point, TipOfTheGun.position);
            LaserPoint.transform.position = hit.point + (hit.normal*0.01f);
            LaserPoint.transform.LookAt(LaserPoint.transform.position+hit.normal);
            LaserPoint.transform.localScale = Vector3.one*((distance>5?distance:5)/5);
        }
        else if(LaserPoint.transform.position!= Vector3.up * 1000f)
        {
            LaserPoint.transform.localScale = Vector3.one;
            LaserPoint.transform.position = Vector3.up * 1000f;
        }
        MyLine.SetPosition(0, TipOfTheGun.position);
        MyLine.SetPosition(1,TipOfTheGun.position+(Vector3.forward*distance));
    }
}
