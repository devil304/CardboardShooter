using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    CanvasGroup MyCG;
    private void Start()
    {
        transform.GetChild(0).TryGetComponent(out MyCG);
    }
    void Update()
    {
        if((MyCG && MyCG.alpha!=0) || (!MyCG))
            transform.LookAt(Camera.main.transform);       
    }
}
