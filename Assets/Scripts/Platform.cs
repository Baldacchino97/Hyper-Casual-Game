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

    public IEnumerator LandingEffect()
    {
        Vector2 originalPosition = transform.position;
        float yChangeValue = 0.7f;

        while (yChangeValue > 0)
        {
            yChangeValue -= 0.1f;
            yChangeValue = Mathf.Clamp(yChangeValue, 0, 1.5f);
            transform.position = new Vector2(transform.position.x, originalPosition.y - yChangeValue);
            yield return 0;
        }

        yield break;
    }
}
