using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    EnemyState e_State;
    [SerializeField] private float findDistance = 8f; //플레이어를 감지할 수 있는 거리 범위
    [SerializeField] private Transform player;
    [SerializeField] private float attackDistance = 2f; //공격이 가능한 거리 범위
    [SerializeField] private float moveSpeed = 5f;
    Rigidbody rigid;
    float attackDelay = 2f;
    float currentTime = 0;
    Vector3 originPosition; //Enemy가 원래 위치해있던 좌표
    Quaternion originRotation;
    [SerializeField] private float moveDistance = 20f;
    int currentHP;
    int maxHP = 50;
    [SerializeField] private Image enemyHPBar;
    [SerializeField] private TextMeshProUGUI personText;
    public static int dieEnemy = 0; //죽인 적의 수
    private float remainHP;
    [SerializeField] TextMeshProUGUI currentPersonText;

    // Start is called before the first frame update
    void Start()
    {
        e_State = EnemyState.Idle;
        currentHP = maxHP;
        originPosition = transform.position;
        originRotation = transform.rotation;
        rigid = GetComponent<Rigidbody>();
        enemyHPBar = enemyHPBar.GetComponent<Image>();
        dieEnemy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (e_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }

    }

    public void HitEnemy()
    {
        if(e_State == EnemyState.Damaged || e_State == EnemyState.Die || e_State == EnemyState.Return)
        {
            return;
        }
        currentHP -= 5;
        enemyHPBar.fillAmount = (float)currentHP / (float)maxHP;
        Debug.Log("적의 currentHP: " + currentHP);


        if(currentHP > 0)
        {
            e_State = EnemyState.Damaged;
            Debug.Log("Any State -> Damaged");
            Damaged();
        }
        else
        {
            e_State = EnemyState.Die;
            Debug.Log("Any State -> Die");
            Die();
            dieEnemy++;
            if (dieEnemy == 2)
            {
                GameManager.instance.ManualGameClear();
            }
            personText.text = dieEnemy.ToString();
            currentPersonText.text = dieEnemy.ToString();
        }
    }

    void Idle()
    {
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            e_State = EnemyState.Move;
            Debug.Log("Idle -> Move");
        }
    }

    void Move()
    {
        if(Vector3.Distance(transform.position, originPosition) > moveDistance)
        {
            e_State = EnemyState.Return;
            Debug.Log("Move -> Return");
        }
        else if(Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.forward = dir;
        }
        else
        {
            e_State = EnemyState.Attack;
            Debug.Log("Move -> Attack");
            currentTime = attackDelay;
        }
    }

    public void AttackAction()
    {
        player.GetComponent<PlayerHP>().DamageAction();
    }

    void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                player.GetComponent<PlayerHP>().DamageAction();
                Debug.Log("공격");
                currentTime = 0;
            }
        }
        else
        {
            e_State = EnemyState.Move;
            Debug.Log("Attack -> Move");
            currentTime = 0;
        }
    }

    void Return()
    {
        if(Vector3.Distance(transform.position, originPosition) > 0.1f)
        {
            Vector3 dir = (originPosition - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.forward = dir;
        }
        else
        {
            transform.position = originPosition;
            transform.rotation = originRotation;
            e_State = EnemyState.Idle;
            Debug.Log("Return -> Idle");
        }
    }

    void Damaged()
    {
        StartCoroutine(DamagedProcess());
    }

    void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        Destroy(gameObject, 2f);
        yield return null;
    }

    IEnumerator DamagedProcess()
    {
        yield return new WaitForSeconds(1f);
        e_State = EnemyState.Move;
        Debug.Log("Damaged -> Move");
    }
}
