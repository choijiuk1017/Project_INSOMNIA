using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    public Vector3 TargetPosition;
    Vector3 CorrectPosition;
    SpriteRenderer sprite;

    public int number;
    public bool inRightPlace;


    // Start is called before the first frame update
    void Awake()
    {
        TargetPosition = transform.position;
        CorrectPosition = transform.position;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // ������ �ε巴�� �̵��ϵ��� ����
        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.05f);

        // ������ �ùٸ� ��ġ�� ���� ��
        if (TargetPosition == CorrectPosition)
        {
            sprite.color = Color.white;
            inRightPlace = true;
        }
        else
        {
            sprite.color = Color.gray;
            inRightPlace = false;
        }
    }
}
