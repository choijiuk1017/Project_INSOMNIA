using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dust : MonoBehaviour
{
    public int monsterHp;

    public GameObject player;

    public GameObject monster;

    Rigidbody2D rigid;

    [SerializeField] private GameObject IdleObject;   

    Animator animator;

    // ������ ������ ���� �ٸ� �͵��� ������ ����(�ʱ�ȭ�� ���� a �������� �ۼ�)
    
    [SerializeField] private float DeadAnimationTime = 1.25f;     // ������ ���� �ִϸ��̼� ��� �ð�(�ִϸ��̼� ��� �� �����ϱ� ����)

    public bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = IdleObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        DeadCheck();
    }

    void DeadCheck()    // ����� �ִϸ��̼� ��� �� ����
    {
        if (monsterHp <= 0)
        {
            isDead = true;
        }
        if (isDead)
        {
            animator.SetBool("isDead", true);
            Invoke("DestroyDust", DeadAnimationTime);
        }
    }

    void DestroyDust()
    {
        Destroy(IdleObject.gameObject);
    }

    public void TakeDamage(int damage)
    {
        monsterHp = monsterHp - damage;

        //���͸� ������ �� ���Ͱ� �ڷ� �з���
        int dirc = transform.position.x - player.transform.position.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 2), ForceMode2D.Impulse);
    }
}
