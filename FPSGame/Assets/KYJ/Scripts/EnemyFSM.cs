using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    CharacterController character;
    float attackDelay = 2f;
    float currentTime = 0;
    Vector3 originPosition; //Enemy가 원래 위치해있던 좌표
    [SerializeField] private float moveDistance = 20f;
    int currentHP;
    int maxHP = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        e_State = EnemyState.Idle;
        currentHP = maxHP;
        originPosition = transform.position;
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(e_State)
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
            character.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            e_State = EnemyState.Attack;
            Debug.Log("Move -> Attack");
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            e_State = EnemyState.Attack;
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
            character.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            {
                transform.position = originPosition;
                e_State = EnemyState.Idle;
                Debug.Log("Return -> Idle");
            }
        }
    }

    void Damaged()
    {

    }

    void Die()
    {

    }
}
