using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WaterBomb : MonoBehaviour
{
    ParticleSystem waterSplash;
    AudioSource audioSource;
    public AudioClip waterSound;

    void Awake()
    {
        waterSplash = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            waterSplash.Play();
            audioSource.PlayOneShot(waterSound);
            Invoke("DestroyWater", 0.3f);
        }
    }

    void DestroyWater()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject, 0.3f);
            Debug.Log("ºÒ ¼Ò¸ê");
        }
    }


}
