using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    BoxCollider2D boxCollider;
    [SerializeField] float speed = 2;
    Vector3 direction = Vector3.up;
    List<Collider2D> colliders = new List<Collider2D>();
    private bool isTriggered = false;
    float timeToRedirection;
    private void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start() {
    }
    void Update() {
        if(colliders.Count > 0) {
            SetNewDirection();
            colliders.Clear();
            boxCollider.enabled = false;
            StartCoroutine(Wait());
        }
        transform.Translate(direction * Time.deltaTime * speed);
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = true;
    }
    private void FixedUpdate() {
        if(!boxCollider.enabled) {
        }

    }
    void OnTriggerEnter2D(Collider2D collision) {
        colliders.Add(collision);
    }
    void OnCollisionEnter2D(Collision2D collision) {
        Vector3 normal = collision.contacts[0].normal;
        direction = Vector3.Scale(direction, normal);
    }
    void SetTime() {
        
    }
    void SetNewDirection() {
        Collider2D closestCollider = colliders[0];
        foreach(var collider in colliders) {
            if(Vector3.Distance(closestCollider.transform.position, transform.position) > Vector3.Distance(collider.transform.position, transform.position)) {
                closestCollider = collider;
            }
        }
        //isTriggered = true;
        if(closestCollider.TryGetComponent<Paddle>(out _)) {
            direction = (gameObject.transform.position - closestCollider.transform.position).normalized;
        } else {
            direction = Vector2.Reflect(direction, GetNormal(closestCollider));// new Vector3(hit.normal.x +hit.point.x - direction.x, hit.normal.y+ hit.point.y).normalized;
        }
    }
    Vector2 GetNormal(Collider2D collision) {
        float angle = Vector2.SignedAngle(collision.gameObject.transform.position - transform.position,Vector2.right);
        if((angle >0 && angle < 45) || (angle > -45&&angle<0)) {
            return Vector2.right;
        } else if(angle < 135 && angle > 45) {
            return Vector2.up;
        } else if(angle > 135 || angle <-135) {
            return Vector2.left;
        } else {
            return Vector2.down;
        }

    }
}
