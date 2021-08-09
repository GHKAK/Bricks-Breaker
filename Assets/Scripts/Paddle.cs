using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    Camera camera;
    Vector2 mousePosition;
    bool isSwiping = false;
    void Start() {
        camera = Camera.main;
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0) /*|| Input.touches[0].phase == TouchPhase.Began*/) {
            isSwiping = true;
        } else if(Input.GetMouseButtonUp(0) /*|| Input.touches[0].phase == TouchPhase.Ended*/) {
            isSwiping = false;
        }
        if(isSwiping) {
            UpdateMousePosition();
        }
    }
    void UpdateMousePosition() {
        mousePosition = camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        if(Mathf.Abs(mousePosition.x) < 21) {
            transform.position = new Vector3(mousePosition.x, transform.position.y);

        }
    }
}
