using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public static float speed =20;
    protected abstract void GetBonus();
    private void OnTriggerEnter2D(Collider2D collision) {
        GetBonus();
    }
    private void Update() {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
