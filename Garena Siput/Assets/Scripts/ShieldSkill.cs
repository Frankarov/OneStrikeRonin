using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ShieldSkill : MonoBehaviour
{

    public GameObject shieldPrefab;
    public float shieldDuration = 3f;
    public float cooldownDuration = 3f;

    public bool universalIsShieldActive = false;
    public float shieldTimer = 0f;
    public float cooldownTimer = 0f;

    public string playerInputPrefix;
    public Image imageSkill;
    private float shieldTimerUI;
    private float shieldCooldownUI = 5f;
    private bool canShield = true;
    InputAction playerInputAction;
    private void Awake()
    {
        playerInputAction = new();
    }

    private void OnEnable()
    {
        playerInputAction.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }
    private void Start()
    {
        imageSkill.fillAmount = 1;
    }

    void Update()
    {

        if(cooldownDuration <= 0f)
        {
            canShield = true;
        }

        if (!canShield)
        {
            imageSkill.fillAmount += 1 / shieldCooldownUI * Time.deltaTime;
            if (imageSkill.fillAmount <= 0)
            {
                imageSkill.fillAmount = 1;
                shieldCooldownUI = 0;
            }
        }



        if (universalIsShieldActive)
        {
            shieldTimer -= Time.deltaTime;



        if (shieldTimer <= 0f)
        {
            DeactivateShield();
        }
        }
        else if (cooldownTimer > 0f)
        {

            cooldownTimer -= Time.deltaTime;
        }

    }

    public void OnShield()
    {
        if(universalIsShieldActive && cooldownTimer > 0f)
        {
            return;
        }

        canShield = false;
        universalIsShieldActive = true;
        shieldTimer = shieldDuration;
        imageSkill.fillAmount = 0;
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shield.transform.parent = transform;
        imageSkill.fillAmount = 0f;
    }
    
    public void OnShieldPlayer2()
    {
        if(universalIsShieldActive && cooldownTimer > 0f)
        {
            return;
        }

        canShield = false;
        universalIsShieldActive = true;
        shieldTimer = shieldDuration;
        imageSkill.fillAmount = 0;
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shield.transform.parent = transform;
        imageSkill.fillAmount = 0f;
    }

    public void DeactivateShield()
    {
        universalIsShieldActive = false;
        cooldownTimer = cooldownDuration;

        Destroy(transform.GetChild(4).gameObject);

    }


}
