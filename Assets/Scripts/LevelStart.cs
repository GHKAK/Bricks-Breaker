using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour {
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Paddle paddle;
    [SerializeField] Vector2 startPosition = new Vector2(0f, -29);
    [SerializeField] Material material;
    Camera mainCamera;
    Ball ball;
    GameObject ballObject;
    bool isSwiping = false;
    Vector2 direction;
    private void Awake() {
        paddle.enabled = false;
        mainCamera = FindObjectOfType<Camera>();
    }
    private void Start() {
        ballObject = BallsPool.Instance.GetPooledBall();
        ball=ballObject.GetComponent<Ball>();
        ballObject.transform.position = startPosition;
        ball.collider.enabled = false;
        ballObject.SetActive(true);
    }
    void Update() {
        if(Input.GetMouseButtonDown(0) ) {
            isSwiping = true;
        } else if(Input.GetMouseButtonUp(0) ) {
            isSwiping = false;
            LaunchBall();
            paddle.enabled = true;
            gameObject.SetActive(false);
            
        }
        if(isSwiping) {
            GetDirection();
            RenderTrajectory();
        }
    }
    void GetDirection() {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        if(mousePosition.y < -28) {
            paddle.gameObject.transform.position = new Vector2(mousePosition.x, paddle.gameObject.transform.position.y);
            ballObject.transform.position = new Vector2(mousePosition.x, ballObject.transform.position.y);
            direction = Vector2.up;
        } else if(mousePosition.y < 40){ 
            paddle.gameObject.transform.position = new Vector2(0, paddle.gameObject.transform.position.y);
            ballObject.transform.position = new Vector2(0, ballObject.transform.position.y);
            direction = (mousePosition - (Vector2)ballObject.transform.position).normalized;
        }
    }
    void RenderTrajectory() {
        var hit = Physics2D.CircleCast(ball.transform.position, ball.transform.localScale.x / 2, direction,100,1);
        float distance = Vector2.Distance(ball.transform.position, hit.point) / 2 ;
        int countBeforeReflection = Mathf.RoundToInt(distance);
        lineRenderer.positionCount = countBeforeReflection ;
        for(int i = 0; i < countBeforeReflection; i++) {
            lineRenderer.SetPosition(i, ball.transform.position + (Vector3)direction * 2 * (i + 1));
        }
        material.SetFloat("_Rep", distance*2);
    }
    void LaunchBall() {
        ball.collider.enabled = true;
        ball.SetDirection(direction);
    }
}