using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFallowCamera : MonoBehaviour
{
    [SerializeField] float MaxAngleBeforRotate = 25;
    public bool rotate = false;
    [SerializeField] float RotationSpeed = 0.01f;
    float ActualRotationSpeed;
    private void Start()
    {
        ActualRotationSpeed = RotationSpeed;
    }
    private void Update()
    {
        float AngleToCamera = Quaternion.Angle(transform.localRotation, Camera.main.transform.localRotation);
        if (AngleToCamera > MaxAngleBeforRotate)
        {
            if (!rotate)
            {
                rotate = true;
            }
        }
        if (rotate)
        {
            ActualRotationSpeed = RotationSpeed * (AngleToCamera > MaxAngleBeforRotate/2 ? 10 * (AngleToCamera / 45) : 1);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation,Camera.main.transform.localRotation,Time.unscaledDeltaTime*ActualRotationSpeed);
            if (transform.localRotation == Camera.main.transform.localRotation)
            {
                rotate = false;
            }
        }
    }
}
