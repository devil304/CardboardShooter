using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// Base Enemy script.
/// </summary>
[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (AudioSource))]
public class EnemyBase : MonoBehaviour
{
    #region Variables
    protected NavMeshAgent MyNMA;
    protected Material MyMaterial;
    protected SkinnedMeshRenderer MySMR;
    protected Animator MyAnimator;
    protected AudioSource MyAudioSource;
    [SerializeField] protected Slider MyHPSlider;
    protected CanvasGroup SliderCG;

    [Tooltip("Health value between 0 and large number.")]
    [Range(0, int.MaxValue/1.1f)]
    [SerializeField] protected int MaxHP =10;
    [Tooltip("Damage value between 0 and large number.")]
    [Range(0, int.MaxValue / 1.1f)]
    [SerializeField] protected int DMG = 1;
    [Tooltip("Set speed of showing and hiding HP Slider.")]
    [Range(0,10)]
    [SerializeField] protected float ShowHideSpeed = 1f;
    [Tooltip("Set time delay before hiding HP Slider")]
    [Range(0, 10)]
    [SerializeField] protected float HideDelay = 3f;
    protected int ActualHP;
    protected float LastHitTime = 0;
    protected bool ShowingHP = false;
    #endregion
    #region Unity Methods
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Get all necessary components and create copy of material
        MyNMA = GetComponent<NavMeshAgent>();
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "Enemy") {
                MySMR = transform.GetChild(i).GetComponent<SkinnedMeshRenderer>();
                break;
            }
        }
        MyMaterial = new Material(MySMR.material);
        MySMR.material = MyMaterial;
        MyAnimator = GetComponent<Animator>();
        MyAudioSource = GetComponent<AudioSource>();
        ActualHP = MaxHP;
        MyHPSlider.value = 1f;
        SliderCG = MyHPSlider.GetComponent<CanvasGroup>();
    }
#if UNITY_EDITOR
    void OnMouseDown()
    {
        Hit(1);
    }
#endif
    #endregion
    #region Other Methods
    public virtual void Hit(int val)
    {
        ActualHP -= val;
        MyHPSlider.value = (float)ActualHP / MaxHP;
        LastHitTime = Time.realtimeSinceStartup;
        if (!ShowingHP)
            StartCoroutine(ShowAndHide_CanvasGroup());
        if (ActualHP <= 0)
            Kill();
    }

    IEnumerator ShowAndHide_CanvasGroup()
    {
        ShowingHP = true;
        while (SliderCG.alpha < 1)
        {
            SliderCG.alpha += (1 / ShowHideSpeed) * Time.deltaTime;
            yield return null;
        }
        float RememberPreviusHitTime = 0;
        LastHitTime = 0;
        do {
            RememberPreviusHitTime = LastHitTime;
            yield return new WaitForSecondsRealtime(HideDelay - (LastHitTime > 0 ? (Time.realtimeSinceStartup - LastHitTime) : 0));
        } while (LastHitTime>RememberPreviusHitTime);
        while (SliderCG.alpha > 0)
        {
            SliderCG.alpha -= (1 / ShowHideSpeed) * Time.deltaTime;
            yield return null;
        }
        ShowingHP = false;
    }

    protected virtual void HitPlayer(int dmg)
    {
        Player.single.Hit(dmg);
    }

    protected virtual void Kill()
    {
        //Start death animation (in shader) and destroy enemy
        MyNMA.isStopped = true;
        MyMaterial.SetFloat("_TriggerTime", Time.time);
        MyMaterial.SetInt("_Kill", 1);
        MyAnimator.SetBool("Die", true);
        Destroy(gameObject, MyMaterial.GetFloat("_Speed")+0.25f);
        Destroy(this);
    }
    #endregion
}
