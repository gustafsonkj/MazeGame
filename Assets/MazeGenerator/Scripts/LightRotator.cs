using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotator : MonoBehaviour {

    public GameObject player;
    Material sky;
    public Transform center;
    public Transform stars;
    public Vector3 axis = Vector3.right;
    public Vector3 desiredPosition;
    public float radius = .1f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;
    public float nightIntensity = .001f;
    public float noonIntensity = 1f;

    void Start()
    {
        sky = RenderSettings.skybox;
        player = GameObject.FindWithTag("Player");
        center = player.transform;
        transform.position = (transform.position - center.position).normalized * radius + center.position;
    }

    void Update()
    {
        stars.rotation = transform.rotation;
        transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        
        if (transform.position.y < 0)
        {
            RenderSettings.ambientIntensity = nightIntensity;
        }
        else
        {
            RenderSettings.ambientIntensity = noonIntensity;
        }
    }
}
