using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust3Movement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 0.01f;     // 먼지 이동 속도
    [SerializeField] private float MoveDistance = 5f;     // 먼지 이동 거리
    [SerializeField] private int CurrentDirction = 0;     // 현재의 방향

    bool Moving = true;

    Vector3 EndPosition;


    // Start is called before the first frame update
    void Start()
    {
        // 초기 이동 방향은 왼쪽으로 설정(스테이지 시작 시 캐릭터 방향 고려)
        EndPosition = new Vector3(gameObject.transform.position.x - MoveDistance, gameObject.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
           Move(EndPosition);  // 끝 지점까지 이동
        }

        // 사망시
        if (gameObject.GetComponent<Dust>().isDead)
        {
            Moving = false;
        }
    }

    void SetEndPosition()
    {
        int direction = Random.Range(0, 2); // 랜덤으로 방향 설정

        CurrentDirction = direction;

        if (direction == 0)     // 왼쪽
        {
            TurnDirection(-1);
        }
        else if (direction == 1)    // 오른쪽
        {
            TurnDirection(1);
        }
    }

    void Move(Vector3 End)  // 끝 지점까지 이동
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, End, MoveSpeed);
        PositionCheck(End);
    }

    void PositionCheck(Vector3 End) // 끝 지점까지 도달하였는지 확인
    {
        if (gameObject.transform.position == End)
        {
            SetEndPosition();
        }
    }

    public void TurnDirection(int dir)  // 방향 바꾸기
    {
        EndPosition = new Vector3(gameObject.transform.position.x + (MoveDistance * dir), gameObject.transform.position.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물과 충돌했을 때 사망
        if (collision.gameObject.tag == "Block")
        {
            Debug.Log("충돌");
            gameObject.GetComponent<Dust>().isDead = true;
        }
    }
}
