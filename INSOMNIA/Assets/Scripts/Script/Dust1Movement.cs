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
        // 살아있을 때 
        if (gameObject.transform.Find("CollisionBox").GetComponent<Dust>().isDead == false)
        {
            if (movement.Moving)
            {
                movement.Move(movement.EndPosition);  //끝 지점까지 이동
            }

            if (!movement.Moving)
            {
                movement.Attacking = true;
                movement.SetTime = true;

                // 정지 2초
                Invoke("SetEndPosition", 2);
            }

            movement.SetAttackCollisionBox();
            movement.AnimationUpdate();
        }

        // 사망시
        if (gameObject.transform.Find("CollisionBox").GetComponent<Dust>().isDead == true)
        {
            movement.Dead();
        }
    }

    void SetEndPosition()
    {
        if (movement.SetTime)    // 방향 설정을 하는 시간일 때
        {
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
