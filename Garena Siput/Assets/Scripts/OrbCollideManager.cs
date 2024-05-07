using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollideManager : MonoBehaviour
{

    private DashSkill dashSkillScript;
    private Movement movementScript;
    public Canvas canvasInverseAnnouncement;

    public bool influenced;
    public GameObject influencer;
    public GameObject canvasReverse;
    public AudioSource audioKenaPower;
    private void Start()
    {
        dashSkillScript = GetComponent<DashSkill>();
        movementScript = GetComponent<Movement>();
    }

    private void Update()
    {
        if (influenced)
        {
            influencer.SetActive(true);
        }
        else
        {
            influencer.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DashOrb"))
        {
            dashSkillScript.canDash = true;
            dashSkillScript.dashTimer = 2;
            collision.gameObject.SetActive(false);
            audioKenaPower.Play();
        }

        if (collision.CompareTag("ReverseOrb"))
        {
            collision.gameObject.SetActive(false);
            InverseMovement();
            audioKenaPower.Play();
        }

        if (collision.CompareTag("SpeedOrb"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(BoostMovement());
            audioKenaPower.Play();
        }

        if (collision.CompareTag("RangeOrb"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(BoostRange());
            audioKenaPower.Play();
        }

        if (collision.CompareTag("RandomOrb"))
        {
            collision.gameObject.SetActive(false);
            RandomPower();
            audioKenaPower.Play();
        }

    }

    private void InverseMovement()
    {
        if(movementScript.inverted == true)
        {
            movementScript.inverted = false;
            canvasInverseAnnouncement.enabled = false;
            canvasReverse.SetActive(false);
            influenced = false;
        }
        else if(movementScript.inverted == false)
        {
            movementScript.inverted = true;
            canvasInverseAnnouncement.enabled = true;
            canvasReverse.SetActive(true);
            influenced = true;
            StartCoroutine(reversestyle());
        }
    }
    
    private IEnumerator reversestyle()
    {
        yield return new WaitForSeconds(5);
        movementScript.inverted = false;
        canvasInverseAnnouncement.enabled = false;
        canvasReverse.SetActive(false);
        influenced = false;
    }

    public void BoostMovementNormal()
    {
        StartCoroutine(BoostMovement());
    }

    public void BoostRangeNormal()
    {
        StartCoroutine(BoostRange());
    }
    private IEnumerator BoostMovement()
    {
        Debug.Log("SpeedBoosted !!");
        movementScript.moveSpeed *= 1.2f;
        influenced = true;
        yield return new WaitForSeconds(5f);
        influenced = false;
        Debug.Log("Speed Back to Normal !!");
        movementScript.moveSpeed /= 1.2f;
    }

    private IEnumerator BoostRange()
    {
        Debug.Log("RangeBoosted !!");
        dashSkillScript.dashDuration *= 2f;
        influenced = true;
        yield return new WaitForSeconds(6f);
        Debug.Log("Range Back to Normal !!");
        influenced = false;
        dashSkillScript.dashDuration /= 2f;
    }

    private void EmptyFunction()
    {
        Debug.Log("EmptyFunction");
    }

    private void RandomPower()
    {
        System.Action[] functions = {EmptyFunction, BoostMovementNormal,BoostRangeNormal};

        int randomIndex = Random.Range(0, functions.Length);

        functions[randomIndex]();
    }

}
