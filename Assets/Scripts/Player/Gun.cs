using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gun script.
/// </summary>
public class Gun : WeaponBase
{
    [SerializeField] float DMG = 1f;
    public override void Shoot()
    {
        if (Physics.Raycast(TipOfTheGun.position, TipOfTheGun.forward, out hit, maxDistance) && hit.collider.tag == "Enemy" && Player.single.Score >= ShootingCost)
        {
            hit.collider.gameObject.SendMessage("Hit", DMG+Player.single.Score/1000);
        }
    }
}
