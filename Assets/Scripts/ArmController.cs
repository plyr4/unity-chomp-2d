using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [Header("JOINTS")]
    [SerializeField]
    public JointController[] joints;

    void FixedUpdate()
    {
        foreach(JointController joint in joints)
        {
            if (joint != null) joint.Rotate();
        }
    }
}

