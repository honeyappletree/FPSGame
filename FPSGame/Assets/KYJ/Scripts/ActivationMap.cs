using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationMap : MonoBehaviour
{
    [SerializeField] private GameObject Minimap;
    private int pressCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Minimap.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if (pressCount == 0)
            {
                OddNum(); //È¦¼ö¹ø ´­·¶À» ¶§
            }
            if(pressCount == 1)
            {
                EvenNum(); //Â¦¼ö¹ø ´­·¶À» ¶§
            }

        }
    }

    void OddNum()
    {
        Minimap.SetActive(true);
        pressCount++;
    }

    void EvenNum()
    {
        Minimap.SetActive(false);
        pressCount--;
    }
}
