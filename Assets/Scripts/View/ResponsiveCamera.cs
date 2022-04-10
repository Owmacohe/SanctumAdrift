using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    Transform player;
    float startDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startDistance = (player.position - transform.position).magnitude;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, direction.magnitude + 1);
        
        bool isHittingPlayer = hit.transform.Equals(player);

        while (isHittingPlayer)
        {
            if (direction.magnitude < startDistance)
            {
                transform.position -= direction.normalized;
            }
        }

        while (!isHittingPlayer)
        {
            if (direction.magnitude > 1)
            {
                transform.position += direction.normalized;
            }
        }
    }
}
