using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriple : Pickup {
    [SerializeField] static GameObject ballPrefab;
    [SerializeField] AudioSource source;
    List<GameObject> activeBallsStart;
    protected override void GetBonus() {
        source.Play();
        activeBallsStart = new List<GameObject>();
        foreach(var ball in BallsPool.Instance.pooledBalls) {
            if(ball.activeInHierarchy) {
                activeBallsStart.Add(ball);
            }
        }
        StartCoroutine(ActiveAllBalls());
       // StartCoroutine(ActiveBalls(0));
    }
    IEnumerator ActiveBalls(int numberStart) {
        int i;
        for(i = numberStart; i < numberStart + 3; i++) {
            if(i >= activeBallsStart.Count) {
                break;
            }
            if(activeBallsStart[i].activeInHierarchy) {
                Vector2 direction = activeBallsStart[i].GetComponent<Ball>().direction;
                BallsPool.Instance.AddBall(activeBallsStart[i].transform.position, -direction);

                BallsPool.Instance.AddBall(activeBallsStart[i].transform.position, Vector2.Perpendicular(direction));
                BallsPool.Instance.AddBall(activeBallsStart[i].transform.position, -Vector2.Perpendicular(direction));
            }
        }
        yield return null;
        //yield return new WaitForFixedUpdate();
        if(i < activeBallsStart.Count) {
            StartCoroutine(ActiveBalls(i));
        } else {
            yield return new WaitForSeconds(source.clip.length);
            Destroy(gameObject);
        }

    }
    IEnumerator ActiveAllBalls() {
        foreach(var ball in activeBallsStart) {
            if(ball.activeInHierarchy) {
                Vector2 direction = ball.GetComponent<Ball>().direction;
                BallsPool.Instance.AddBall(ball.transform.position, -direction);
                BallsPool.Instance.AddBall(ball.transform.position, Vector2.Perpendicular(direction));
                BallsPool.Instance.AddBall(ball.transform.position, -Vector2.Perpendicular(direction));
            }
        }
        yield return null;
    }
    //}
}


