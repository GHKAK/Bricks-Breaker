using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuHandler : MonoBehaviour {
    [SerializeField] GameObject PauseMenu;
    public void Pause() {
        Paddle.Instance.enabled = false;
        var audioListener = FindObjectOfType<AudioListener>();
        audioListener.enabled = false;
        LevelStart levelStart =FindObjectOfType<LevelStart>();
        if(levelStart != null) {
            FindObjectOfType<LevelStart>().enabled = false;
        }
        Time.timeScale = 0;
        PauseMenu.gameObject.SetActive(true);
    }
    private void OnApplicationPause(bool pause) {
        Pause();
    }
}
