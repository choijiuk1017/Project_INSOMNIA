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
            // 왼쪽으로 이동하는 것이 원래 상태, 오른쪽 이동을 구현하기 위해 좌우반전 사용
            // CurrentDirection - 0: 왼쪽, 1: 오른쪽
            // TurnDirection - -1:왼쪽, 1: 오른쪽

            if (gameObject.transform.parent.gameObject.transform.localScale == Scale) // 왼쪽 이동
            {
                // ←  front □□ back
                if (part == "front") // 왼쪽으로 충돌해서 오른쪽으로 U턴
                {
                    movement.CurrentDirection = 1;
                    movement.TurnDirection(1);
                }
                
                else if (part == "back") // 오른쪽으로 충돌해서 왼쪽으로 U턴
                {
                    movement.CurrentDirection = 0;
                    movement.TurnDirection(-1);
                }
            }

            // 오른쪽 이동
            else if (gameObject.transform.parent.gameObject.transform.localScale == new Vector3(-Scale.x, Scale.y, Scale.z))
            {
                // back □□ front  →
                if (part == "front") // 오른쪽으로 충돌해서 왼쪽으로 U턴
                {
                    movement.CurrentDirection = 0;
                    movement.TurnDirection(-1);
                }

                else if (part == "back") // 왼쪽으로 충돌해서 오른쪽으로 U턴
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
