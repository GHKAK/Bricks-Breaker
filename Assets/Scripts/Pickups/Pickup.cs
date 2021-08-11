using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public static float speed =10;
    protected abstract void GetBonus();
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent(out PaddleCollider _)) {
            GetBonus();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    private void Update() {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
