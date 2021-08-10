using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDestroyer : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.SetActive(false);
    }
}
