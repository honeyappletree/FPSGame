using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WaterBomb : MonoBehaviour
{
    ParticleSystem waterSplash;

    void Awake()
    {
        waterSplash = GetComponentInChildren<ParticleSystem>();
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
