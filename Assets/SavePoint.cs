using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private void Start()
    {
        if (SceneController.instance.data.sceneIndex == SceneManager.GetActiveScene().buildIndex && SceneController.instance.data.respawnPoint == (Vector2) transform.position)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Save");
            SceneController.instance.Save(transform.position);
            gameObject.SetActive(false);
        }
    }
}
