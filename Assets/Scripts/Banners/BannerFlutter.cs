using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerFlutter : MonoBehaviour
{
    enum BannerSizes { Small, Medium, Large, Huge };
    [Tooltip("Length of the banner to flutter")]
    [SerializeField] BannerSizes size;
    [Tooltip("Chance for the banner to change velocity (aka flutter)")]
    [Range(1, 10)] [SerializeField] int flutterFrequency = 5;
    [Tooltip("Which way the banner should flutter")]
    [Range(-1, 1)] [SerializeField] int direction = 1;

    float speed; // Maximum amount of velocity that can be applied
    float max; // Maximum x-axis rotation that can be applied to the banner
    Transform bannerTransform; // Transform component of the banner
    Rigidbody bannerRB; // RigidBody component of the banner

    void Start()
    {
        bannerTransform = GetComponent<Transform>();
        bannerRB = GetComponent<Rigidbody>();

        // Adding a RigidBody if the banner doesn't already have one
        if (bannerRB == null)
        {
            bannerRB = gameObject.AddComponent<Rigidbody>();
            bannerRB.useGravity = false;
        }

        // Setting the speed and max x-axis rotation values
        switch (size)
        {
            case BannerSizes.Small:
                speed = 0.2f;
                max = 0.2f;
                break;
            case BannerSizes.Medium:
                speed = 0.1f;
                max = 0.2f;
                break;
        }
    }

    void FixedUpdate()
    {
        float speedRange = Random.Range(-speed, speed);

        if (bannerTransform.localRotation.x >= 0 && bannerTransform.localRotation.x < max)
        {
            // Checking to see if it can flutter
            if (Random.Range(0, 100) <= flutterFrequency)
            {
                bannerRB.angularVelocity = direction * transform.TransformVector(Vector3.right * speedRange); // Setting a negative to positive velocity value
            }
        }
        else if (bannerTransform.localRotation.x < 0)
        {
            bannerRB.angularVelocity = direction * transform.TransformVector(Vector3.right * (speed / 20)); // Setting a slight positive velocity value
        }
        else if (bannerTransform.localRotation.x >= max)
        {
            bannerRB.angularVelocity = direction * transform.TransformVector(Vector3.right * (-speed / 20)); // Setting a slight negative velocity value
        }

        //Debug.Log("Rotation: " + bannerTransform.rotation.x + "\nVelocity: " + bannerRB.angularVelocity.x);
    }
}
