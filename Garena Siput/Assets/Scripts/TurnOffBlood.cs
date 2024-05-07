using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBlood : MonoBehaviour
{

    public GameObject animasiBloodSplat;
    public void TurnOffBlinkAnimEvent()
    {
        animasiBloodSplat.SetActive(false);
    }


}
