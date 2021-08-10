using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCollider : MonoBehaviour {
    Paddle paddle;
    private void Start() {
        paddle = GetComponentInParent<Paddle>();
    }
    void OnTriggerEnter2D(Collider2D collision) {
        Ball ball = collision.gameObject.GetComponentInParent<Ball>();
        if(ball != null) {
            ball.PaddleCollision(paddle.gameObject.transform.position);
        }
    }
}
