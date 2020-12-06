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

    //раньше вместо  Awake был Start, но при смене камеры у нас боковая камера не успевала просчитывать свои координаты
    private void Awake()
    {
        offset = transform.position - playerTarget.position;
        startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        //линейная интерполяция
        transform.position =Vector3.Lerp(transform.position, playerTarget.position + playerTarget.rotation*offset,moveSpeed*Time.fixedDeltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerTarget.rotation * startRotation,rotationSpeed*Time.fixedDeltaTime);
    }
}
