 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public int speed;
    private GameObject player;
    private Transform target;
    private Vector2 targetVector;
    private Rigidbody2D rb2d;
    private Vector2 path;
    private Vector3 position;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        targetVector = target.position;
        rb2d = GetComponent<Rigidbody2D>();
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
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
