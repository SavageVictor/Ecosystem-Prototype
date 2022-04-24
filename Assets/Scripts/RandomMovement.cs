using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float accelerationTime = 2f;
    [SerializeField] private float maxSpeed = 5f;
    private Vector2 movement;
    [ReadOnly]
    [SerializeField] float timeLeft;
     
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft = accelerationTime;
        }
    }
     
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * maxSpeed * Time.fixedDeltaTime);
    }
}
