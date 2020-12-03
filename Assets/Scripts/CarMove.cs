using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public AxleInfo[] carAxis = new AxleInfo[2];
    public float carSpeed;
    public float steerAngle;//угол поворота колес

    float horInput;
    float verInput;

    private void FixedUpdate()
    {
        horInput = Input.GetAxis("Horizontal");//принимает нажатие влево-вправо
        verInput = Input.GetAxis("Vertical");//принимает нажатие w-s возвращает -1 или 1

        Accelerate();
    }

    void Accelerate()
    {
        foreach(AxleInfo axle in carAxis)
        {
            if (axle.steering)
            {
                axle.rightWheel.steerAngle = steerAngle*horInput ;
                axle.leftWheel.steerAngle = steerAngle * horInput;
            }
            if (axle.motor)
            {
                axle.rightWheel.motorTorque = carSpeed * verInput;
                axle.leftWheel.motorTorque = carSpeed * verInput;
            }
            VisualWheelsToColliders(axle.rightWheel, axle.visRightWheel);
            VisualWheelsToColliders(axle.leftWheel, axle.visLeftWheel);
        }
    }
    void VisualWheelsToColliders(WheelCollider col,Transform visWheel)
    {
        Vector3 position;
        Quaternion rotatation;

        col.GetWorldPose(out position, out rotatation);

        visWheel.position = position;
        visWheel.rotation = rotatation;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider rightWheel;
    public WheelCollider leftWheel;

    public Transform visRightWheel;//модель правого колеса
    public Transform visLeftWheel;

    public bool steering;//ось может крутиться
    public bool motor;
}