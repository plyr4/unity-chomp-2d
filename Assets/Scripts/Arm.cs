using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [Header("JOINTS")]
    [SerializeField]
    public TJoint[] joints;

    void FixedUpdate()
    {
        foreach (TJoint joint in joints)
        {
            if (joint != null) joint.Rotate();
        }
    }
}

