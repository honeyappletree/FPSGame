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
    private float currentTime;
    [SerializeField] private Image speedUP; //속도 올리는 스킬의 맨 윗부분 이미지
    [SerializeField] private TextMeshProUGUI coolTimeText1; //속도 증가 쿨타임 텍스트
    private float mouse_X; //플레이어가 mouse를 좌우로 움직이면 회전하기 위함.
    private float speedUp;
    private bool isCool = false;
    private float coolTime = 5;
    private float speedTimer = 2; //속도를 일시적으로 올리기 위한 타이머

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        speedUP.GetComponent<Image>();
        coolTimeText1.alpha = 0;
        currentTime = coolTime;
        transform.Translate(0, 1, -11);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Rotate();

        if(Input.GetKeyDown(KeyCode.LeftShift) && isCool == false) //플레이어의 속도 증가 쿨타임
        {
            isCool = true;
            currentTime = coolTime;
            StartCoroutine(SkillCooltime());
        }
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

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        mouse_X += mouseX * speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mouse_X, 0);
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
        isCool = true;

        speedTimer = 2;
        while(speedTimer > 0)
        {
            speedTimer -= Time.deltaTime;
            speed = 12;
            Debug.Log("speed: " + speed);
            yield return null;
        }
        speed = 6;
        Debug.Log("쿨타임 시작" + ", speed는 " + speed);

        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            speedUP.color = new Color(0.67f, 0.67f, 0.67f, 1);
            speedUP.fillAmount = (int)currentTime / coolTime;
            speedUp = speedUP.fillAmount * 5;
            coolTimeText1.alpha = 1;
            coolTimeText1.text = speedUp.ToString();

            yield return null;
        }
        coolTimeText1.alpha = 0;
        speedUP.color = new Color(1, 1, 1, 1);

        isCool = false;
    }

}
