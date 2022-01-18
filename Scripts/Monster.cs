using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int monsterHp;

    public GameObject player;

    public GameObject monster;

    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        monsterHp = 5;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //몬스터의 체력이 줄어드는 함수
    public void TakeDamage(int damage)
    {
        monsterHp = monsterHp - damage;

        //몬스터를 공격할 시 몬스터가 뒤로 밀려남
        int dirc = transform.position.x - player.transform.position.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 2), ForceMode2D.Impulse);

        if (monsterHp == 0)
        {
            monster.SetActive(false);
        }
    }
}
