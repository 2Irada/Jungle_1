using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public SaveData data;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (data.sceneIndex != SceneManager.GetActiveScene().buildIndex) ColorManager.instance.SwitchMainColoring(Coloring.Red);
        else Load();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
        else if (Input.GetKeyDown(KeyCode.F11)) 
        {
            ResetSavedata();
        }
    }

    public void Save(Vector2 respawnPoint)
    {
        //sceneIndex 저장
        data.sceneIndex = SceneManager.GetActiveScene().buildIndex;

        //respawnPoint 저장
        data.respawnPoint = respawnPoint;

        //jellyColoring 저장
        JellyShooter _js = FindObjectOfType<JellyShooter>();
        Coloring _coloring = _js.jellyColoring;
        if (_js.jelliedObject != null) _coloring = _js.jelliedObject.objectColoring;
        data.jellyColoring = _coloring;

        //mainColoring 저장
        data.mainColoring = ColorManager.instance.mainColoring;

        //isEyeBall 리스트 저장
        data.isEyeBallList.Clear();
        List<ColoredObject> _tempList = new List<ColoredObject>();
        _tempList.AddRange(FindObjectsOfType<ColoredObject>());
        for (int i = 0; i < _tempList.Count; i++)
        {
            data.isEyeBallList.Add(_tempList[i].isEyeball);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Load()
    {
        if (data.sceneIndex != SceneManager.GetActiveScene().buildIndex) return;

        JellyShooter _js = FindObjectOfType<JellyShooter>();
        //플레이어 이동
        _js.transform.position = data.respawnPoint;
        //젤리 색 변경
        _js.SetJellyColoring(data.jellyColoring);

        //배경 색 변경
        ColorManager.instance.SwitchMainColoring(data.mainColoring);

        //isEyeBall 적용
        List<ColoredObject> _coloredObjects = new List<ColoredObject>();
        _coloredObjects.AddRange(FindObjectsOfType<ColoredObject>());
        if (_coloredObjects.Count != data.isEyeBallList.Count)
        {
            Debug.LogWarning("Save data and actual colored object count does not match.");
            return;
        }
        for (int i = 0; i < _coloredObjects.Count; i++)
        {
            _coloredObjects[i].startAsEyeball = data.isEyeBallList[i];
            _coloredObjects[i].InitEyeball();
        }
    }

    void ResetSavedata()
    {
        data.sceneIndex = -1;
        data.deathCount = 0;
    }

    public void NextLevel()
    {
        if (SceneManager.sceneCount - 1 > SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
