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
        // 퍼즐이 부드럽게 이동하도록 설정
        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.05f);

        // 퍼즐이 올바른 위치에 있을 때
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
