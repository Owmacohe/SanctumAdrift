using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float minDistance = 5;

    Transform player;
    float startDistance;
    Vector3 direction;
    bool isHittingPlayer;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startDistance = (player.position - transform.position).magnitude;
    }
    
    void FixedUpdate()
    {
        Vector3 amount = direction.normalized * speed;

        if (isHittingPlayer)
        {
            if (direction.magnitude < startDistance)
            {
                transform.position -= amount;
            }
        }
        else
        {
            if (direction.magnitude > minDistance)
            {
                transform.position += amount;
            }
        }
    }

    void Update()
    {
        direction = player.position - transform.position;
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, direction.magnitude + 1);
        
        isHittingPlayer = hit.transform.Equals(player);
    }
}
