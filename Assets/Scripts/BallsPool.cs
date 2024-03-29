using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsPool : MonoBehaviour {


    public static BallsPool Instance;
    public List<GameObject> pooledBalls;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] int amountToPool = 50000;

    void Awake() {
        Instance = this;
        pooledBalls = new List<GameObject>();
        GameObject ball;
        for(int i = 0; i < amountToPool; i++) {
            ball = Instantiate(ballPrefab);
            ball.SetActive(false);
            pooledBalls.Add(ball);
        }
    }

    void Start() {

       //EntryForTests();
    }
    public GameObject GetPooledBall() {
        for(int i = 0; i < amountToPool; i++) {
            if(!pooledBalls[i].activeInHierarchy) {
                return pooledBalls[i];
            }
        }
        return null;
    }
    public void AddBall(Vector2 startPosition, Vector2 startDirection) {
        GameObject ball = GetPooledBall();
        if(ball != null) {
            ball.transform.position = startPosition;
            ball.SetActive(true);
            ball.GetComponent<Ball>().SetDirection(startDirection);
        }
    }
    void EntryForTests() {
        var balls = FindObjectsOfType<Ball>();
        foreach(var ball in balls) {
            pooledBalls.Add(ball.gameObject);
        }
        for(int i = 0; i < 6; i++) {

            pooledBalls[i].SetActive(true);
            pooledBalls[i].GetComponent<Ball>().SetDirection((Vector2.up + Vector2.right).normalized);
        }
    }
}