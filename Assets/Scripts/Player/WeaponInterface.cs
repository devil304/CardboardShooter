using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WeaponInterface
{
    public RaycastHit RHit { get; }
    public void Shoot();
    public void UpdateFromPlayer();
}
