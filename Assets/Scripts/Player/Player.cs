using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player single;
    [HideInInspector]

    [SerializeField] Slider MyHPSlider;

    [Tooltip("Health value between 0 and inf.")]
    [Range(0, int.MaxValue / 1.1f)]
    [SerializeField] int MaxHP = 100;
    int ActualHP;
    private void Awake()
    {
        if(!single)
            single = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ActualHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit(int val)
    {
        ActualHP -= val;
        MyHPSlider.value = (float)ActualHP / MaxHP;
        if (ActualHP <= 0)
            Kill();
    }
    void Kill()
    {

    }

        //Display FPS counter in debug builds
    private void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            GUI.Label(new Rect(10, 10, 100, 20), (int)(1f / Time.unscaledDeltaTime) + " FPS");
        }
    }
}
