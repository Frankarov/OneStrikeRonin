using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Health : MonoBehaviour
{
    public string playerInputPrefix;
    private DashSkill dashSkillScript;
    public TextMeshProUGUI lifeText;
    public int maxLives = 5;
    [SerializeField]
    private float currentLives;
    private BoxCollider2D ownCollider;
    private Animator animatorOwn;
    private bool gembok = false;
    public GameOverManager gameOverManagerScript;

    public GameObject animasiBloodSplat;
    public PlayableDirector timelineOver;

    public AudioSource audioDashHit;

    private void Start()
    {
        dashSkillScript = GetComponent<DashSkill>();
        currentLives = maxLives;
        UpdateLifeText();
        ownCollider = GetComponent<BoxCollider2D>();
        animatorOwn = GetComponent<Animator>();
    }




    private void UpdateLifeText()
    {
        lifeText.text = $"{currentLives}";
    }

    public void TakeDamage()
    {
        if (currentLives > 0)
        {
            StartCoroutine(FlashText());
            currentLives-=0.5f;
            UpdateLifeText();
        }

        animasiBloodSplat.SetActive(true);
        audioDashHit.Play();
    }

    private IEnumerator FlashText()
    {
        lifeText.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        lifeText.color = Color.white;

        StartCoroutine(ShakeText());

    }

    private IEnumerator ShakeText()
    {
        Vector3 originalPos = lifeText.rectTransform.localPosition;

        for (float t = 0.0f; t < 0.1f; t += Time.deltaTime * 10.0f)
        {
            lifeText.rectTransform.localPosition = originalPos + Random.insideUnitSphere * 50.0f;
            yield return null;
        }

        lifeText.rectTransform.localPosition = originalPos;
    }

    private void Update()
    {
        if(currentLives <= 0 && !gembok)
        {
            Debug.Log(playerInputPrefix + "is dead.");
            ownCollider.enabled = false;
            animatorOwn.SetTrigger("Die");
            gembok = true;
            StartCoroutine(DestroyHim());
            
        }
    }

    private IEnumerator DestroyHim()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        timelineOver.Play();
        gameOverManagerScript.over();
    }

}
