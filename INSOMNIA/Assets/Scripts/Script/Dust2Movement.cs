using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust2Movement : MonoBehaviour
{
    Movement movement;

    [SerializeField] private float RunSpeed = 0.03f;      // 돌진 속도
    [SerializeField] private float RunDistance = 9.45f;   // 돌진 거리

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
        // 살아있을 때 
        if(!gameObject.transform.Find("CollisionBox").GetComponent<Dust>().isDead)
        {
            if (movement.Moving)
            {
                movement.Move(movement.EndPosition);  //끝 지점까지 이동
            }

            if (!movement.Moving)
            {
                movement.Attacking = true;
                movement.SetTime = true;

                //돌진
                Run();

                // 목표 지점에 도달
                if (gameObject.transform.position == movement.EndPosition)
                {
                    movement.Attacking = false;

                    // 정지 1.5초
                    Invoke("SetEndPosition", 1.5f);    
                }
            }
            movement.SetAttackCollisionBox();
            movement.AnimationUpdate();
        }
        
        // 사망시
        if (gameObject.transform.Find("CollisionBox").GetComponent<Dust>().isDead)
        {
            movement.Dead();
        }
    }

    void Run()
    {
        if (movement.CurrentDirection == 0)     //왼쪽
        {
            SetRunEnd(-1);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, movement.EndPosition, RunSpeed);
        }
        else if (movement.CurrentDirection == 1)    //오른쪽
        {
            SetRunEnd(1);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, movement.EndPosition, RunSpeed);
        }

    }
    void SetRunEnd(int dir)
    {
        if (SetRun) // 돌진 설정
        {
            movement.EndPosition = new Vector3(gameObject.transform.position.x + (RunDistance * dir), gameObject.transform.position.y, 0);

            movement.SetTime = false;
            SetRun = false;
        }
    }


    void SetEndPosition()
    {
        if (movement.SetTime)    // 방향 설정을 하는 시간일 때
        {
            SetRun = true;

            int direction = Random.Range(0, 2); // 랜덤으로 방향 설정
            movement.CurrentDirection = direction;

            if (direction == 0)     // 왼쪽
            {
                movement.TurnDirection(-1);
            }
            else if (direction == 1)    // 오른쪽
            {
                movement.TurnDirection(1);
            }
        }
    }
}
