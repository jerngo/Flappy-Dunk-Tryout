using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBall : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;

    public bool isTouchingRing;
    AudioManager audioManager;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //fly on tap
        if (Input.GetMouseButtonDown(0)) {
            anim.SetTrigger("WingFlap");
            audioManager.Play("BallFly");
            rigid.velocity = Vector2.up * speed;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //slow down and reset combo
        if (collision.gameObject.tag == "Ring") {
            FindObjectOfType<Spawner>().slowDown(true);
            if (collision.gameObject.GetComponent<Ring>().isActivated) {
                isTouchingRing = true;
            }
           
        }

        //game over when ball hit roof/floor
        if (collision.gameObject.tag == "Border") {
            audioManager.Play("BallHit");
            FindObjectOfType<GameLevelManager>().GameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ring")
        {
            FindObjectOfType<Spawner>().slowDown(false);
        }
    }
}
