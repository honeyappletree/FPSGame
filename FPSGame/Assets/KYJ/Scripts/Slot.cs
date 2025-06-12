using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO item; //획득한 아이템
    private int itemCount;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private ItemDataBase itemDatabase;
    public static bool useItem = false;

    // Start is called before the first frame update
    void Start()
    {
        itemCount = 0;
        //itemImage.color = new Color(1, 1, 1, 0);
        itemNameText.text = " ";
        itemDescription.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemSO _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemIcon;
        itemImage.color = new Color(1, 1, 1, 1);
        Debug.Log("item 저장완료" + itemCount);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        Debug.Log("아이템 저장되지 않음");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (itemImage.sprite != null) //아이템 이미지가 활성화됐을 때 실행
            {
                itemNameText.text = item.itemName;
                itemDescription.text = item.description;
                Debug.Log("아이템 정보가 업데이트되었습니다.");
            }
            else if(itemImage.sprite == null) //아이템 이미지가 비활성화됐을 때 실행
            {
                itemNameText.text = " ";
                itemDescription.text = " ";
            }
        }

        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(itemImage != null)
            {
                itemDatabase.UseItem(item);
                SetSlotCount(-1);
                useItem = true;
                Debug.Log("아이템을 사용했습니다.");
            }
            else if(itemImage == null)
            {
                Debug.Log("아이템을 없어서 사용할 수 없습니다.");
            }
        }
    }
}
