using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public bool isGround;
    public int jumpCount = 1;
    public int playerHp;

    private float curTime;
    public float coolTime = 0.5f;

    public Transform pos;
    public Vector2 boxSize;

    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    public GameObject hp4;

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

        playerHp = 4;
    }

    private void Update()
    {
        if(isGround)//땅에 닿아 있고
        {
            if (jumpCount > 0)//점프 가능 횟수가 0보다 크면
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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //걷는 애니메이션
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        //근접 공격
        if (curTime <= 0)
        {
            if(Input.GetKey(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                   if(collider.tag == "Monster")
                   {
                        collider.GetComponent<Monster>().TakeDamage(1);
                   }
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

        if(col.gameObject.tag == "Monster")
        {
            OnDamaged(col.transform.position);
            playerHp--;
            if(playerHp == 3)
            {
                hp1.SetActive(false);
            }
            if (playerHp == 2)
            {
                hp2.SetActive(false);
            }
            if (playerHp == 1)
            {
                hp3.SetActive(false);
            }
            if (playerHp == 0)
            {
                hp4.SetActive(false);
            }
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

    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 5;

        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, -2) * 10.0f, ForceMode2D.Impulse);

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 3;
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }

    //공격 범위를 나타내는 기즈모 그리는 함수
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }


}
