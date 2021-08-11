using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public bool isDestroyed=false;
    public void SetInactive() {
        gameObject.SetActive(false);
        if(Random.Range(0, 99) < 1) {
            var pickups = PickupsController.Instance.pickups;
            GameObject pickup = pickups[Random.Range(0, pickups.Length)];
            Instantiate(pickup, transform.position,pickup.transform.rotation);
        }
    }
}
