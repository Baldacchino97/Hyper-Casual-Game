﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    enum PlayerState
    {
        Standing, Jumping, Falling
    }

    public Transform playerParentTransform;

    PlayerState CurrentState = PlayerState.Falling;

    Rigidbody2D rb;
    BoxCollider2D bc2D;
    
    // Start is called before the first frame update
    float previousPosXofParent;

    bool isDead = false;

    public GameObject shootEffectPrefab;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        GetInput();
        BounceAtWall();
        GetPreviousPositionOfParent();
        DeadCheck();
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

    void GetPreviousPositionOfParent()
    {
        previousPosXofParent = transform.parent.transform.position.x;
    }

    void Jump()
    {
        bc2D.enabled = false;
        CurrentState = PlayerState.Jumping;

        rb.velocity = new Vector2(ParentVelocity(), 10);

        transform.SetParent(playerParentTransform);
    }

    float ParentVelocity()
    {
        return (transform.parent.transform.position.x - previousPosXofParent) / Time.deltaTime;
    }

    IEnumerator Shoot()
    {
        Destroy(Instantiate(shootEffectPrefab, transform.position, Quaternion.identity), 1.0f);


        CurrentState = PlayerState.Falling;
        
        rb.isKinematic = true;
        rb.velocity = new Vector2(0,0);

        yield return new WaitForSeconds(0.5f);

        //ChangeBackgroundColor();

        bc2D.enabled = true;
        rb.isKinematic = false;
        rb.velocity = new Vector2(0,-30);

        yield break;
    }

    void BounceAtWall()
    {
        if (rb.position.x < - 2.5f)
        {
            rb.position = new Vector2(-2.5f, rb.position.y);
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }

        if (rb.position.x > 2.5f)
        {
            rb.position = new Vector2(2.5f, rb.position.y);
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
    }

    void DeadCheck()
    {
        if (isDead == false && Camera.main.transform.position.y - transform.position.y > 10)
        {
            isDead = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        rb.velocity = Vector2.zero;
        CurrentState = PlayerState.Standing;

        transform.SetParent(other.gameObject.transform);
        StartCoroutine(other.gameObject.GetComponent<Platform>().LandingEffect());

        GameObject.Find("GameManager").GetComponent<ScoreManager>().AddScore();
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        GameObject.Find("Platform").GetComponent<PlatformManager>().MakePlatform();
        Destroy(other.gameObject, 0.1f);
    }
}
