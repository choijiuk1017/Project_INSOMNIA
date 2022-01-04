using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public bool isGround;
    public int jumpCount = 1;

    private float curTime;
    public float coolTime = 0.5f;

    public Transform pos;
    public Vector2 boxSize;

    Rigidbody2D rigid;

    SpriteRenderer spriteRenderer;

    Animator anim;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        isGround = false;

        jumpCount = 0;
    }

    void Update()
    {
        if(isGround)//땅에 닿아 있고
        {
            if(jumpCount > 0)//점프 가능 횟수가 0보다 크면
            {
                if (Input.GetButtonDown("Jump"))//점프
                {
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    jumpCount--;//점프 가능 횟수 감소
                }
            }
        }
        

        //유저가 키에서 손을 떼면 이동을 멈춤
        if (Input.GetButtonUp("Horizontal") )
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //방향에 따라 좌우 반전
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        
        //걷는 애니메이션
        if(Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }


        if (curTime <= 0)
        {
            if(Input.GetKey(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    Debug.Log(collider.tag);
                }
                anim.SetTrigger("atk");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }



    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = true;    //Ground에 닿으면 isGround는 true
            jumpCount = 1;          //Ground에 닿으면 점프횟수가 1로 초기화됨
        }
    }

    void FixedUpdate()
    {
        move();  
    }

    //플레이어 이동함수
    void move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

}
