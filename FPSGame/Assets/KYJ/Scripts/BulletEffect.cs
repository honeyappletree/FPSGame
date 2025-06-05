using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(EffectReturn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EffectReturn()
    {
        yield return new WaitForSeconds(0.3f);
        EffectPool.ReturnObject(this);
        Debug.Log("이펙트가 반환되었습니다.");
    }
}
