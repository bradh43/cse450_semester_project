using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float backgroundLength, startPosition;
    public GameObject mainCamera;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        backgroundLength = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float endBackground = (mainCamera.transform.position.x * (1 - parallaxEffect));
        float moveDistance = (mainCamera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPosition + moveDistance, transform.position.y, transform.position.z);

        if(endBackground > startPosition + backgroundLength){
            startPosition += backgroundLength;
        } else if(endBackground < startPosition - backgroundLength){
            startPosition -= backgroundLength;
        }
    }
}
