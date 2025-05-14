using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private int jumpPower;
    [SerializeField] private int speed = 6;
    private bool isJumping;
    private bool isGround;
    private int jumpCount;
    private float currentTime = 0;
    [SerializeField] private Image speedUP;
    [SerializeField] private TextMeshProUGUI coolTimeText;
    private float coolTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        speedUP.GetComponent<Image>();
        coolTimeText.color = new Color(1, 1, 1, 0);
        StartCoroutine(SkillCooltime());
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

    IEnumerator SkillCooltime()
    {
        while(coolTime > 0)
        {
            coolTime -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(speedUP.fillAmount < 0)
                {
                    speedUP.color = new Color(0.66f, 0.66f, 0.66f, 1);
                    coolTimeText.color = new Color(1, 1, 1, 1);
                    speedUP.fillAmount = (int)currentTime / (int)coolTime;
                    coolTimeText.text = coolTime.ToString();
                    Debug.Log("able");
                }
                else if(speedUP.fillAmount == 0)
                {
                    speedUP.color = new Color(1, 1, 1, 1);
                    coolTimeText.color = new Color(1, 1, 1, 0);
                    Debug.Log("disable");
                }
                yield return null;
            }
        }
        
    }
}
