using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour {
    [SerializeField] GameObject ballPrefab;
    public static BallsController Instance;
    public static List<Ball> Balls { get; private set; }
    private void Awake() {
        Balls = new List<Ball>();
        
        Instance = this ;
        UpdateBalls();
    }
    //void Start() {
    //    Balls.Add(FindObjectOfType<Ball>());
    //}
    public void AddBall(Vector2 startPosition, Vector2 startDirection) {
        var ballObject = Instantiate(ballPrefab, startPosition, ballPrefab.transform.rotation);
        Ball ball = ballObject.GetComponent<Ball>();
        ball.SetDirection(startDirection);
    }
    public void UpdateBalls() {
        var balls = FindObjectsOfType<Ball>();
        foreach(var ball in balls) {
            Balls.Add(ball);
        }
    }
}
