using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationMap : MonoBehaviour
{
    [SerializeField] private GameObject Minimap;
    private int pressCount;

    // Start is called before the first frame update
    void Start()
    {
        pressCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (pressCount == 1)
            {
                Minimap.SetActive(true);
                pressCount--;
                Debug.Log(pressCount);
            }
            else if (pressCount == 0)
            {
                Minimap.SetActive(false);
                pressCount++;
                Debug.Log(pressCount);
            }
        }
    }
}
