using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    CanvasGroup MyCG;
    private void Start()
    {
        MyCG = transform.GetChild(0).GetComponent<CanvasGroup>();
    }
    void Update()
    {
        if(MyCG.alpha!=0)
            transform.LookAt(Camera.main.transform);       
    }
}
