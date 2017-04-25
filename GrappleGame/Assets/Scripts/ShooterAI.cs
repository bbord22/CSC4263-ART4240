using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShooterAI : MonoBehaviour
{
    private int facing;
    private Rigidbody2D rigidBody;
    private int state;
    private GameObject player;
    private Transform playerT;
    private float cooldownTimer;
    private float distFromPlayer;
    private bool isFacingRight = true;
    private bool isFacingLeft = false;
    public float AggroRadius;
    public float maxSpeed;
    public float shootCooldown;
    public GameObject bullet;
    public GameObject bulletSpawn;

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
        if (!(state == 2))
        {
            checkPlayerDist();
        }

        if (state == 1)
        {
            facePlayer();

            if(cooldownTimer <= 0)
            {
                shoot();
            }

            rigidBody.velocity = new Vector2(0, 0);
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

    void PlayerDied()
    {
        state = 2;
        player = null;
        playerT = null;
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
            gameObject.SendMessage("PlayerDied");
            Destroy(other.gameObject);
			StartCoroutine ("EndGame");
        }
    }

	IEnumerator EndGame(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Scenes/GameOver");
	}

    void shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawn.transform) as GameObject;
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
        if (!(state == 2))
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