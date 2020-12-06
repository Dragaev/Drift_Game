using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public AxleInfo[] carAxis = new AxleInfo[2];
    public WheelCollider[] wheelColliders;
    public float carSpeed;
    public float steerAngle;//угол поворота колес
    public Transform centerOfMass;
    [Range(0,1)]//диапазон для steerHelp
    public float steerHelpValue = 0;
    bool onGround;
    public float nitroPower;
    public GameObject nitroEffects;

    float lastYrotation;

    float horInput;
    float verInput;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        horInput = Input.GetAxis("Horizontal");//принимает нажатие влево-вправо
        verInput = Input.GetAxis("Vertical");//принимает нажатие w-s возвращает -1 или 1

        CheckOnGround();

        Accelerate();
        ManageNitro();
        SteerHelpAssist();
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

    void SteerHelpAssist()
    {
        //проверяем нахождение колес на земле
        if (!onGround)
            return;
        //проверка от внезапного доворачивания от 360 к 0
        if (Mathf.Abs(transform.rotation.eulerAngles.y - lastYrotation) < 10f)
        {
            float turnAdjust = (transform.rotation.eulerAngles.y - lastYrotation) * steerHelpValue;
            Quaternion rotateHelp = Quaternion.AngleAxis(turnAdjust, Vector3.up);//поворачиваем машину вправо-влево по up
            rb.velocity = rotateHelp * rb.velocity;
        }
        lastYrotation = transform.rotation.eulerAngles.y;

    }

    void CheckOnGround()
    {
        onGround = true;
        foreach (WheelCollider wheelCol in wheelColliders)
        {
            if (!wheelCol.isGrounded)
                onGround = false;
        }
    }

    void ManageNitro()
    {
        //verInput- смещение по горизонатльной оси, нужен, чтобы нитро срабатывало только когда жмем клавишу вперед
        if (Input.GetKey(KeyCode.LeftShift) && verInput > 0.01f)
        {
            rb.AddForce(transform.forward * nitroPower);
            nitroEffects.SetActive(true);
        }
        else
        {
            //если эффект нитро активен-activeSelf
            if (nitroEffects.activeSelf)
                nitroEffects.SetActive(false);
        }
            
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