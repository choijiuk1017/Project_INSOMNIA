using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator animator;

    [SerializeField] private float MoveSpeed = 0.01f;     // 먼지 이동 속도
    [SerializeField] private float MoveDistance = 5f;     // 먼지 이동 거리
    public int CurrentDirection = 0;    // 현재의 방향

    public bool Moving = true;
    public bool Attacking = false;
    public bool SetTime = false;    // 끝 지점을 설정하는 시간을 체크하기 위한 변수

    public Vector3 EndPosition;
    [SerializeField] private Vector3 Scale;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting()
    {
        animator = GetComponent<Animator>();

        // 초기 이동 방향은 왼쪽으로 설정(스테이지 시작 시 캐릭터 방향 고려)
        EndPosition = new Vector3(gameObject.transform.position.x - MoveDistance, gameObject.transform.position.y, 0);
    }

    public void Dead()
    {
        // 끝 지점을 현재 위치로 변경하여 움직임을 멈춤
        EndPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        Moving = false;
        Attacking = false;

        // 원래 방향으로 재설정
        gameObject.transform.localScale = Scale;
    }

    public void AnimationUpdate()
    {
        if (Moving)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (Attacking)
        {
            animator.SetBool("isAttack", true);
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }

    public void SetAttackCollisionBox()
    {
        // 공격할 때만 공격 콜라이더 활성화
        if (Attacking)
        {
            gameObject.transform.Find("AttackBox").gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("AttackBox").gameObject.SetActive(false);
        }
    }

    public void Move(Vector3 End)  // 끝 지점까지 이동
    {
        Attacking = false;
        Moving = true;

        transform.position = Vector3.MoveTowards(gameObject.transform.position, End, MoveSpeed);
        PositionCheck(End);
    }

    public void PositionCheck(Vector3 End) // 끝 지점까지 도달하였는지 확인
    {
        if (gameObject.transform.position == End)
        {
            Moving = false;
        }
    }

    public void AnimationDirection()
    {
        // 이동 방향에 맞춘 애니메이션 재생을 위해 오른쪽으로 이동할 때 좌우반전
        if (CurrentDirection == 0)
        {
            gameObject.transform.localScale = Scale;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-Scale.x, Scale.y, Scale.z);
        }
    }

    public void TurnDirection(int dir)  //방향 바꾸기
    {
        EndPosition = new Vector3(gameObject.transform.position.x + (MoveDistance * dir), gameObject.transform.position.y, 0);
        SetTime = false;

        AnimationDirection();
        Moving = true;
    }
}
