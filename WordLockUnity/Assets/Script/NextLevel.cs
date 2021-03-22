using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScreenFader.screenFader.ChangeScene(nextScene);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        }
    }
}
