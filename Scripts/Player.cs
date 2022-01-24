using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public DataController DataController;


    public float maxSpeed;
    public float jumpPower;
    public bool isGround;
    public bool isGetDouble;
    public int jumpCount = 1;
    public int playerHp;

    private float curTime;
    public float coolTime = 0.5f;

    public Transform pos;
    public Vector2 boxSize;


    public GameObject player;
    public GameObject monster;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    public GameObject hp4;
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

        playerHp = 4;
    }

    private void Update()
    {
        if(isGround)//���� ��� �ְ�
        {
            if (jumpCount > 0)//���� ���� Ƚ���� 0���� ũ��
            {
                if (Input.GetButtonDown("Jump"))//����
                {
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    jumpCount--;//���� ���� Ƚ�� ����
                    anim.SetBool("isJumping",true);
                }
            }
        }
        

        //������ Ű���� ���� ���� �̵��� ����
        if (Input.GetButtonUp("Horizontal") )
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //���⿡ ���� �¿� ����
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //�ȴ� �ִϸ��̼�
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        //���� ����
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
            anim.SetBool("isJumping", false);
            isGround = true;    //Ground�� ������ isGround�� true
            if(isGetDouble == false)
            {
                jumpCount = 1; //�������� ���� ���� ���¿��� Ground�� ������ ����Ƚ���� 1�� �ʱ�ȭ��
            }
            else
            {
                jumpCount = 2; //�������� ���� �ĺ��ʹ� 2�� ������ ������
            } 
        }
        
        //���Ϳ� ���� �� �÷��̾��� ü���� ����
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
                Die();
            }
        }

        //������ ȹ��� 2�� ���� ����
        if (col.gameObject.tag == "Item")
        {
            jumpCount = 2;
            isGetDouble = true;
            item.SetActive(false);
        }

    }

    //���ε isTrigger�� �����ϱ� ������ OnTriggerEnter���� ó��
    private void OnTriggerEnter2D(Collider2D col)
    {
        //���ε�� �浹�� ����
        if (col.gameObject.tag == "Lamp1")
        {
            DataController.gameData.isClear1 = true;
            playerHp = 4;
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(true);
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

    //�÷��̾� �̵��Լ�
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
        gameObject.layer = 7;

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

    void Die()
    {
        player.SetActive(false);
        gameoverCanvas.SetActive(true);
    }

    //���� ������ ��Ÿ���� ����� �׸��� �Լ�
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }


}
