using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int maxHealth = 5;
    public float timeInvincible = 2;

    bool isInvincible;
    float invincibleTimer;
    public int health { get { return currentHealth; } }
    int currentHealth;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;


    // Start is called b efore the first frame update
    void Start()
    {
        currentHealth = maxHealth;


        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)

        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            { isInvincible = false; }
        }
    }
    void FixedUpdate()
    {

        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed + 0.6f * horizontal;
        position.y = position.y + speed + 0.6f * vertical;

        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            return;
        }
        isInvincible = true;
        invincibleTimer = timeInvincible;


        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

    }


}