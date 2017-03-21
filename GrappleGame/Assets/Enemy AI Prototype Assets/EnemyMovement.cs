using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private int facing;
    private float maxSpeed;
    private Rigidbody2D rigidBody;
    private int state;
    private GameObject player;
    private Transform playerT;
    private float distFromPlayer;
    private float cooldown;
    private float dashTime;
    public float AggroRadius;
    public float cooldownTime;

    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        facing = 1;
        maxSpeed = 3f;
        state = 0;
        dashTime = 0.0f;
        cooldown = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerT = player.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (state == 0)
        {
            checkPlayerDist();
        }
        else if (state == 1)
        {
            facePlayer();
            checkPlayerDist();

            if (cooldown <= 0.0f)
            {
                dash();
            }
        }

        if(cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
        }

        if(dashTime > 0.0f)
        {
            dashTime -= Time.deltaTime;
        } else
        {
            rigidBody.velocity = new Vector2(facing * maxSpeed, rigidBody.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "PlatformEdge")
        {
            flip();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            state = 2;
            player = null;
            playerT = null;
            Destroy(other.gameObject);
        }
    }

    void dash()
    {
        Debug.Log("Dashing");
        rigidBody.AddForce(new Vector2(facing * 200f * 4, 0));
        cooldown = cooldownTime;
        dashTime = .5f;
    }

    void facePlayer()
    {
        if (transform.position.x < playerT.position.x && facing == -1)
        {
            flip();
        } else if (transform.position.x > playerT.position.x && facing == 1)
        {
            flip();
        }
    }

    void checkPlayerDist()
    {
        distFromPlayer = Vector2.Distance(playerT.position, transform.position);

        if(distFromPlayer < AggroRadius)
        {
            state = 1;
        } else
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
