using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    float speed;
    float rotationRadius;
    public bool isActivated;
    float borderLimit;

    public Color activeColor;
    public Color unActiveColor;

    public SpriteRenderer spriteRendererBack;
    public SpriteRenderer spriteRendererFront;

    public GameObject ringGroup;

    bool isInFromTop;
    bool isOutFromTop;

    // Start is called before the first frame update
    void Start()
    {
        unActivateRing();
        
        rotationRadius = FindObjectOfType<Spawner>().ringRotation;

        //ringGroup.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, rotationRadius));

        //transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, rotationRadius));

        borderLimit = FindObjectOfType<Spawner>().ringBorderLimit;


    }


    // Update is called once per frame
    private void Update()
    {
        speed = FindObjectOfType<Spawner>().ringSpeed;

        transform.position += Vector3.left * speed * Time.deltaTime;

        //unactivated the ring when out of limit
        if (transform.position.x <= borderLimit)
        {

            if (isActivated)
            {

                unActivateRing();
                FindObjectOfType<Spawner>().dequeRing();
                Destroy(this.gameObject, 5);
                gameLose();
            }

        }
    }

    public void activateRing() {
        isActivated = true;
        spriteRendererFront.color = activeColor;
        spriteRendererBack.color = activeColor;
    }

    public void unActivateRing() {
        isActivated = false;
        spriteRendererFront.color = unActiveColor;
        spriteRendererBack.color = unActiveColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if ball come in from top of the ring
        isInFromTop = isBallComesFromTop(collision.transform.position);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //check if ball come out from top of the ring
        isOutFromTop = isBallComesFromTop(collision.transform.position);
        if(isActivated) checkBallPassing();
    }

    //do stuff when ball comes from top to bottom (get point), or bottom to top (lose)
    void checkBallPassing() {
        if (isInFromTop == isOutFromTop)
        {

        }

        else if (isInFromTop && !isOutFromTop)
        {
            print("point");
            unActivateRing();
            scorePoint();
        }

        else if (!isInFromTop && isOutFromTop) {
            unActivateRing();
            gameLose();
        }
    }

    //func to add point
    void scorePoint() {
        FindObjectOfType<Spawner>().dequeRing();
        BBall ball = FindObjectOfType<BBall>();
        GameLevelManager gameManager = FindObjectOfType<GameLevelManager>();
        if (ball.isTouchingRing)
        {
            gameManager.RestCombo();
            ball.isTouchingRing = false;
        }
        else {
            gameManager.AddCombo(1);
        }

        gameManager.AddScore();
    }

    //func when lose
    void gameLose() {
        FindObjectOfType<Spawner>().dequeRing();
        FindObjectOfType<GameLevelManager>().GameOver();
        StopAllCoroutines();
    }

    //check ball position according the ring
    bool isBallComesFromTop(Vector3 ballPosition) {
        Vector3 ballDirectionLocal = transform.InverseTransformPoint(ballPosition);

        if (ballDirectionLocal.y > 0)
        {
            return true;
        }

        else if (ballDirectionLocal.y < 0) 
        {
            return false;
        }

        else return false;
    }
}
