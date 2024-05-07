using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlinkSkill : MonoBehaviour
{
    private Vector3 dashDirection;

    public float blinkDistance = 5f;
    public string playerInputPrefix;

    public Color blinkColor = Color.white;
    public float blinkDuration = 0.1f;
    public float blinkCooldown = 1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool canBlink = true;
    public Image imageSkill;

    public Animator animatorBlink;
    public AudioSource teleportSFX;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        imageSkill.fillAmount = 1;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw(playerInputPrefix + "Horizontal");
        float vertical = Input.GetAxisRaw(playerInputPrefix + "Vertical");

        dashDirection = new Vector3(horizontal, vertical, 0f).normalized;

        if (canBlink)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && playerInputPrefix == "Player1")
            {
                StartCoroutine(BlinkCoroutine());
                imageSkill.fillAmount = 0;
                animatorBlink.SetBool("Blink", true);
                teleportSFX.Play();

            }
            else if (Input.GetKeyDown(KeyCode.PageDown) && playerInputPrefix == "Player2")
            {
                StartCoroutine(BlinkCoroutine());
                imageSkill.fillAmount = 0;
                animatorBlink.SetBool("Blink", true);
                teleportSFX.Play();
            }
        }

        if (!canBlink)
        {
            imageSkill.fillAmount += 1 / blinkCooldown * Time.deltaTime;
            if (imageSkill.fillAmount <= 0)
            {
                imageSkill.fillAmount = 1;
            }
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        canBlink = false;

        spriteRenderer.color = blinkColor;
        yield return new WaitForSeconds(blinkDuration / 2);

        //Blink
        animatorBlink.SetTrigger("Blink");
        spriteRenderer.enabled = false;
        transform.Translate(dashDirection * blinkDistance);
        yield return new WaitForSeconds(blinkDuration);

        //Balek warna dan renderernya
        spriteRenderer.enabled = true;
        spriteRenderer.color = originalColor;

        //Cooldown
        yield return new WaitForSeconds(blinkCooldown);
        canBlink = true;
    }

}

