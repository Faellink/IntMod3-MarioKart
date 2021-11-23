using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public List<Dot_Truck> truck_Infos;
    public float motorValue;
    public float steeringValue;
    public float breakValue;

    private float _motor;
    private float _steering;
    private float _breakTorque;

    private Rigidbody _rb;
    private NavMeshAgent _agent;

    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _motor = maxMotorTorque * motorValue;
        _steering = maxSteeringAngle * steeringValue;
        _breakTorque = Mathf.Abs(breakValue);
        
        if(_breakTorque > 0.001)
        {
            _breakTorque = maxMotorTorque;
            _motor = 0;
            _rb.drag = 2.0f;
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

    public void ChangeDestination(Vector3 destination)
    {
        _agent.SetDestination(destination);
        transform.LookAt(_agent.destination);
    }

    public void SetSpeedValues(float maxMotorTorque, float motorValue, float steeringValue, float breakValue)
    {
        this.maxMotorTorque = maxMotorTorque;
        this.motorValue = motorValue;
        this.steeringValue = steeringValue;
        this.breakValue = breakValue;
    }
    
}
