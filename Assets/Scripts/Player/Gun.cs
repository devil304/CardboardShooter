using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Gun script.
/// </summary>
public class Gun : WeaponBase
{
    [SerializeField] float DMG = 1f;
    [SerializeField] GameObject SparksVFX;
    public override void Shoot()
    {
        if (Physics.Raycast(TipOfTheGun.position, TipOfTheGun.forward, out hit, maxDistance)) {
            Player.single.MySFXAudioSource.PlayOneShot(SFX[Random.Range(0,SFX.Length)]);
            GameObject Spawned = Instantiate(SparksVFX,hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
            Destroy(Spawned, 1.5f);
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.SendMessage("Hit", DMG + Player.single.Score / 1000);
            }
        }
    }
}
