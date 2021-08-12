using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {
    public void StartLevel() {
        SceneManager.LoadScene(1);
    }
}
