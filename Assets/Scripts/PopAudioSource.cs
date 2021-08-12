using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopAudioSource : MonoBehaviour
{
    public static PopAudioSource Instance;
    [SerializeField] public  AudioSource audioSource;
    private void Awake() {
        Instance = this;
    }
}
