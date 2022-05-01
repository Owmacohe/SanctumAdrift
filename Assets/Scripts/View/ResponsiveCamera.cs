using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    [Tooltip("Global default distance from the player")]
    [SerializeField] float startDistance = 3;
    [Tooltip("Speed at which the camera responds")]
    [SerializeField] float speed = 0.75f;
    [Tooltip("Minimum distance that the camera can get to the player")]
    [SerializeField] float minDistance = 1;

    PlayerController player; // Player script
    Transform playerTransform; // Transform of the player
    Vector3 direction; // Vector between the camera and player
    bool isHittingPlayer; // Whether the raycast between the camera and player is hitting

    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        player = temp.GetComponent<PlayerController>();
        playerTransform = temp.transform;
    }
    
    void Update()
    {
        if (!player.isRotating)
        {
            isHittingPlayer = CheckHitting();
        }
    }

    void FixedUpdate()
    {
        Vector3 amount = direction.normalized * speed;
        
        // Zooming out if the player can be "seen"
        if (isHittingPlayer)
        {
            if (direction.magnitude < startDistance)
            {
                transform.position -= amount;
                //print("away");
            }
            
            // Making sure it doesn't zoom out too far
            if (!CheckHitting())
            {
                transform.position += amount;
                //print("fix close");
            }
        }
        // Zooming in if the player can't be "seen"
        else
        {
            if (direction.magnitude > minDistance)
            {
                transform.position += amount;
                //print("close");
            }
        }
    }
    
    /// <summary>
    /// Casts a ray to see if the camera can "see" the player
    /// </summary>
    /// <returns>Whether the ray hits the player</returns>
    bool CheckHitting()
    {
        direction = (playerTransform.position + Vector3.up) - transform.position; // TODO: should the ray point to the bottom of the player?
        
        RaycastHit hit;
        // The ray goes out twice as far as the direction Vector
        // just to make sure
        Physics.Raycast(transform.position, direction, out hit, 2 * direction.magnitude);

        return hit.transform.Equals(playerTransform); // TODO: gets too close to some models and throws errors (fixed by growing minDistance)
    }
}