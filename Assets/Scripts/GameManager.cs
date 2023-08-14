using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private GameObject BlackObj;
    [SerializeField] private GameObject PinkObj;
    [SerializeField] private GameObject BlueObj;
    [SerializeField] private GameObject YellowObj;

    void Awake()
    {
        Instance = this;
    }

    public void Retry()
    { // if game is over or end, player restart game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ObjectActive(int value)
    { // object active by playerStance
        switch(value)
        {
            case 0:
                
                BlackObj.SetActive(true);
                PinkObj.SetActive(false);
                BlueObj.SetActive(false);
                YellowObj.SetActive(false);
                break;
            case 1:
                BlackObj.SetActive(true);
                PinkObj.SetActive(false);
                BlueObj.SetActive(true);
                YellowObj.SetActive(true);
                break;
            case 2:
                BlackObj.SetActive(true);
                PinkObj.SetActive(true);
                BlueObj.SetActive(false);
                YellowObj.SetActive(true);
                break;
            case 3:
                BlackObj.SetActive(true);
                PinkObj.SetActive(true);
                BlueObj.SetActive(true);
                YellowObj.SetActive(false);
                break;
        }
    }
}
