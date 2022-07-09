using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{




    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log($"{name} --Collided with-- {collision.gameObject.name}");

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} **Triggered with** {other.gameObject.name}");
    }

}
