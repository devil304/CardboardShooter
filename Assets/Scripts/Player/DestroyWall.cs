using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyWall : MonoBehaviour
{
    AudioSource MySFX;
    [SerializeField] AudioClip Up, Down;
    private void Start()
    {
        MySFX.PlayOneShot(Up);
    }
    void goingDown()
    {
        MySFX.PlayOneShot(Down);
    }
    void destroyWall()
    {
        Destroy(transform.parent.gameObject);
    }
}
