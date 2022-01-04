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
        if(isGround)//���� ��� �ְ�
        {
            if(jumpCount > 0)//���� ���� Ƚ���� 0���� ũ��
            {
                if (Input.GetButtonDown("Jump"))//����
                {
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    jumpCount--;//���� ���� Ƚ�� ����
                }
            }
        }
        

        //������ Ű���� ���� ���� �̵��� ����
        if (Input.GetButtonUp("Horizontal") )
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //���⿡ ���� �¿� ����
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        
        //�ȴ� �ִϸ��̼�
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
            isGround = true;    //Ground�� ������ isGround�� true
            jumpCount = 1;          //Ground�� ������ ����Ƚ���� 1�� �ʱ�ȭ��
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

}
