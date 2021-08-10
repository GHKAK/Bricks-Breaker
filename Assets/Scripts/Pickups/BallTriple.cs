using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriple : Pickup {
    [SerializeField] static GameObject ballPrefab;
    List<GameObject> activeBallsStart;
    private void Start() {
    }
    protected override void GetBonus() {
        //if(collision.gameObject.TryGetComponent<Paddle>(out _)){ 
        activeBallsStart = new List<GameObject>();
        foreach(var ball in BallsPool.Instance.pooledBalls) {
            if(ball.activeInHierarchy) {
                activeBallsStart.Add(ball);
            }
        }
        foreach(var ball in activeBallsStart) {
            if(ball.activeInHierarchy) {
                Vector2 direction = ball.GetComponent<Ball>().direction;
                BallsPool.Instance.AddBall(ball.transform.position, -direction);

                BallsPool.Instance.AddBall(ball.transform.position, Vector2.Perpendicular(direction));
                BallsPool.Instance.AddBall(ball.transform.position, -Vector2.Perpendicular(direction));
            }
        }
    }
    //}
}


