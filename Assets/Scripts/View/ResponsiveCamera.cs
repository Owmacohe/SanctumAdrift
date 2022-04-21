using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    [SerializeField] float startDistance = 9.8f;
    [SerializeField] float speed = 0.75f;
    [SerializeField] float minDistance = 1;

    PlayerController player;
    Transform playerTransform;
    Vector3 direction;
    bool isHittingPlayer;

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
        if (!player.isRotating)
        {
            Vector3 amount = direction.normalized * speed;
        
            if (isHittingPlayer)
            {
                if (direction.magnitude < startDistance)
                {
                    transform.position -= amount;
                    //print("away");
                }
            
                if (!CheckHitting())
                {
                    transform.position += amount;
                    //print("fix close");
                }
            }
            else
            {
                if (direction.magnitude > minDistance)
                {
                    transform.position += amount;
                    //print("close");
                }
            }
        }
    }
    
    bool CheckHitting()
    {
        direction = (playerTransform.position + Vector3.up) - transform.position;
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, 2 * direction.magnitude);

        return hit.transform.Equals(playerTransform); // TODO: gets too close to some models and throws errors (fixed by growing minDistance)
    }
}