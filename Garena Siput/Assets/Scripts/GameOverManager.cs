using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void over()
    {
        StartCoroutine(overr());
    }
    public IEnumerator overr()
    {
        yield return new WaitForSeconds(14.4f);
        SceneManager.LoadScene("Playground");
    }
    


}
