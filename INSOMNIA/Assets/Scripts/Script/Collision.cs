using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private string part;
    [SerializeField] private Vector3 Scale;

    GameObject PlayerObject;
    Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.transform.parent.gameObject.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // �������� �̵��ϴ� ���� ���� ����, ������ �̵��� �����ϱ� ���� �¿���� ���
            // CurrentDirection - 0: ����, 1: ������
            // TurnDirection - -1:����, 1: ������

            if (gameObject.transform.parent.gameObject.transform.localScale == Scale) // ���� �̵�
            {
                // ��  front ��� back
                if (part == "front") // �������� �浹�ؼ� ���������� U��
                {
                    movement.CurrentDirection = 1;
                    movement.TurnDirection(1);
                }
                
                else if (part == "back") // ���������� �浹�ؼ� �������� U��
                {
                    movement.CurrentDirection = 0;
                    movement.TurnDirection(-1);
                }
            }

            // ������ �̵�
            else if (gameObject.transform.parent.gameObject.transform.localScale == new Vector3(-Scale.x, Scale.y, Scale.z))
            {
                // back ��� front  ��
                if (part == "front") // ���������� �浹�ؼ� �������� U��
                {
                    movement.CurrentDirection = 0;
                    movement.TurnDirection(-1);
                }

                else if (part == "back") // �������� �浹�ؼ� ���������� U��
                {
                    movement.CurrentDirection = 1;
                    movement.TurnDirection(1);
                }
            }
        }
    }

    void OnPlayerCollision()
    {
        PlayerObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
