using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPartScript : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        ColliderScript.colliderInstance.CheckCollision(transform.name, collision.transform.name);
    }
   
}
