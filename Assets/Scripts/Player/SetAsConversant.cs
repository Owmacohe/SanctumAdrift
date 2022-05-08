using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsConversant : MonoBehaviour
{
    public void SetConversant()
    {
        FindObjectOfType<PlayerController>().conversant = transform;
    }
}
