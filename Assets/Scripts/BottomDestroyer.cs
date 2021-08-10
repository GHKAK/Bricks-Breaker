using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDestroyer : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.parent == null) { 
            Destroy(other.gameObject); 
        } else {
            if(other.transform.parent.gameObject.TryGetComponent(out Ball ball)) {
                ball.StopAllCoroutines();
            }
            other.transform.parent.gameObject.SetActive(false);
        }
    }
}
