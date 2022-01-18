using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
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
        transform.position = Vector2.Lerp(transform.position, playerTransform.position, 2f * Time.deltaTime);
        transform.Translate(0, 0, -10);
    }
}
