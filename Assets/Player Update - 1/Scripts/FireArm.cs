using UnityEngine;
using System.Collections;

public class FireArm : Weapon
{
    public float FireRate;
    public int ShotsFired;
    public float Range;
    public float Accuracy;
    private float CoolDownTime;

    public int ClipSize;
    private int _currentAmmo;
    public int CurrentAmmo { get { return _currentAmmo; } set { _currentAmmo = value; AmmoLabel.SetText(string.Format("{0}/{1}", CurrentAmmo, ClipSize)); } }

    public float ReloadSpeed = 0.5f;

    public Animator animator;

    public bool CanAim = false;

    public Vector3 AimPosition;
    public float WeaponCamera_AimFOV;
    public float NormalCamera_AimFOV;

    float WeaponCamera_defaultFOV;
    float NormalCamera_defaultFOV;

    Vector3 defaultPosition;

    bool isAiming = false;
    public bool isAutomatic = false;

    public GUITextElement AmmoLabel;


    [Header("SFX")]
    public AudioClip SFX_Fire;
    public AudioClip SFX_Empty;
    public AudioClip[] SFX_Reload;


    Camera weaponCamera;
    Camera normalCamera;

    public void Start()
    {
        weaponCamera = transform.parent.GetComponent<Camera>();
        normalCamera = transform.parent.parent.GetComponent<Camera>();

        defaultPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        WeaponCamera_defaultFOV = (float)weaponCamera.fieldOfView;
        NormalCamera_defaultFOV = (float)normalCamera.fieldOfView;

        AmmoLabel = PlayerGUI.Instance.AmmoLabel;
    }

    
    public void PlayReloadSFX(int i)
    {
        AudioSource.PlayClipAtPoint(SFX_Reload[i], transform.position);
    }

    public void Update()
    {
        if (CoolDownTime > 0)
            CoolDownTime -= Time.deltaTime;
          
        if (CoolDownTime < 0)
            CoolDownTime = 0;

        if(CanActivate())
            CustomKeyBinds();

        HandleAiming();


        

    }
    public float fovSpeed = 300;

    public void HandleAiming()
    {

        if (isAiming)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, AimPosition, 0.5f * Time.deltaTime);
            weaponCamera.fieldOfView = Mathf.Lerp(weaponCamera.fieldOfView, WeaponCamera_AimFOV, fovSpeed * Time.deltaTime);
            normalCamera.fieldOfView = Mathf.Lerp(normalCamera.fieldOfView, NormalCamera_AimFOV, fovSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, defaultPosition, 0.5f * Time.deltaTime);
            weaponCamera.fieldOfView = Mathf.Lerp(weaponCamera.fieldOfView, WeaponCamera_defaultFOV, fovSpeed * Time.deltaTime);
            normalCamera.fieldOfView = Mathf.Lerp(normalCamera.fieldOfView, NormalCamera_defaultFOV, fovSpeed * Time.deltaTime);
        }
    }

    public override void Activate()
    {
        if (CanActivate())
        {
            Fire(Camera.main);
        }
    }

    public void Fire(Camera camera)
    {
        if (CurrentAmmo > 0)
        {
            CurrentAmmo -= 1;
            animator.SetTrigger("Fire");
            CoolDownTime = FireRate;
            AudioSource.PlayClipAtPoint(SFX_Fire, transform.position);
            if(CurrentAmmo <= 0)
                animator.SetBool("Empty", true);
        }
        else
        {
            AudioSource.PlayClipAtPoint(SFX_Empty, transform.position);
            CoolDownTime = FireRate;
            
        }
    }

    public void CustomKeyBinds()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetMouseButton(0) && isAutomatic)
            Activate();

        if (Input.GetMouseButtonDown(0) && !isAutomatic)
            Activate();

        if (Input.GetKey(KeyCode.V))
            AmmoLabel.PopGUI(0.1f);


        if(!isDisabled)
        isAiming = Input.GetMouseButton(1);

    
    }

    public void Reload()
    {
        if (CurrentAmmo == ClipSize)
            return;

        DisableWeapon();
       animator.SetTrigger("Reload");
        animator.SetBool("Empty", false);
        Invoke("EnableWeapon", ReloadSpeed);
        Invoke("ReloadClip", ReloadSpeed);
    }

    public void ReloadClip()
    {
        CurrentAmmo = ClipSize;
    }


    bool isDisabled = false;
    public void DisableWeapon() { isAiming = false; isDisabled = true; }
    public void EnableWeapon() { isDisabled = false;  }

    public override bool CanActivate()
    {
        return CoolDownTime <= 0  && !isDisabled;
    }
}
