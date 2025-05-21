using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName; //아이템의 이름
    public string[] part; //hp를 회복할 것이라는 표시
    public int[] num; //hp회복량
}

public class ItemDataBase : MonoBehaviour
{
    [SerializeField] private PlayerHP PlayerHP;
    [SerializeField] private ItemEffect[] effect; //회복 아이템이 2개이므로 배열 사용

    private const string HP = "HP";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem(ItemSO _item)
    {
        for(int i = 0; i < effect.Length; i++)
        {
            if (effect[i].itemName == _item.itemName) //ItemSO 스크립트의 아이템 이름과 effect 배열 속 아이템 이름과 같을 때
            {
               for(int j = 0; j < effect[i].part.Length; j++)
               {
                    switch (effect[i].part[j])
                    {
                        case HP:
                            PlayerHP.IncreaseHP(effect[i].num[j]);
                            break;
                        default:
                            Debug.Log("디폴트로 간주됨.");
                            break;
                    }
                    Debug.Log(_item.itemName + "을 사용했습니다.");
               }
            }
            
        }
        return;
    }
}
