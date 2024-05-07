using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DashSkill : MonoBehaviour
{
    public string playerInputPrefix;
    public string enemyInputPrefix;
    public DashSkill enemyDashSkillScript;
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;

    public bool canDash = true;
    private float dashCooldown = 0.9f; 
    public float dashTimer = 0f;

    public Animator animationDash;

    public Movement movementScript;
    public ShieldSkill shieldSkillEnemyScript;
    public Health healthEnemyScript;
    public GameObject particleEffectKnockback;
    public Animator particleEffectAnim;

    public Image imageSkill;
    public AudioSource shieldBreak;
    public AudioSource dashSwoosh;
    public AudioSource Parry;
    private void Start()
    {
        imageSkill.fillAmount = 1;
    }

    private void Update()
    {

        if (canDash)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerInputPrefix == "Player1")
            {
                StartDash();
            }
            else if (Input.GetKeyDown(KeyCode.Delete) && playerInputPrefix == "Player2")
            {
                StartDash();
            }
        }

        if (isDashing)
        {
            Dash();
        }

        if (!canDash)
        {
            dashTimer += Time.deltaTime;
            imageSkill.fillAmount += 1 / dashCooldown * Time.deltaTime;
            if (imageSkill.fillAmount <= 0)
            {
                imageSkill.fillAmount = 1;
            }

            if (dashTimer >= dashCooldown)
            {
                canDash = true;
                dashTimer = 0f;
            }
        }

    }

    private void StartDash()
    {
        if (canDash)
        {
            isDashing = true;
            Invoke("StopDash", dashDuration);
            movementScript.universalCanMove = false;
            canDash = false;
            animationDash.SetTrigger("Dash");
            imageSkill.fillAmount = 0f;

        }

    }

    private void Dash()
    {

        float horizontal = Input.GetAxisRaw(playerInputPrefix + "Horizontal");
        float vertical = Input.GetAxisRaw(playerInputPrefix + "Vertical");
        dashSwoosh.Play();
        Vector3 dashDirection = new Vector3(horizontal, vertical, 0f).normalized;
        transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
    }

    private void StopDash()
    {
        isDashing = false;
        movementScript.universalCanMove = true;
    }

    public float knockbackForce = 5f;

    private void Knockback(Collider2D collision)
    {
        Camera.main.GetComponent<CameraShake>().Shake(3f);
        particleEffectKnockback.SetActive(true);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDashing)
        {
            if(collision.CompareTag("Player2") && playerInputPrefix == "Player1" && enemyDashSkillScript.isDashing == true)
            {
                Debug.Log("PARRY KNOCKBACK");
                Knockback(collision);
                Parry.Play();
            }
            else if (collision.CompareTag("Player1") && playerInputPrefix == "Player2" && enemyDashSkillScript.isDashing == true)
            {
                Debug.Log("PARRY KNOCKBACK");
                Knockback(collision);
                Parry.Play();
            }
            else
            {
                if (shieldSkillEnemyScript.universalIsShieldActive)
                {
                    if (collision.CompareTag("Player2") && playerInputPrefix == "Player1")
                    {
                        Debug.Log("Player1 Hit DASH (COUNTERED)");
                        shieldSkillEnemyScript.DeactivateShield();
                        Camera.main.GetComponent<CameraShake>().Shake(0.3f);
                        shieldBreak.Play();


                    }else if(collision.CompareTag("Player1") && playerInputPrefix == "Player2")
                    {
                        Debug.Log("Player2 Hit DASH (COUNTERED)");
                        shieldSkillEnemyScript.DeactivateShield();
                        Camera.main.GetComponent<CameraShake>().Shake(0.3f);
                        shieldBreak.Play();
                    }
                }
                else
                {
                    //Nanti bakal buat script baru yang dlmnya khusus health player, terus di sini referensikan penurunan health
                    if (collision.CompareTag("Player2") && playerInputPrefix == "Player1" && !shieldSkillEnemyScript.universalIsShieldActive)
                    {
                        Debug.Log("Player1 Hit FATAL");
                        healthEnemyScript.TakeDamage();
                        Camera.main.GetComponent<CameraShake>().Shake(0.8f);

                    }
                    else if (collision.CompareTag("Player1") && playerInputPrefix == "Player2" && !shieldSkillEnemyScript.universalIsShieldActive)
                    {
                        Debug.Log("Player2 Hit FATAL");
                        healthEnemyScript.TakeDamage();
                        Camera.main.GetComponent<CameraShake>().Shake(0.8f);
                    }
                }
            }






        } //tutup if dashing

    }


}
