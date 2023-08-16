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

    public GameObject gameOverBG;
    public TextMeshProUGUI pressSpace;
    public TextMeshProUGUI ingKu;
    private bool _isFadeIn;
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
        pressSpace.color = new Color(255, 255, 255, 255);
        ingKu.color = new Color(255, 255, 255, 0);
        _isFadeIn = true;
    }

    private void Update()
    {
        if (_isGameEnd && !_isFadeIn) StartCoroutine(StartFadeIn());
        else StartCoroutine(StartFadeOut());

        if (_isGameEnd && Input.GetKeyDown(KeyCode.Space) && !plusIngku)
        {
            StopAllCoroutines();

            gameOverBG.SetActive(false);

            
            StartCoroutine(StartIngKu());            
        }
    }

    IEnumerator StartFadeIn()
    {
        yield return new WaitForSeconds(1.0f);
        pressSpace.color = new Color(255, 255, 255, 255);
        _isFadeIn = true;
    }

    IEnumerator StartFadeOut()
    {
        yield return new WaitForSeconds(1.0f);
        pressSpace.color = new Color(255, 255, 255, 0);
        _isFadeIn = false;
    }

    IEnumerator StartIngKu()
    {
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
