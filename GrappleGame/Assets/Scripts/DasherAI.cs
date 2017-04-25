using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DasherAI : MonoBehaviour
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
	public bool isFacingRight = true;
	public bool isFacingLeft = false;
    public bool isDashing = false;
    public float AggroRadius;
    public float cooldownTime;
	public Animator anim;

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
		anim = this.GetComponent<Animator> ();
    }

	void FixedUpdate()
	{
		if (isFacingRight) 
		{
			anim.SetInteger ("State", 0);
		}

		if (isFacingLeft) 
		{
			anim.SetInteger ("State", 1);
		}

		if (isFacingRight && isDashing) 
		{
			anim.SetInteger ("State", 2);
		}

		if (isFacingLeft && isDashing) 
		{
			anim.SetInteger ("State", 3);
		}
	}


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
        }
        else
        {
            isDashing = false;
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
			StartCoroutine ("EndGame");
        }
    }

	IEnumerator EndGame(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Scenes/GameOver");
	}


    void dash()
    {
        isDashing = true;
        rigidBody.AddForce(new Vector2(facing * 200f * 4, 0));
        cooldown = cooldownTime;
        dashTime = .5f;
        GetComponent<AudioSource>().Play();
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
        isFacingLeft = !isFacingLeft;
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
