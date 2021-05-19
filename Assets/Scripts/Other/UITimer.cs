using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITimer : MonoBehaviour
{
    [SerializeField] string Prefix = "Time ";
    float StartTime,TimeToDisplay;
    TextMeshProUGUI MyTMP;
    void Start()
    {
        MyTMP = GetComponent<TextMeshProUGUI>();
        StartTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.single && !Player.single.died)
            TimeToDisplay = Time.realtimeSinceStartup - StartTime;
        MyTMP.text = Prefix +"\n"+ Mathf.FloorToInt((TimeToDisplay) / 60) + ":" + String.Format("{0:00.00}", (TimeToDisplay) % 60) ;
    }
}
