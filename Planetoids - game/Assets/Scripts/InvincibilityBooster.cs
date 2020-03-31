using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityBooster : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            FindObjectOfType<PlayerController>().MakeInvincible();
            Destroy(gameObject);
        }
    }
}
