using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
        float vertical;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        Vector2 position = rigidbody2d.position;
        position.x = position.x + 0.6f * horizontal ;
        position.y = position.y + 0.6f * vertical ;

        rigidbody2d.MovePosition(position);
    }
}
