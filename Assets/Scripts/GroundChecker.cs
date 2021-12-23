using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [HideInInspector]
    public bool isOnGround;
    [HideInInspector]
    public GameObject groundObject;

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.Equals(transform.parent.gameObject))
        {
            isOnGround = true;
            groundObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.Equals(transform.parent.gameObject))
        {
            isOnGround = false;
            groundObject = null;
        }
    }
}
