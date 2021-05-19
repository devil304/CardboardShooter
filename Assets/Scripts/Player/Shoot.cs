using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Tooltip("Set shooting range")]
    [Range(0,100)]
    [SerializeField] float maxDistance = 15f;
    [Tooltip("Shooting damage value")]
    [Range(0, int.MaxValue / 1.1f)]
    [SerializeField] float DMG = 5f;
    void Update()
    {
        //Shoot the enemy
        RaycastHit hit;
        if (Google.XR.Cardboard.Api.IsTriggerPressed && Physics.Raycast(transform.position, transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Enemy")
        {
            hit.collider.gameObject.SendMessage("Hit",DMG);
        }
    }
}
