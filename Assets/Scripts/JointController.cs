using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JointController : MonoBehaviour
{
    [SerializeField]
    [Range(-179f,179f)]
    public float min, max, rest;
    [SerializeField]
    public float rotationSpeed;
    [SerializeField]
    public float restSpeed;
    [SerializeField]
    public float angleApproximationFactor = 0.01f;
    [SerializeField]
    public GameObject pivot;
    private Renderer r;
    private Color c;

    [SerializeField]
    public PlayerInput playerInput;
    private InputAction inputAction;
    [SerializeField]
    public string inputActionName;
    public float axis;
    public float axisModifier = 1f;
    [SerializeField]
    public bool autoRest;

    [ExecuteInEditMode]
    void OnValidate(){
        rest = Mathf.Clamp(rest, min, max);
    }

    private void Awake() {
        inputAction = playerInput.actions[inputActionName];
    }

    void Start() {
        r = pivot.GetComponent<Renderer>();
        if (r != null) c = r.material.color;
        transform.localEulerAngles = new Vector3(0, 0, rest);
    }

    void Update() {
        axis = inputAction.ReadValue<float>() * axisModifier;
    }

    public void Rotate()
    {   
        // process rotation
        if (axis > 0f) {
            // transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(max, Vector3.forward), axis * rotationSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0f, 0f, axis * rotationSpeed) * Time.deltaTime);
        } else if (axis < 0f) {
            // transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(min, Vector3.forward), -axis * rotationSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0f, 0f, axis * rotationSpeed) * Time.deltaTime); 
        } else if (autoRest) {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(rest, Vector3.forward), restSpeed * Time.deltaTime);
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
        float z = Mathf.Clamp(NormalizeAngle(transform.transform.localEulerAngles.z), min, max);
        return (Mathf.Abs(z - rest) < angleApproximationFactor);
    }

    public bool Flexed() {
        float z = Mathf.Clamp(NormalizeAngle(transform.transform.localEulerAngles.z), min, max);
        return (Mathf.Abs(z - min) < angleApproximationFactor) || (Mathf.Abs(max - z) < angleApproximationFactor);
    }

    public void ClampRotation() {
        float z = Mathf.Clamp(NormalizeAngle(transform.transform.localEulerAngles.z), min, max);
        transform.localEulerAngles = new Vector3(0, 0, z);
    }

    public float NormalizeAngle(float angle) {
        if (angle >= 180) angle -= 360;
        if (angle <= -179) angle += 360;
        return angle;
    }
    
}
