using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator animator;

    [SerializeField] private float MoveSpeed = 0.01f;     // ���� �̵� �ӵ�
    [SerializeField] private float MoveDistance = 5f;     // ���� �̵� �Ÿ�
    public int CurrentDirection = 0;    // ������ ����

    public bool Moving = true;
    public bool Attacking = false;
    public bool SetTime = false;    // �� ������ �����ϴ� �ð��� üũ�ϱ� ���� ����

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

        // �ʱ� �̵� ������ �������� ����(�������� ���� �� ĳ���� ���� ���)
        EndPosition = new Vector3(gameObject.transform.position.x - MoveDistance, gameObject.transform.position.y, 0);
    }

    public void Dead()
    {
        // �� ������ ���� ��ġ�� �����Ͽ� �������� ����
        EndPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        Moving = false;
        Attacking = false;

        // ���� �������� �缳��
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
        // ������ ���� ���� �ݶ��̴� Ȱ��ȭ
        if (Attacking)
        {
            gameObject.transform.Find("AttackBox").gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("AttackBox").gameObject.SetActive(false);
        }
    }

    public void Move(Vector3 End)  // �� �������� �̵�
    {
        Attacking = false;
        Moving = true;

        transform.position = Vector3.MoveTowards(gameObject.transform.position, End, MoveSpeed);
        PositionCheck(End);
    }

    public void PositionCheck(Vector3 End) // �� �������� �����Ͽ����� Ȯ��
    {
        if (gameObject.transform.position == End)
        {
            Moving = false;
        }
    }

    public void AnimationDirection()
    {
        // �̵� ���⿡ ���� �ִϸ��̼� ����� ���� ���������� �̵��� �� �¿����
        if (CurrentDirection == 0)
        {
            gameObject.transform.localScale = Scale;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-Scale.x, Scale.y, Scale.z);
        }
    }

    public void TurnDirection(int dir)  //���� �ٲٱ�
    {
        EndPosition = new Vector3(gameObject.transform.position.x + (MoveDistance * dir), gameObject.transform.position.y, 0);
        SetTime = false;

        AnimationDirection();
        Moving = true;
    }
}
