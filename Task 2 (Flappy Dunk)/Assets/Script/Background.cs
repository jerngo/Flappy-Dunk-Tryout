using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 limitPos;
    // Start is called before the first frame update
    float speed;
    private void Update()
    {
        speed = FindObjectOfType<Spawner>().ringSpeed;

        Move();

        if (transform.position.x<=limitPos.x){
            transform.position = startPos;
        }
    }
    private void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
