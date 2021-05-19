using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    public void destroyWall()
    {
        Destroy(transform.parent.gameObject);
    }
}
