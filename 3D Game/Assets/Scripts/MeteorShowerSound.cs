using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerSound : MonoBehaviour
{
    public AudioClip meteorSFX;

    private void OnParticleCollision(GameObject other)
    {
        if (Random.Range(1, 100) < 80)
        {
            Player.instance.audioSource.PlayOneShot(meteorSFX);
        }
    }
}
