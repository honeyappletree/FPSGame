using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMove : MonoBehaviour
{
    private float mouse_Y;
    [SerializeField] private float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        mouse_Y += mouseY * speed * Time.deltaTime;

        mouse_Y = Mathf.Clamp(mouse_Y, -50f, 50f);
        transform.localRotation = Quaternion.Euler(-mouse_Y, 0f, 0f);
    }
}
