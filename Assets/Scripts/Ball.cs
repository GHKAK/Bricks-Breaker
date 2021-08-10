using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    int layerRaycast;
    [SerializeField] float speed = 2;
    Vector2 direction = Vector2.up;
    RaycastHit2D hit;
    private bool isCollide = false;
    private void Awake() {
        layerRaycast = LayerMask.GetMask("Default");
    }
    private void Start() {
        SetTarget();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Time.timeScale = 1;
        }
        if(!isCollide) {
            transform.Translate(direction * Time.deltaTime * speed);
        } else {
        }
    }
    public void SetDirection(Vector2 newDirection) {
        direction = newDirection;
    }
    public void PaddleCollision(Vector3 paddlePosition) {
        direction = (transform.position - (Vector3)direction * 0.5f - paddlePosition).normalized;
        StopAllCoroutines();
        SetTarget();
    }
    void SetTarget() {
        hit = Physics2D.Raycast(transform.position,/* Vector2.one, 0, */direction, 100, layerRaycast);

        isCollide = false;
        StartCoroutine(WaitDistance());

    }
    IEnumerator WaitDistance() {
        yield return new WaitForSeconds((Vector2.Distance(hit.point, transform.position) - 1) / speed);
        isCollide = true;
        float timeStart = Time.time;
        Vector2 previousDirection = direction;
        if(hit.collider != null) {
            if(hit.collider.TryGetComponent(out Target target)) {
                direction = Vector2.Reflect(direction, GetNormal());
                target.SetInactive();
            } else {
                direction = Vector2.Reflect(direction, GetNormal());
            }
            transform.position = hit.point - previousDirection * 0.5f;
        }
        SetTarget();
    }
    Vector2 GetNormal() {
        float angle = Vector2.SignedAngle(hit.collider.transform.position - (Vector3)hit.point, Vector2.right);
        angle = Mathf.RoundToInt(angle);
        if(angle % 45 == 0 && angle % 90 != 0 && angle != 0) {
            var circleHits = Physics2D.CircleCastAll(hit.point, 0.5f, direction, 0.05f, layerRaycast);
            if(circleHits.Length == 1) {
                angle = Vector2.SignedAngle(hit.collider.transform.position - (Vector3)(hit.point - direction), Vector2.right);
            } else {
                Vector2 midPoint = MidPoint2(circleHits);

                angle = Vector2.SignedAngle(midPoint - hit.point, Vector2.right);
            }
            foreach(var circleHit in circleHits) {
                if(circleHit.rigidbody != hit.rigidbody && circleHit.rigidbody.gameObject.TryGetComponent(out Target target)) {
                    target.SetInactive();
                }
            }
        }
        if((angle > 0 && angle < 45) || (angle > -45 && angle < 0)) {
            return Vector2.right;
        } else if(angle < 135 && angle > 45) {
            return Vector2.up;
        } else if(angle > -135 && angle < -45) {
            return Vector2.down;
        } else {
            return Vector2.left;
        }
    }
    Vector2 MidPoint2(RaycastHit2D[] hits) {
        Vector2 midPoint;
        try {
            midPoint = hits[0].rigidbody.transform.position;
            for(int i = 1; i < hits.Length; i++) {
                Vector2 second = hits[i].rigidbody.transform.position;
                Vector2 difference = midPoint - second;
                midPoint = midPoint - difference * 0.5f;

            }
        } catch(System.Exception) {
            StopAllCoroutines();
            SetTarget();
        }
        return midPoint;


    }


}

