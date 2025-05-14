using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    private int pressCount;

    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(false);
        pressCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (pressCount == 0)
            {
                inventory.SetActive(true);
                pressCount++;
                Time.timeScale = 0;
                Debug.Log(pressCount);
            }
            else if (pressCount == 1)
            {
                inventory.SetActive(false);
                pressCount--;
                Time.timeScale = 1;
                Debug.Log(pressCount);
            }
        }
    }
}
