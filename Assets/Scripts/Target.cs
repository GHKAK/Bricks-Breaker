using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    [SerializeField] AudioClip squarePop;
    public bool isDestroyed=false;
    AudioSource source;
    public void SetInactive() {
        source = PopAudioSource.Instance.audioSource;
        if(source.isPlaying) {
            source.loop = true;
        } else {
            source.Play();
        }

        StartCoroutine(WaitSoundPlay());

        GetComponent<BoxCollider2D>().enabled=false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;

        if(Random.Range(0, 99) < 5) {
            var pickups = PickupsController.Instance.pickups;
            GameObject pickup = pickups[Random.Range(0, pickups.Length)];
            Instantiate(pickup, transform.position,pickup.transform.rotation);
        }
    }
    IEnumerator WaitSoundPlay() {
        yield return new WaitForSeconds(squarePop.length);
        source.loop = false;
        gameObject.SetActive(false);
    }
}
