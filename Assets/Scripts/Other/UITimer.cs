using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITimer : MonoBehaviour
{
    [SerializeField] string Prefix = "Time ";
    float StartTime;
    TextMeshProUGUI MyTMP;
    void Start()
    {
        MyTMP = GetComponent<TextMeshProUGUI>();
        StartTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        MyTMP.text = Prefix +"\n"+ Mathf.FloorToInt((Time.realtimeSinceStartup - StartTime) / 60) + ":" + String.Format("{0:00.00}", (Time.realtimeSinceStartup - StartTime) % 60) ;
    }
}
