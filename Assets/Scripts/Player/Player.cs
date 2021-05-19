using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Player script. Handles HP, UI and Weapons.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Actual Player script instance.
    /// </summary>
    public static Player single;

    [HideInInspector] public int Score;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] string ScorePrefix = "Score: ";

    [SerializeField] Slider MyHPSlider;
    Image HPBarFiller;
    float HPColorAtStart = 115;
    [HideInInspector] public bool died = false;

    [Tooltip("Health value between 0 and large number.")]
    [Range(0, int.MaxValue / 1.1f)]
    [SerializeField] int MaxHP = 100;
    int ActualHP;


    WeaponInterface ActualWeapon;

    float s, v;

    GameObject LastHitedWeapon = null;

    [HideInInspector] public AudioSource MySFXAudioSource;

#if UNITY_EDITOR
    [SerializeField] float RotationSpeed=25;

    [SerializeField] bool HitMyself=true;
#endif

    #endregion
    #region Unity Methods
    private void Awake()
    {
        Time.timeScale = 1;
        if(!single)
            single = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        MySFXAudioSource = GetComponent<AudioSource>();
        Score = 100;
        ActualHP = MaxHP;
        MyHPSlider.value = (float)ActualHP / MaxHP;
        HPBarFiller = MyHPSlider.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        float h;
        Color.RGBToHSV(HPBarFiller.color, out h, out s, out v);
        HPColorAtStart = h;
        ActualWeapon = FindObjectOfType<Gun>();
#if UNITY_EDITOR
        if(HitMyself)
            StartCoroutine(HitMe());
        Cursor.lockState = CursorLockMode.Confined;
#endif
    }
#if UNITY_EDITOR
    Vector2 lastMousePos = Vector2.zero;
#endif
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        float x = RotationSpeed * Input.GetAxis("Mouse X");
        float y = RotationSpeed * -Input.GetAxis("Mouse Y");
        Camera.main.transform.Rotate(new Vector3(0, x*Time.unscaledDeltaTime, 0), Space.World);
        Camera.main.transform.Rotate(new Vector3(y * Time.unscaledDeltaTime, 0, 0), Space.Self);
#endif
        ScoreText.text = ScorePrefix + Score;
        ActualWeapon.UpdateFromPlayer();
        if(ActualWeapon.RHit.collider && ActualWeapon.RHit.collider.tag == "Weapon")
        {
            if(LastHitedWeapon!= ActualWeapon.RHit.collider.gameObject)
            {
                if (LastHitedWeapon)
                    LastHitedWeapon.SendMessage("HideText");
                LastHitedWeapon = ActualWeapon.RHit.collider.gameObject;
            }
            LastHitedWeapon.SendMessage("ShowText");
        }else if (LastHitedWeapon)
        {
            LastHitedWeapon.SendMessage("HideText");
            LastHitedWeapon = null;
        }

        if (
#if UNITY_ANDROID && !UNITY_EDITOR
            Google.XR.Cardboard.Api.IsTriggerPressed
#else
            Input.GetMouseButtonDown(0)
#endif
            )
        {
            if (ActualWeapon.RHit.collider && ActualWeapon.RHit.collider.tag == "Weapon")
            {
                ActualWeapon = ActualWeapon.RHit.collider.GetComponent<WeaponInterface>();
            }else
                ActualWeapon.Shoot();
        }

    }
    //Display FPS counter in debug builds
    private void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            GUI.Label(new Rect(25, 25, 100, 20), (int)(1f / Time.unscaledDeltaTime) + " FPS");
        }
    }
#endregion
#region Other Methods
    public void Hit(int val)
    {
        Debug.Log("Player hited");
        ActualHP -= val;
        ActualHP = ActualHP > MaxHP ? MaxHP : ActualHP;
        MyHPSlider.value = (float)ActualHP / MaxHP;
        HPBarFiller.color = Color.HSVToRGB(HPColorAtStart*((float)ActualHP / MaxHP),s,v);
        if (ActualHP <= 0)
            Kill();
    }
    void Kill()
    {
        Time.timeScale = 0.001f;
        died = true;
        StartCoroutine(WiatAndReload());
    }
    IEnumerator WiatAndReload()
    {
        yield return new WaitForSecondsRealtime(5);
        single = null;
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    #endregion
}
