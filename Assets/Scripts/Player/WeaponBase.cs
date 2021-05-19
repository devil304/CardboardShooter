using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon Base script.
/// </summary>
public class WeaponBase : MonoBehaviour, WeaponInterface
{
    #region Variables
    [Tooltip("Set shooting range")]
    [Range(0, 100)]
    [SerializeField] protected float maxDistance = 25f;
    [Tooltip("Shooting damage value")]
    [Range(0, int.MaxValue / 1.1f)]
    protected RaycastHit hit;
    [SerializeField] LineRenderer MyLine;
    [SerializeField] protected Transform TipOfTheGun;
    [SerializeField] MeshRenderer LaserPoint;
    [SerializeField] CanvasGroup MyTextCG;
    [ColorUsage(true, true)]
    [SerializeField] Color LaserColor=Color.red;
    [SerializeField] protected int ShootingCost = 10;

    public RaycastHit RHit { get => hit; }
    #endregion
    #region Methods
    public virtual void Shoot()
    {
        throw new NotImplementedException();
    }

    public void ShowText()
    {
        MyTextCG.alpha = 1;
    }

    public void HideText()
    {
        MyTextCG.alpha = 0;
    }

    public void UpdateFromPlayer()
    {
        if (MyLine.material.GetColor("_EmissionColor") != LaserColor)
            MyLine.material.SetColor("_EmissionColor", LaserColor);
        float distance = maxDistance;
        if (Physics.Raycast(TipOfTheGun.position, TipOfTheGun.forward, out hit, maxDistance))
        {
            if(LaserPoint.material.GetColor("_EmissionColor") != LaserColor)
                LaserPoint.material.SetColor("_EmissionColor", LaserColor);
            distance = Vector3.Distance(hit.point, TipOfTheGun.position);
            LaserPoint.transform.parent.transform.position = hit.point + (hit.normal * 0.01f);
            LaserPoint.transform.parent.transform.LookAt(LaserPoint.transform.parent.transform.position + hit.normal);
            LaserPoint.transform.parent.transform.localScale = Vector3.one * ((distance > 5 ? distance : 5) / 5);
        }
        else if (LaserPoint.transform.parent.transform.position != Vector3.up * 1000f)
        {
            LaserPoint.transform.parent.transform.localScale = Vector3.one;
            LaserPoint.transform.parent.transform.position = Vector3.up * 1000f;
        }
        MyLine.SetPosition(0, TipOfTheGun.position);
        MyLine.SetPosition(1, TipOfTheGun.position + (MyLine.transform.forward * distance));
    }
    #endregion
}
