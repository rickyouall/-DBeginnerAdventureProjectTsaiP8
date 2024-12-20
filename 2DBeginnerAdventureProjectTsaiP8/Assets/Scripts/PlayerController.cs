using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2;

    public GameObject projectilePrefab;

    bool isInvincible;
    float invincibleTimer;
    public int health { get { return currentHealth; } }
    int currentHealth;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    AudioSource audioSource;
    public AudioClip throwSound;
    public AudioClip hitSound;



    // Start is called b efore the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

      audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)

        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            { isInvincible = false; }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonePlayerCharacter character = hit.collider.GetComponent<NonePlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();

                }

            }
        }
        }
    void FixedUpdate()
    {

        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");

            if (isInvincible)
            {
                return;
            }

            isInvincible = true;
            invincibleTimer = timeInvincible;
            PlaySound(hitSound);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIhealthbar.instance.SetValue(currentHealth/(float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity );

        projectile projectile = projectileObject.GetComponent<projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(throwSound);
    }
    public void PlaySound(AudioClip clip)
    { 
    audioSource.PlayOneShot(clip);
    }




}