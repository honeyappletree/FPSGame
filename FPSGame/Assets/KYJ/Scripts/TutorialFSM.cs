using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialFSM : MonoBehaviour
{
    enum Tutorial
    {
        Idle,
        MoveView, //시야이동
        MovePlayer, //플레이어 이동
        Jump, //플레이어 점프
        AttackEnemy, //적 공격하는 법
        ReloadBullet, //탄환 재장전
        PickupItem, //아이템 획득 + 사용
        UseSkill //스킬 사용하기
    }
    Tutorial t_State;
    [SerializeField] private GameObject tutorialPanel; //튜토리얼 팁 안내창
    [SerializeField] private TextMeshProUGUI tutorialText; //튜토리얼 순서마다 텍스트 변경
    [SerializeField] [TextArea] private string[] tutorialOrder; //튜토리얼 순서 배열(TextArea는 줄을 띄우기 위함)
    RaycastHit hitInfo;
    [SerializeField] private float range; //상황별 거리 측정
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI hpText;
    float remainHP;
    [SerializeField] GameObject player;
    public static bool isDamaged = false;
    public static bool doneTutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        t_State = Tutorial.Idle;
        tutorialText.text = tutorialOrder[0];
    }

    // Update is called once per frame
    void Update()
    {
        switch(t_State)
        {
            case Tutorial.Idle:
                Idle();
                break;
            case Tutorial.MoveView:
                MoveView();
                break;
            case Tutorial.MovePlayer:
                MovePlayer();
                break;
            case Tutorial.Jump:
                Jump();
                break;
            case Tutorial.AttackEnemy:
                Attack();
                break;
            case Tutorial.ReloadBullet:
                ReloadBullet();
                break;
            case Tutorial.PickupItem:
                PickupItem();
                break;
            case Tutorial.UseSkill:
                UseSkill();
                break;
        }
    }

    void Idle()
    {
        StartCoroutine(PIdle());
    }

    void MoveView()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                tutorialText.text = tutorialOrder[2];
            }
        }
    }

    void Jump()
    {
        if(PlayerMove.jumpTotal >= 1)
        {
            tutorialText.text = tutorialOrder[4];
            t_State = Tutorial.AttackEnemy;
        }
    }

    void MovePlayer()
    {
        tutorialText.text = tutorialOrder[3];
        t_State = Tutorial.Jump;
    }

    void Attack()
    {
        if(PlayerFire.currentBullet <= 4)
        {
            tutorialText.text = tutorialOrder[5];
            t_State = Tutorial.ReloadBullet;
        }
        if(EnemyFSM.dieEnemy == 1)
        {
            tutorialText.text = tutorialOrder[7];
            t_State = Tutorial.PickupItem;
        }
        
    }

    void ReloadBullet()
    {
        if(PlayerFire.isReload == true && PlayerFire.currentBullet == 10)
        {
            tutorialText.text = tutorialOrder[4];
            t_State = Tutorial.AttackEnemy;
        }
        if (EnemyFSM.dieEnemy == 1)
        {
            t_State = Tutorial.PickupItem;
        }

    }

    void PickupItem()
    {
        Debug.Log("playerHP: " + PlayerHP.currentHP);
        if (PlayerHP.currentHP < 100)
        {
            Debug.Log("playerHP: " + PlayerHP.currentHP);
            if (PlayerHP.currentHP < 100)
            {
                tutorialText.text = tutorialOrder[7];
                t_State = Tutorial.PickupItem;

                if (isDamaged == false)
                {
                    hpBar.fillAmount = PlayerHP.currentHP / PlayerHP.maxHP;
                    remainHP = hpBar.fillAmount * 100;
                    hpText.text = remainHP.ToString();
                    isDamaged = true;
                }

                if (Inventory.isActive == true && Slot.useItem == true && PlayerHP.isRecovery == true)
                {
                    tutorialText.text = tutorialOrder[9];
                    t_State = Tutorial.UseSkill;
                }
            }
        }
        else if (PlayerHP.currentHP >= 100)
        {
            if(PlayerHP.currentHP == 100)
            {
                StartCoroutine(PDamage());
            }
        }
    }   

    void UseSkill()
    {
        if(PlayerFire.plusNum == 2)
        {
            StartCoroutine(UsePSkill());
        }
    }

    IEnumerator PIdle()
    {
        yield return new WaitForSeconds(2.5f);
        tutorialText.text = tutorialOrder[1];
        t_State = Tutorial.MoveView;
    }

    IEnumerator PDamage()
    {
        Debug.Log("코루틴이 시작되었습니다.");
        tutorialText.text = tutorialOrder[8];
        yield return new WaitForSeconds(1f);

        if (isDamaged == false)
        {
            PlayerHP.currentHP = 90;
            hpBar.fillAmount = (float)PlayerHP.currentHP / PlayerHP.maxHP;
            remainHP = hpBar.fillAmount * 100;
            hpText.text = remainHP.ToString();
            isDamaged = true;
        }

        tutorialText.text = tutorialOrder[7];

        yield return new WaitUntil(() => Inventory.isActive && Slot.useItem && PlayerHP.isRecovery); //조건이 만족될 때까지 기다리기

        tutorialText.text = tutorialOrder[9];
        t_State = Tutorial.UseSkill;
        Debug.Log("튜토리얼 다음 단계로 넘어감");

    }

    IEnumerator UsePSkill()
    {
        tutorialText.text = tutorialOrder[10];
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("LobbyScene");
        doneTutorial = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Coordinates"))
        {
            MovePlayer();
        }
    }
}
