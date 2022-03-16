using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TJoint : MonoBehaviour
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
    public enum InputAxis{
    leftX,
    leftY,
    rightX,
    rightY,
    button
    };
    public InputAxis inputAxis;
    public bool useJoystickMod;
    
    [SerializeField]
    public string inputActionName;
    public float axis;
    public float axisModifier = 1f;
    public float lerpSpeed = 5f;
    [SerializeField]
    public bool autoRest;
    public bool clamp;

    [ExecuteInEditMode]
    void OnValidate(){
        rest = Mathf.Clamp(rest, min, max);
    }

    private void Awake() {
        // inputAction = playerInput.actions[inputActionName];
        r = pivot.GetComponent<Renderer>();
        if (r != null) c = r.material.color;
        transform.localEulerAngles = new Vector3(0, 0, rest);
    }

    void Start() {
    }

    void Update() {
        // axis = inputAction.ReadValue<float>() * axisModifier;
    }

    public void Rotate()
    {   
        // float leftStickX = playerInput.actions[inputActionName + "StickYAxis"].ReadValue<float>()*-1;
        // float leftStickY = playerInput.actions[inputActionName + "StickXAxis"].ReadValue<float>();
        // float inputXAxis = 0;
        // float inputYAxis = 0;
        switch (inputAxis) {
            case InputAxis.leftX:
                axis = playerInput.actions["LeftStickXAxis"].ReadValue<float>();
                break;
            case InputAxis.leftY:
                axis = playerInput.actions["LeftStickYAxis"].ReadValue<float>();
                break;
            case InputAxis.rightX:
                axis = playerInput.actions["RightStickXAxis"].ReadValue<float>();
                break;
            case InputAxis.rightY:
                axis = playerInput.actions["RightStickYAxis"].ReadValue<float>();
                break;
            case InputAxis.button:
                axis = playerInput.actions["ButtonAxis"].ReadValue<float>();
                break;
            default:
                break;
        }


        // process rotation
        if (axis > 0f) {
            // transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(max, Vector3.forward), axis * rotationSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0f, 0f, axis * rotationSpeed) * Time.deltaTime);
            // if ((useJoystickMod && playerInput.actions["LeftTrigger"].ReadValue<float>() != 0f) || (!useJoystickMod && playerInput.actions["LeftTrigger"].ReadValue<float>() == 0f)) {
            //     float currentAngle = transform.eulerAngles.z;
            //     float newAngle = Mathf.Atan2 (inputXAxis, -inputYAxis) * 180 / Mathf.PI;
            //     float angle = Mathf.LerpAngle (currentAngle, newAngle, lerpSpeed * Time.deltaTime);
            //     transform.eulerAngles = new Vector3 (0, 0, angle);
            // } 
        } else if (axis < 0f) {
            // transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(min, Vector3.forward), -axis * rotationSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0f, 0f, axis * rotationSpeed) * Time.deltaTime); 
        } else if (autoRest) {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(rest, Vector3.forward), restSpeed * Time.deltaTime);
        }

        // clamp rotation
        if (clamp) ClampRotation();

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
        return MinFlexed() || MaxFlexed();
    }

    public bool MinFlexed() {
        float z = Mathf.Clamp(NormalizeAngle(transform.transform.localEulerAngles.z), min, max);
        return Mathf.Abs(z - min) < angleApproximationFactor;
    }

    public bool MaxFlexed() {
        float z = Mathf.Clamp(NormalizeAngle(transform.transform.localEulerAngles.z), min, max);
        return Mathf.Abs(max - z) < angleApproximationFactor;
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
