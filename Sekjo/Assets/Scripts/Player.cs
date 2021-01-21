using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    enum PlayerState
    {
        Standing, Jumping, Falling
    }

    PlayerState CurrentState = PlayerState.Falling;

    Rigidbody2D rb;
    //BoxCollider2D bc2;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //bc2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentState == PlayerState.Standing)
            {
                Jump();
            }
            else if (CurrentState == PlayerState.Jumping)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    void Jump()
    {
        Debug.Log("jump");
        CurrentState = PlayerState.Jumping;
        rb.velocity = new Vector2(0, 10);
    }

    IEnumerator Shoot()
    {
        Debug.Log("shoot");

        CurrentState = PlayerState.Falling;
        
        rb.isKinematic = true;
        rb.velocity = new Vector2(0,0);

        yield return new WaitForSeconds(0.5f);

        rb.isKinematic = false;
        rb.velocity = new Vector2(0,-30);

        yield break;
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        CurrentState = PlayerState.Standing;
    }
}
