using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust3Movement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 0.01f;     // ���� �̵� �ӵ�
    [SerializeField] private float MoveDistance = 5f;     // ���� �̵� �Ÿ�
    [SerializeField] private int CurrentDirction = 0;     // ������ ����

    bool Moving = true;

    Vector3 EndPosition;


    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� �̵� ������ �������� ����(�������� ���� �� ĳ���� ���� ���)
        EndPosition = new Vector3(gameObject.transform.position.x - MoveDistance, gameObject.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
           Move(EndPosition);  // �� �������� �̵�
        }

        // �����
        if (gameObject.GetComponent<Dust>().isDead)
        {
            Moving = false;
        }
    }

    void SetEndPosition()
    {
        int direction = Random.Range(0, 2); // �������� ���� ����

        CurrentDirction = direction;

        if (direction == 0)     // ����
        {
            TurnDirection(-1);
        }
        else if (direction == 1)    // ������
        {
            TurnDirection(1);
        }
    }

    void Move(Vector3 End)  // �� �������� �̵�
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, End, MoveSpeed);
        PositionCheck(End);
    }

    void PositionCheck(Vector3 End) // �� �������� �����Ͽ����� Ȯ��
    {
        if (gameObject.transform.position == End)
        {
            SetEndPosition();
        }
    }

    public void TurnDirection(int dir)  // ���� �ٲٱ�
    {
        EndPosition = new Vector3(gameObject.transform.position.x + (MoveDistance * dir), gameObject.transform.position.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ֹ��� �浹���� �� ���
        if (collision.gameObject.tag == "Block")
        {
            Debug.Log("�浹");
            gameObject.GetComponent<Dust>().isDead = true;
        }
    }
}
