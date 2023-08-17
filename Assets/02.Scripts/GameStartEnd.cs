using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartEnd : MonoBehaviour
{
    
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Ending") StartCoroutine(Ending());
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    public void gameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif       
    }
}