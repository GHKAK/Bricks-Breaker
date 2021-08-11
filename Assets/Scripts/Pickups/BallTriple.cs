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
        StartCoroutine(ActiveBalls(0));

        //foreach(var ball in activeBallsStart) {
        //    if(ball.activeInHierarchy) {
        //        Vector2 direction = ball.GetComponent<Ball>().direction;
        //        BallsPool.Instance.AddBall(ball.transform.position, -direction);

        //        BallsPool.Instance.AddBall(ball.transform.position, Vector2.Perpendicular(direction));
        //        BallsPool.Instance.AddBall(ball.transform.position, -Vector2.Perpendicular(direction));
        //    }
        //}
    }
    IEnumerator ActiveBalls(int numberStart) {
        int i;
        for(i = numberStart; i < numberStart +10 && i < activeBallsStart.Count; i++) {
            if(activeBallsStart[i].activeInHierarchy) {
                Vector2 direction = activeBallsStart[i].GetComponent<Ball>().direction;
                BallsPool.Instance.AddBall(activeBallsStart[i].transform.position, -direction);

                BallsPool.Instance.AddBall(activeBallsStart[i].transform.position, Vector2.Perpendicular(direction));
                BallsPool.Instance.AddBall(activeBallsStart[i].transform.position, -Vector2.Perpendicular(direction));
            }
        }
        yield return new WaitForEndOfFrame();
        if(i < activeBallsStart.Count) { 
            StartCoroutine(ActiveBalls(i)); 
        }

    }
    //}
}


