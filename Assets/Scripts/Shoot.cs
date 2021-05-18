using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] float maxDistance = 15f;
    void Update()
    {
        //Shoot the enemy
        RaycastHit hit;
        if (Google.XR.Cardboard.Api.IsTriggerPressed && Physics.Raycast(transform.position, transform.forward, out hit, maxDistance) && hit.collider.gameObject.tag == "Enemy")
        {
            hit.collider.gameObject.SendMessage("Kill");
        }
    }
    //Display FPS counter in debug builds
    private void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            GUI.Label(new Rect(10, 10, 100, 20), (int)(1f/ Time.unscaledDeltaTime)+" FPS");
        }
    }
}
