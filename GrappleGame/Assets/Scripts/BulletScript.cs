using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour {
    public int speed;
    private GameObject player;
    private Transform target;
    private Rigidbody2D rb2d;
    private Vector2 targetVector;
    private Vector2 path;
    private Vector3 position;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        rb2d = GetComponent<Rigidbody2D>();
        targetVector = target.position;
        path = (targetVector - (Vector2)transform.position).normalized;
	}
	
	void Update () {
        position = transform.position;
        rb2d.velocity = path * speed;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            gameObject.SendMessageUpwards("PlayerDied");
            Destroy(other.gameObject);
            StartCoroutine("EndGame");
        }

        Destroy(gameObject);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Scenes/GameOver");
    }
}
