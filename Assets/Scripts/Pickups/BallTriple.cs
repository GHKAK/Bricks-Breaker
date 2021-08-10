using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriple : Pickup {
    [SerializeField] static GameObject ballPrefab;
    private void Start() {
    }
    protected override void GetBonus() {
        //if(collision.gameObject.TryGetComponent<Paddle>(out _)){ 
        foreach(var ball in BallsController.Balls) {
            BallsController.Instance.AddBall(ball.transform.position + Vector3.up, Vector3.up);
            BallsController.Instance.AddBall(ball.transform.position + (Vector3)Vectors.downLeft, (Vector3)Vectors.downLeft);
            BallsController.Instance.AddBall(ball.transform.position + (Vector3)Vectors.downRight, (Vector3)Vectors.downRight);
        }
        BallsController.Instance.UpdateBalls();
    }
    //}
}


