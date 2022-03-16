using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour
{
    public bool edible;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mouth")
        {
            edible = true;
            Mouth mouth = other.GetComponent<Mouth>();
            if (mouth != null) {
                mouth.AddEdible(this);
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mouth")
        {
            edible = false;
            Mouth mouth = other.GetComponent<Mouth>();
            if (mouth != null) {
                mouth.RemoveEdible(this);
            }
        }
    }

    public void Consume() {
        print("consume!");
        if (gameObject) Destroy(gameObject);
    }
}
