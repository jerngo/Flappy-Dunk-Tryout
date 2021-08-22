using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleRing : MonoBehaviour
{
    float speed;
    private void Update()
    {
        speed = FindObjectOfType<Spawner>().ringSpeed;

        Move();
    }
    private void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
