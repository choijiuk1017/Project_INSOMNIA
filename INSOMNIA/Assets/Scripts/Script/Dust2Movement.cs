using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust2Movement : MonoBehaviour
{
    Movement movement;

    [SerializeField] private float RunSpeed = 0.03f;      // ���� �ӵ�
    [SerializeField] private float RunDistance = 9.45f;   // ���� �Ÿ�

    bool SetRun = true;
    

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        movement.Setting();
    }

    // Update is called once per frame
    void Update()
    {
        // ������� �� 
        if(!gameObject.transform.Find("CollisionBox").GetComponent<Dust>().isDead)
        {
            if (movement.Moving)
            {
                movement.Move(movement.EndPosition);  //�� �������� �̵�
            }

            if (!movement.Moving)
            {
                movement.Attacking = true;
                movement.SetTime = true;

                //����
                Run();

                // ��ǥ ������ ����
                if (gameObject.transform.position == movement.EndPosition)
                {
                    movement.Attacking = false;

                    // ���� 1.5��
                    Invoke("SetEndPosition", 1.5f);    
                }
            }
            movement.SetAttackCollisionBox();
            movement.AnimationUpdate();
        }
        
        // �����
        if (gameObject.transform.Find("CollisionBox").GetComponent<Dust>().isDead)
        {
            movement.Dead();
        }
    }

    void Run()
    {
        if (movement.CurrentDirection == 0)     //����
        {
            SetRunEnd(-1);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, movement.EndPosition, RunSpeed);
        }
        else if (movement.CurrentDirection == 1)    //������
        {
            SetRunEnd(1);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, movement.EndPosition, RunSpeed);
        }

    }
    void SetRunEnd(int dir)
    {
        if (SetRun) // ���� ����
        {
            movement.EndPosition = new Vector3(gameObject.transform.position.x + (RunDistance * dir), gameObject.transform.position.y, 0);

            movement.SetTime = false;
            SetRun = false;
        }
    }


    void SetEndPosition()
    {
        if (movement.SetTime)    // ���� ������ �ϴ� �ð��� ��
        {
            SetRun = true;

            int direction = Random.Range(0, 2); // �������� ���� ����
            movement.CurrentDirection = direction;

            if (direction == 0)     // ����
            {
                movement.TurnDirection(-1);
            }
            else if (direction == 1)    // ������
            {
                movement.TurnDirection(1);
            }
        }
    }
}
