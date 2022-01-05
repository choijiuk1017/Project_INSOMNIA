using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int monsterHp;

    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        monsterHp = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //������ ü���� �پ��� �Լ�
    public void TakeDamage(int damage)
    {
        monsterHp = monsterHp - damage; 
        if(monsterHp == 0)
        {
            monster.SetActive(false);
        }
    }
}
