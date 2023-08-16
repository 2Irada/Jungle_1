using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public SaveData data;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void Save(Vector2 respawnPoint)
    {
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

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
