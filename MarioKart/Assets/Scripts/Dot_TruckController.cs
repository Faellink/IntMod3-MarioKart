using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

[Serializable]
public class Dot_Truck : Object
{
    public WheelCollider leftWheel;
    public GameObject leftWheelMesh;
    public WheelCollider rightWheel;
    public GameObject rightWheelMesh;
    public bool motor;
    public bool steering;
    public bool reverseTurn;
}

public class Dot_TruckController : MonoBehaviour
{

    public GameController gameController;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Transform finishLine;
    public Transform secondCheckpoint;
    public Transform thirdCheckpoint;
    public Transform fourthCheckpoint;
    public List<Dot_Truck> truck_Infos;

    private float _motor;
    private float _steering;
    private float _breakTorque;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        _motor = maxMotorTorque * Input.GetAxis("Vertical");
        _steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        _breakTorque = Mathf.Abs(Input.GetAxis("Jump"));

        if(_breakTorque > 0.001)
        {
            _breakTorque = maxMotorTorque;
            _motor = 0;
            _rb.drag = 1.5f;
        }
        else
        {
            _breakTorque = 0;
            _rb.drag = 0;
        }

        foreach (Dot_Truck truck_Info in truck_Infos)
        {
            if (truck_Info.steering)
            {
                truck_Info.leftWheel.steerAngle = truck_Info.rightWheel.steerAngle = ((truck_Info.reverseTurn) ? -1 : 1) * _steering;
            }

            if (truck_Info.motor == true)
            {
                truck_Info.leftWheel.motorTorque = _motor;
                truck_Info.rightWheel.motorTorque = _motor;
            }

            truck_Info.leftWheel.brakeTorque = _breakTorque;
            truck_Info.rightWheel.brakeTorque = _breakTorque;

            VisualizeWheel(truck_Info);

        }

    }

    public void VisualizeWheel(Dot_Truck wheelPair)
    {
        wheelPair.leftWheel.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelPair.leftWheelMesh.transform.position = pos;
        wheelPair.leftWheelMesh.transform.rotation = rot;
        wheelPair.rightWheel.GetWorldPose(out pos, out rot);
        wheelPair.rightWheelMesh.transform.position = pos;
        wheelPair.rightWheelMesh.transform.rotation = rot;
    }

    IEnumerator FlipCrashedVehicle()
    {
        yield return new WaitForSeconds(5.0f);
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x, 2, currentPosition.z);
        transform.rotation = Quaternion.Euler(0, 100, 0);

        switch (gameController.numCheckPoint)
        {
            case 0:
            case 4:
                transform.LookAt(finishLine);
                break;
            case 1:
                transform.LookAt(secondCheckpoint);
                break;
            case 2:
                transform.LookAt(thirdCheckpoint);
                break;
            case 3:
                transform.LookAt(fourthCheckpoint);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("road"))
        {
            StartCoroutine(FlipCrashedVehicle());
        }
    }

}
