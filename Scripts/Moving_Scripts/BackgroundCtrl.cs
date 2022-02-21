using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCtrl : MonoBehaviour
{
    public GameObject player;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(playerTransform.position.x, playerTransform.position.y + 2);
        transform.Translate(0, 0, 1);
    }
}
