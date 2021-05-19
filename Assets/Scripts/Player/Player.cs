using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player single;
    [HideInInspector]public int Score = 0;

    [SerializeField]TextMeshProUGUI ScoreText;
    [SerializeField] string ScorePrefix = "Score: ";

    [SerializeField] Slider MyHPSlider;
    Image HPBarFiller;
    float HPColorAtStart = 115;

    [Tooltip("Health value between 0 and large number.")]
    [Range(0, int.MaxValue / 1.1f)]
    [SerializeField] int MaxHP = 100;
    int ActualHP;
    float s, v;
    private void Awake()
    {
        if(!single)
            single = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ActualHP = MaxHP;
        MyHPSlider.value = (float)ActualHP / MaxHP;
        HPBarFiller = MyHPSlider.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        float h;
        Color.RGBToHSV(HPBarFiller.color, out h, out s, out v);
        HPColorAtStart = h;
        StartCoroutine(HitMe());
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = ScorePrefix + Score;
    }
#if UNITY_EDITOR
    IEnumerator HitMe()
    {
        while (ActualHP > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Hit(1);
        }
    }
#endif
    public void Hit(int val)
    {
        ActualHP -= val;
        MyHPSlider.value = (float)ActualHP / MaxHP;
        HPBarFiller.color = Color.HSVToRGB(HPColorAtStart*((float)ActualHP / MaxHP),s,v);
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
            GUI.Label(new Rect(25, 25, 100, 20), (int)(1f / Time.unscaledDeltaTime) + " FPS");
        }
    }
}
