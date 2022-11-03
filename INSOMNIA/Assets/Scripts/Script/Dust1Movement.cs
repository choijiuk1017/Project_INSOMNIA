using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust1Movement : MonoBehaviour
{
    Movement movement;


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
        if (gameObject.transform.Find("CollisionBox").GetComponent<Dust>().isDead == false)
        {
            if (movement.Moving)
            {
                movement.Move(movement.EndPosition);  //�� �������� �̵�
            }

            if (!movement.Moving)
            {
                movement.Attacking = true;
                movement.SetTime = true;

                // ���� 2��
                Invoke("SetEndPosition", 2);
            }

            movement.SetAttackCollisionBox();
            movement.AnimationUpdate();
        }

        // �����
        if (gameObject.transform.Find("CollisionBox").GetComponent<Dust>().isDead == true)
        {
            movement.Dead();
        }
    }

    void SetEndPosition()
    {
        if (movement.SetTime)    // ���� ������ �ϴ� �ð��� ��
        {
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
