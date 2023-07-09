using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public void GetInput()
    {
        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");
}

    private void Steer()
    {
        m_SteeringAngle = maxStreetAngle * m_HorizontalInput;
        frontRightW.steerAngle = m_SteeringAngle;
        frontLeftW.steerAngle = m_SteeringAngle;
    }

    private void Accelerater()
    {
        rearRightW.motorTorque = m_VerticalInput * motorForce;
        rearLeftW.motorTorque = m_VerticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontRightW, frontRightT);
        UpdateWheelPose(frontLeftW, frontLeftT);
        UpdateWheelPose(rearRightW, rearRightT);
        UpdateWheelPose(rearLeftW, rearLeftT);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 _pos = transform.position;
        Quaternion _quat = transform.rotation;

        collider.GetWorldPose(out _pos, out _quat);

        transform.position = _pos;
        transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerater();
        UpdateWheelPoses();
    }

    private float m_HorizontalInput;
    private float m_VerticalInput;
    private float m_SteeringAngle;

    public WheelCollider frontRightW, frontLeftW;
    public WheelCollider rearRightW, rearLeftW;
    public Transform frontRightT, frontLeftT;
    public Transform rearRightT, rearLeftT;
    public float maxStreetAngle = 30;
    public float motorForce = 50;


}
