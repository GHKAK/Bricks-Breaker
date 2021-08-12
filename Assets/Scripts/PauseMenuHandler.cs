using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour {
    public void Continue() {
        FindObjectOfType<Paddle>().enabled = true;
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Paddle.Instance.enabled = true;
        var camera = FindObjectOfType<Camera>();
        if(camera.gameObject.TryGetComponent(out AudioListener audioListener)) {
            audioListener.enabled = true;
        }

        LineRenderer levelStartLine = FindObjectOfType<LineRenderer>();
        if(levelStartLine != null) {
            LevelStart levelStart = levelStartLine.gameObject.GetComponent<LevelStart>();
            if(levelStart != null) {
                FindObjectOfType<LevelStart>().enabled = true;
            }
        }

    }
    public void Home() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
