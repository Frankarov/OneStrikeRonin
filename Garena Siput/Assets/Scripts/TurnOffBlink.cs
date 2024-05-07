using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBlink : MonoBehaviour
{
    public Animator animatorBlink;
    public void TurnOffBlinkAnimEvent()
    {
        animatorBlink.SetBool("Blink", false);
    }

}
