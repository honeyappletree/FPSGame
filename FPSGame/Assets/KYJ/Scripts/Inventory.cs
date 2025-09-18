using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject back;
    private int pressCount;
    public static bool isActive;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
        isActive = false;
        slots = back.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isActive == false)
            {
                inventory.SetActive(true);
                Cursor.visible = true;
                isActive = true;
                Time.timeScale = 0;
                Debug.Log("인벤토리 활성화");
            }
            else if (isActive == true)
            {
                inventory.SetActive(false);
                Cursor.visible = false;
                isActive = false;
                Time.timeScale = 1;
                Debug.Log("인벤토리 비활성화");
            }
        }
    }

    public void AcquireItem(ItemSO _item)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].AddItem(_item);
                    slots[i].SetSlotCount(0);
                    itemNameText.text = " ";
                    itemDescription.text = " ";
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].ClearSlot();
                return;
            }
        }
    }
}
