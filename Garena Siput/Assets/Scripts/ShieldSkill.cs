using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private void Start()
    {
        imageSkill.fillAmount = 1;
    }

    void Update()
    {
        if (!universalIsShieldActive && cooldownTimer <= 0f)
        {
            if (playerInputPrefix == "Player1" && Input.GetKeyDown(KeyCode.Q))
            {
                ActivateShield();
            }

            if (playerInputPrefix == "Player2" && Input.GetKeyDown(KeyCode.End))
            {
                ActivateShield();
            }
        }

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

    void ActivateShield()
    {
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
