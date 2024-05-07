using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    InputAction playerInputAction;
    public Movement movement;
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
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        imageSkill.fillAmount = 1;
    }

    void Update()
    {

        dashDirection = movement.input;



        if (!canBlink)
        {
            imageSkill.fillAmount += 1 / blinkCooldown * Time.deltaTime;
            if (imageSkill.fillAmount <= 0)
            {
                imageSkill.fillAmount = 1;
            }
        }
    }

    public void OnBlink()
    {
        if (canBlink)
        {
            StartCoroutine(BlinkCoroutine());
            imageSkill.fillAmount = 0;
            animatorBlink.SetBool("Blink", true);
            teleportSFX.Play();
        }
    }
    public void OnBlinkPlayer2()
    {
        if (canBlink)
        {
            StartCoroutine(BlinkCoroutine());
            imageSkill.fillAmount = 0;
            animatorBlink.SetBool("Blink", true);
            teleportSFX.Play();
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

