using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private int jumpPower;
    [SerializeField] private int speed;
    private bool isJumping;
    private bool isGround;
    private int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(jumpCount < 2)
            {
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                isJumping = true;
                isGround = false;
                jumpCount++;
            }
            else if(isGround == false && jumpCount > 2)
            {
                isJumping = false;
            }
        }
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isGround = true;
            jumpCount = 0;
        }
    }
}
