using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    int layerRaycast;
    public CircleCollider2D collider;
    [SerializeField] float speed = 2;
    public Vector2 direction;
    RaycastHit2D previousHit;
    RaycastHit2D hit ;
    private bool isCollide = false;
    private void Awake() {
        layerRaycast = LayerMask.GetMask("Default");
        collider = GetComponentInChildren<CircleCollider2D>();
        //if(gameObject.activeInHierarchy) {
        //    SetTarget();
        //}
    }
    private void Start() {
    }
    private void Update() {
        if(!isCollide) {
            transform.Translate(direction * Time.deltaTime * speed);
        }
        if(transform.position.y < -29) {
            collider.enabled = true;
        } else {
            collider.enabled = false;
        }

    }
    public void SetDirection(Vector2 newDirection) {
        direction = newDirection;
        SetTarget();
    }
    public void PaddleCollision(Vector3 paddlePosition) {
        direction = (transform.position - (Vector3)direction * gameObject.transform.localScale.x - paddlePosition).normalized;
        StopAllCoroutines();
        SetTarget();
    }
    void SetTarget() {
        StopAllCoroutines();
        //hit = Physics2D.CircleCast(transform.position,0.35f,direction,100, layerRaycast);
        previousHit = hit;
        hit = Physics2D.Raycast(transform.position,  direction, 100, layerRaycast);
        //if(previousHit&& previousHit.rigidbody == hit.rigidbody) {
        //    gameObject.SetActive(false);
        //    return;
        //}
        isCollide = false;
        StartCoroutine(WaitDistance());

    }
    IEnumerator WaitDistance() {
        yield return new WaitForSeconds((Vector2.Distance(hit.point, transform.position) - gameObject.transform.localScale.x) / speed);
       // transform.Translate(direction * Time.deltaTime/2 * speed);
        isCollide = true;
        float timeStart = Time.time;
        Vector2 previousDirection = direction;
        Vector2 normal = Vector2.zero;
        if(hit.collider!=null) {
            
            if(hit.collider.TryGetComponent(out Target target)) {   
                if(target.isDestroyed) {
                    SetTarget();
                } else {
                    target.isDestroyed = true;
                    normal = GetNormal();
                    direction = Vector2.Reflect(direction, normal);
                    target.SetInactive();
                }            
            } else {
                normal = GetNormal();
                direction = Vector2.Reflect(direction, normal);
            }
            transform.position = hit.centroid-previousDirection * 0.3f/* + normal * gameObject.transform.localScale.x*/;
        }
        SetTarget();
    }
    Vector2 GetNormal() {
        var vector = (Vector3)hit.point - hit.collider.transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, vector);
        angle = Mathf.RoundToInt(angle);
        var circleHits = Physics2D.CircleCastAll(hit.point, gameObject.transform.localScale.x / 10, direction, 0.05f, layerRaycast);

        if(angle % 45 == 0 && angle % 90 != 0 && angle != 0) {
            RaycastHit2D[] arrayWithHit = new RaycastHit2D[circleHits.Length + 1];
            arrayWithHit[0] = hit;
            circleHits.CopyTo(arrayWithHit,1);
            if(circleHits.Length == 1) {
                angle = Vector2.SignedAngle( Vector2.right,hit.rigidbody.transform.position - (Vector3)(hit.point - direction));
            } else {
                Vector2 midPoint = MidPoint2(circleHits);

                angle = Vector2.SignedAngle(midPoint - hit.point, Vector2.right);
            }
        }
        foreach(var circleHit in circleHits) {
            if(circleHit.rigidbody != hit.rigidbody && circleHit.rigidbody.gameObject.TryGetComponent(out Target target)) {
                target.isDestroyed = true;
                target.SetInactive();
            }
        }
        if((angle >= 0 && angle < 45) || (angle > -45 && angle <= 0)) {
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
            midPoint = Vector2.zero;
        }
        return midPoint;


    }


}

