using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{   
    [SerializeField]
    public TJoint jawJoint;

    [SerializeField]
    public List<Food> edibles;

    private void Awake()
    {
        edibles = new List<Food>();
    }


    void FixedUpdate()
    {
        // how to check if flex just occurred
        //   "chewing" not, keeping mouth closed.
        if(jawJoint.MaxFlexed()) {
            for (int i = edibles.Count - 1; i >= 0; i--)
            {
                Food f = edibles[i];
                edibles.RemoveAt(i);
                f.Consume();
            }
        }
    }

    public void AddEdible(Food edible) {
        edibles.Add(edible);
    }
    
    public void RemoveEdible(Food edible) {
        edibles.Remove(edible);
    }
}
