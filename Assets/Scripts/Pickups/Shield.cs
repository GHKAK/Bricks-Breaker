using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    public static Shield Instance;
    float time = 0;
    float progress = 0;
    float xScale;
    [SerializeField] float timeToDecrease = 1;
    [SerializeField] float decreaseTime = 5;
    private void Awake() {
        Instance = this;
        xScale = transform.localScale.x;
    }
    public void ResetShield() {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        SetScale(new Vector3(xScale, transform.localScale.y, transform.localScale.z));
        progress = 0;
        time = 0;
        StopAllCoroutines();
        StartCoroutine(SetShield());
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Ball ball = collision.gameObject.GetComponentInParent<Ball>();
        if(ball != null) {
            Vector2 previousDirection = ball.direction;
            ball.direction = Vector2.zero;
            ball.StopAllCoroutines();
            ball.transform.position -= (Vector3)previousDirection * (ball.transform.localScale.x / 2);
            ball.SetDirection(Vector2.Reflect(previousDirection, Vector2.up));

        }
    }
    IEnumerator SetShield() {
        yield return new WaitForSeconds(timeToDecrease);
        while(progress < 1) {
            time += Time.deltaTime;
            progress = time / decreaseTime;
            SetScale(new Vector3(xScale * (1 - progress)*0.9f, transform.localScale.y, transform.localScale.z));
            yield return new WaitForEndOfFrame();
        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
    void SetScale(Vector3 scale) {
        Vector3 localScale = transform.localScale;
        localScale.Set(scale.x, scale.y, scale.z);
        transform.localScale = localScale;
    }
}
