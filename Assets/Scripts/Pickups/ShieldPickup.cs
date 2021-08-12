using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : Pickup {
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] AudioSource source;
    protected override void GetBonus() {
        Shield.Instance.ResetShield();
        StartCoroutine(WaitSoundPlay());
    }
    IEnumerator WaitSoundPlay() {
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        Destroy(gameObject);
    }
}
