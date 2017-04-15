using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    private int facing;
    private Rigidbody2D rigidBody;
    private int state;
    private GameObject player;
    private Transform playerT;
    private float cooldownTimer;
    private float distFromPlayer;
    public float AggroRadius;
    public float maxSpeed;
    public float shootCooldown;
    public GameObject bullet;

    void Start()
    {
        facing = 1;
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        state = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerT = player.GetComponent<Transform>();
        cooldownTimer = 0;
    }

    void Update()
    {
        checkPlayerDist();

        if (state == 1)
        {
            facePlayer();

            if(cooldownTimer <= 0)
            {
                shoot();
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(facing * maxSpeed, rigidBody.velocity.y);
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlatformEdge")
        {
            flip();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = 2;
            player = null;
            playerT = null;
            Destroy(other.gameObject);
        }
    }

    void shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        cooldownTimer = shootCooldown;
    }

    void facePlayer()
    {
        if (transform.position.x < playerT.position.x && facing == -1)
        {
            flip();
        }
        else if (transform.position.x > playerT.position.x && facing == 1)
        {
            flip();
        }
    }

    void checkPlayerDist()
    {
        distFromPlayer = Vector2.Distance(playerT.position, transform.position);

        if (distFromPlayer < AggroRadius)
        {
            state = 1;
        }
        else
        {
            state = 0;
        }
    }

    void flip()
    {
        facing = facing * -1; //change which way it is labeled as facing
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}