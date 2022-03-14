using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [Header("JOINTS")]
    public JointController shoulderJoint;
    public JointController elbowJoint;
    public JointController wristJoint;
    public JointController jawJoint;

    void FixedUpdate()
    {
        if (shoulderJoint != null) shoulderJoint.Rotate();
        if (elbowJoint != null) elbowJoint.Rotate();
        if (wristJoint != null) wristJoint.Rotate();
        if (jawJoint != null) jawJoint.Rotate();
    }
}

