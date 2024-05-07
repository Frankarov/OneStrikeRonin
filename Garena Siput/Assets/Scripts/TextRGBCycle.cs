using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextRGBCycle : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public Color[] colors;
    public float colorChangeInterval = 2f;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        if (textMeshPro == null)
        {
            Debug.LogError("No TMGUI FOUND");
        }

        StartCoroutine(CycleColors());
    }

    IEnumerator CycleColors()
    {
        if (colors.Length < 2)
        {
            Debug.LogError("Warna");
            yield break;
        }

        int currentIndex = 0;

        while (true)
        {
            textMeshPro.color = colors[currentIndex];

            yield return new WaitForSecondsRealtime(colorChangeInterval);

            currentIndex = (currentIndex + 1) % colors.Length;
        }
    }
}
