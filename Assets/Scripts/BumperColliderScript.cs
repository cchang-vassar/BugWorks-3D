using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperColliderScript : MonoBehaviour
{
    public bool collided = false;

    void Start()
    {
        Collider collider = this.GetComponent<BoxCollider>();
        collider.isTrigger = true;
    }

     void OnCollisionEnter(Collision collision)
      {
        ContactPoint[] contacts = new ContactPoint[] { };
        contacts = collision.contacts;

        if (collision.GetContacts(contacts) > 0)
        {
            collided = true;
        }
      }
}
