using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMove : MonoBehaviour
{
    private float mouse_X;
    private float mouse_Y;
    [SerializeField] private float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mouse_X += mouseX * speed * Time.deltaTime;
        mouse_Y += mouseY * speed * Time.deltaTime;

        mouse_Y = Mathf.Clamp(mouse_Y, -90f, 90f);
        mouse_X = Mathf.Clamp(mouse_X, -90f, 90f);
        transform.eulerAngles = new Vector3(-mouse_Y, mouse_X, 0);
    }
}
