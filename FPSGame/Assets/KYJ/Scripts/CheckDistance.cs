using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{
    [SerializeField] private float range;
    private bool canPickup = false;
    private RaycastHit hitInfo;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TextMeshProUGUI pickupText;
    [SerializeField] private Inventory theInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryAction();
    }

    void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            CheckItem();
            CanPickUp();
        }
    }

    void CheckItem()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    void ItemInfoAppear()
    {
        canPickup = true;
        pickupText.gameObject.SetActive(true);
    }

    void ItemInfoDisappear()
    {
        canPickup = false;
        pickupText.gameObject.SetActive(false);
    }

    void CanPickUp()
    {
        if(canPickup)
        {
            if(hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPick>().item.itemName + "»πµÊ«ﬂΩ¿¥œ¥Ÿ.");
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPick>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }
}
