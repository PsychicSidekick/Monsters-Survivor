using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerSound : MonoBehaviour
{
    public AudioClip meteorSFX;
    public float playRate;

    private void Start()
    {
        StartCoroutine(RepeatMeteorSFX());
    }

    IEnumerator RepeatMeteorSFX()
    {
        while(true)
        {
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1 / playRate);
        }
    }
}
