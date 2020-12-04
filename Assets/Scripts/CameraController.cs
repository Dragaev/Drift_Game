using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTarget;
    public float moveSpeed;
    public float rotationSpeed;

    Quaternion startRotation;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerTarget.position;
        startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        //линейная интерполяция
        transform.position =Vector3.Lerp(transform.position, playerTarget.position + transform.rotation*offset,moveSpeed*Time.fixedDeltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerTarget.rotation * startRotation,rotationSpeed*Time.fixedDeltaTime);
    }
}
