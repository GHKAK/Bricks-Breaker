using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsController : MonoBehaviour {
    public static PickupsController Instance;
    [SerializeField] public GameObject[] pickups;
    private void Awake() {
        Instance = this;
    }
}
