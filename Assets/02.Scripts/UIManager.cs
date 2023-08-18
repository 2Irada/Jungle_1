using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public bool _isGameEnd;

    public Image[] imageList;

    public GameObject GameOver;
    public TextMeshProUGUI ingKu;

    private bool plusIngku;
    public static int deathCount;


    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        imageList[0].color = ColorManager.instance.red;
        imageList[1].color = ColorManager.instance.green;
        imageList[2].color = ColorManager.instance.blue;
    }

    private void OnEnable()
    {
        plusIngku = false;
        ingKu.color = new Color(255, 255, 255, 0);
    }

    private void Update()
    {
        if (_isGameEnd && !plusIngku)
        {
            StopAllCoroutines();
            StartCoroutine(StartIngKu());
        }
    }

    public IEnumerator StartIngKu()
    {
        SceneController.instance.isRestart = true;
        if (!GameOver.activeSelf) GameOver.SetActive(true);

        plusIngku = true;
        deathCount++;
        string forIngKu = ingKu.text;
        for (int i = 1; i < deathCount; i++)
        {
            forIngKu += "ㅋ";
        }
        ingKu.text = forIngKu;
        ingKu.color = new Color(255, 255, 255, 255);

        yield return new WaitForSeconds(2.0f);
        plusIngku = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // 조정 후 세이브 포인트로 스폰하도록 변경
    }

    public void GameReset()
    {
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
}
