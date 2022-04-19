using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    [SerializeField] float startDistance = 9.8f;
    [SerializeField] float speed = 0.75f;
    [SerializeField] float minDistance = 1;

    Transform player;
    Vector3 direction;
    bool isHittingPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        isHittingPlayer = CheckHitting();
    }

    void FixedUpdate()
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
    
    bool CheckHitting()
    {
        direction = (player.position + Vector3.up) - transform.position;
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, 2 * direction.magnitude);

        return hit.transform.Equals(player); // TODO: gets too close to some models and throws errors (fixed by growing minDistance)
    }
}