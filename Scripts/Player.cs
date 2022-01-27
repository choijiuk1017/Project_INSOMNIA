using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public DataController DataController;
    public HealthPointManager HealthPointManager;

    public float maxSpeed;
    public float jumpPower;
    public bool isGround;
    public bool isGetDouble;
    public int jumpCount = 1;

    private float curTime;
    public float coolTime = 0.5f;

    public Transform pos;
    public Vector2 boxSize;


    public GameObject player;
    public GameObject monster;
    public GameObject item;
    public GameObject gameoverCanvas;

    Rigidbody2D rigid;

    SpriteRenderer spriteRenderer;

    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        isGround = false;

        isGetDouble = false;

        jumpCount = 0;

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
                    anim.SetBool("isJumping",true);
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


        if(DataController.gameData.isClear1 == true && HealthPointManager.startHealthContainer < 2)
        {
            HealthPointManager.TakeDamage(HealthPointManager.startHealthContainer * HealthPointManager.healthPerHeart);
            HealthPointManager.AddHeartContainer();
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            anim.SetBool("isJumping", false);
            isGround = true;    //Ground에 닿으면 isGround는 true
            if(isGetDouble == false)
            {
                jumpCount = 1; //아이템을 먹지 않은 상태에서 Ground에 닿으면 점프횟수가 1로 초기화됨
            }
            else
            {
                jumpCount = 2; //아이템을 먹은 후부터는 2단 점프가 가능함
            } 
        }
        
        //몬스터에 닿을 시 플레이어의 체력이 감소
        if(col.gameObject.tag == "Monster")
        {
            OnDamaged(col.transform.position);
            HealthPointManager.TakeDamage(-1);
        }

        //아이템 획득시 2단 점프 가능
        if (col.gameObject.tag == "Item")
        {
            jumpCount = 2;
            isGetDouble = true;
            item.SetActive(false);
        }

    }

    //가로등에 isTrigger을 적용하기 때문에 OnTriggerEnter에서 처리
    private void OnTriggerEnter2D(Collider2D col)
    {
        //가로등과 충돌시 저장
        if (col.gameObject.tag == "Lamp1")
        {
            DataController.gameData.isClear1 = true;
        }

        if (col.gameObject.tag == "DeadZone")
        {
            Die();
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

    //플레이어가 데미지를 입을 경우 무적시간을 주는 함수
    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 8;

        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 5.0f, ForceMode2D.Impulse);

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 3;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void Die()
    {
        player.SetActive(false);
        gameoverCanvas.SetActive(true);
    }

    //공격 범위를 나타내는 기즈모 그리는 함수
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }


}
