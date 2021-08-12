using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeLaunchPickup : Pickup {
    [SerializeField] AudioSource source;
    protected override void GetBonus() { 
        BallsPool.Instance.AddBall(Paddle.Instance.gameObject.transform.position + Vector3.up, Vector3.up);
        BallsPool.Instance.AddBall(Paddle.Instance.gameObject.transform.position + Vector3.up, Vectors.topLeft);
        BallsPool.Instance.AddBall(Paddle.Instance.gameObject.transform.position + Vector3.up, Vectors.topRight);
        StartCoroutine(WaitSoundPlay());
    }
    IEnumerator WaitSoundPlay() {
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        Destroy(gameObject);
    }
}

