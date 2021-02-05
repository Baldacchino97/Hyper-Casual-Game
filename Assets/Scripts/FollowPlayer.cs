using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public  GameObject PlayerObj;
    float smooth = 0.3f;
    Vector2 velocity = Vector2.zero;
    int yOffset = 2;

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = new Vector3(0, PlayerObj.transform.position.y + yOffset);
        
        if (targetPosition.y < transform.position.y) return;

        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smooth);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
