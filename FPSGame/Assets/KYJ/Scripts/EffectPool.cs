using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public static EffectPool instance; //ΩÃ±€≈Ê
    [SerializeField] private GameObject effectPrefab; //¿Ã∆Â∆Æ ø¿∫Í¡ß∆Æ
    [SerializeField] private Transform bulletPosition;
    Queue<BulletEffect> effectPrefabQueue = new Queue<BulletEffect>();

    private void Awake()
    {
        instance = this;
        Initialize(10);
    }

    void Initialize(int initCount)
    {
        for(int i = 0; i < initCount; i++)
        {
            effectPrefabQueue.Enqueue(CreatNewObject());
        }
    }

    private BulletEffect CreatNewObject()
    {
        var newObject = Instantiate(effectPrefab, bulletPosition.transform.position, Quaternion.identity).GetComponent<BulletEffect>();
        newObject.gameObject.SetActive(false);
        newObject.transform.SetParent(transform);
        return newObject;
    }

    public static BulletEffect GetObject()
    {
        if(instance.effectPrefabQueue.Count > 0 && PlayerFire.canShoot == true)
        {
            var obj = instance.effectPrefabQueue.Dequeue();
            obj.transform.SetParent(instance.transform);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instance.CreatNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(instance.transform);
            return newObj;
        }
    }

    public static void ReturnObject(BulletEffect obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.effectPrefabQueue.Enqueue(obj);
    }
}
