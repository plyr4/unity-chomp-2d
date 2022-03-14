using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    [SerializeField]
    [Range(0f,179f)]
    public float min;
    [SerializeField]
    [Range(0f,179f)]
    public float max;
    [SerializeField]
    public float rotationSpeed;
    [SerializeField]
    public float returnSpeed;
    [SerializeField]
    public float angleApproximationFactor = 0.01f;
    [SerializeField]
    public GameObject pivot;
    private Renderer r;
    private Color c;

    [SerializeField]
    public KeyCode key;
    bool keyDown;
    
    void Start() {
        r = pivot.GetComponent<Renderer>();
        if (r != null) c = r.material.color;
    }

    void Update() {
        keyDown = Input.GetKey(key);
    }

    public void Rotate()
    {
        // process rotation
        if (keyDown) {
            transform.Rotate(new Vector3(0f, 0f, rotationSpeed) * Time.deltaTime);        
        } else {
            transform.Rotate(new Vector3(0f, 0f, -returnSpeed) * Time.deltaTime);        
        }

        // clamp rotation
        ClampRotation();

        // control pivot color
        UpdatePivotColor();
    }

    public void UpdatePivotColor() {
        if (r != null) {
            r.material.color = Color.yellow;
            if (Flexed()) r.material.color = Color.green;
            if (Idle()) r.material.color = c;
        }
    }

    public bool Idle() {
        return (Mathf.Abs(ClampAngle(transform.transform.localEulerAngles.z, min, max) - min) < angleApproximationFactor);
    }

    public bool Flexed() {
        return (Mathf.Abs(max - ClampAngle(transform.transform.localEulerAngles.z, min, max)) < angleApproximationFactor);
    }

    public void ClampRotation() {
        transform.localEulerAngles = new Vector3(0, 0, ClampAngle(transform.transform.localEulerAngles.z, min, max));
    }

    public float ClampAngle(float angle ,  float min,  float max) {
        if (angle<90 || angle>270){
            if (angle>180) angle -= 360;
            if (max>180) max -= 360;
            if (min>180) min -= 360;
        }    
        angle = Mathf.Clamp(angle, min, max);
        if (angle<0) angle += 360;
        return angle;
    }
}
