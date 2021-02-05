using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float velocity = 1;
    float distance = 2;
    float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = new Vector2(Mathf.Sin(angle) * distance, transform.position.y);
        angle += velocity / 100f;
    }
}
