using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{   
    public GameObject platformPrefab;

    int platformIndex = 0;
    int distanceToNextPlatform = 4;

    float platformWidth = 2;
    float platformHeight = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        InitPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitPlatform()
    {
        for(int i = 0; i < 5; i++)
        {
            MakePlatform();
        }
    }

    public void MakePlatform()
    {
        Vector2 position = new Vector2(0,platformIndex * distanceToNextPlatform);

        GameObject newPlatformObj = Instantiate(platformPrefab, position, Quaternion.identity);

        newPlatformObj.transform.SetParent(transform);
        newPlatformObj.transform.localScale = new Vector2(platformWidth, platformHeight);

        SetSpeed(newPlatformObj);
        DecreasePlatformWidth();

        platformIndex ++;
    }

    void SetSpeed(GameObject newPlatformObj)
    {
        newPlatformObj.GetComponent<Platform>().velocity = Random.Range(-3,4);
    }

    void DecreasePlatformWidth()
    {
        if (platformWidth > 0.7f)
        {
            platformWidth -= 0.03f;
        }
    }
}
