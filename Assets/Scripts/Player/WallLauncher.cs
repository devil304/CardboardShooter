using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wall launcher script.
/// </summary>
public class WallLauncher : WeaponBase
{
    [SerializeField] GameObject Wall;

    public override void Shoot()
    {
        if (Physics.Raycast(TipOfTheGun.position, TipOfTheGun.forward, out hit, maxDistance) && hit.collider.tag == "Floor" && Player.single.Score >= ShootingCost + Player.single.Score / 1000)
        {
            Player.single.Score -= ShootingCost + Player.single.Score / 1000;
            Vector3 WallPosForCalc = hit.point * 2;
            WallPosForCalc.y = hit.point.y;
            GameObject SpawnedWall = Instantiate(Wall,hit.point,Quaternion.LookRotation(WallPosForCalc,Vector3.up));
        }
    }
}
