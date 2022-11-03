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

    // 먼지의 종류에 따라 다른 것들을 변수로 선언(초기화는 먼지 a 기준으로 작성)
    
    [SerializeField] private float DeadAnimationTime = 1.25f;     // 먼지의 죽음 애니메이션 재생 시간(애니메이션 재생 후 제거하기 위함)

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

    void DeadCheck()    // 사망시 애니메이션 재생 후 삭제
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

        //몬스터를 공격할 시 몬스터가 뒤로 밀려남
        int dirc = transform.position.x - player.transform.position.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 2), ForceMode2D.Impulse);
    }
}
