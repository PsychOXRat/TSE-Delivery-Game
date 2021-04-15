//using TMPro.EditorUtilities;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public bool isDriving;
    public float rawSpeed;
    public int speed;

    public Transform COM;
    public Rigidbody rb;
    public float horizontalInput;
    public float verticalInput;
    private bool handbrake;
    public float currentSteerAngle;
    public float currentSteerAngleAckermann;
    private float currentbrakeForce;
    private bool isBraking;

    public float tractionMultiplier = 1;
    //public AudioSource source;

    public enum DriveType { FWD, RWD, AWD }
    public enum SteeringType { FrontSteer, RearSteer, AllSteer }
    public DriveType drive;
    public SteeringType steerType;

    [SerializeField] [Range(0.0f, 1.0f)] private float awdFrontBias;
    [SerializeField] [Range(0.0f, 1.0f)] private float frontBrakeBias;
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;
    [Tooltip("This is the turn angle for the inside wheel, usually set higher than outside wheel")]
    [SerializeField] private float ackermannAngle;

    [SerializeField] public WheelCollider[] wheels = new WheelCollider[4];
    [SerializeField] private GameObject[] wheelMeshes = new GameObject[4];

    private void Start()
    {
        rb.centerOfMass = COM.localPosition;
    }
    private void Update()
    {
        UpdateWheels();
        speed = (int)rawSpeed;
        Debug.Log(rawSpeed);
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        rawSpeed = Vector3.Dot(rb.velocity, rb.transform.forward);
    }


    private void GetInput()
    {
        if (isDriving)
        {
            horizontalInput = Input.GetAxis(Horizontal);
            verticalInput = Input.GetAxis(Vertical);
            handbrake = (Input.GetAxis("Jump") != 0) ? true : false;
        }
    }

    private void TractionControl(WheelCollider wheel, WheelCollider wheel1)
    {
        WheelHit hit = new WheelHit();
        WheelHit hit2 = new WheelHit();
        if (wheel.GetGroundHit(out hit) || wheel1.GetGroundHit(out hit2))
        {
            if (hit.forwardSlip > 0.5f || hit2.forwardSlip > 0.5f)
            {
                tractionMultiplier -= 0.01f;
                //print("accel slip!");
                //source.Play();
            }
            else if(tractionMultiplier <1)
            {
                tractionMultiplier += 0.005f;
                //source.Pause();
            }
            else
            {
                //source.Pause();
            }
        }
    }

    private void HandleMotor()
    {

        switch (drive)
        {
            case DriveType.FWD:
                //TractionControl(frontLeftWheelCollider, frontRightWheelCollider);
                float wheelPower = motorForce * tractionMultiplier;
                //print(wheelPower);
                if (verticalInput > 0)
                {
                    wheels[0].motorTorque = (verticalInput * wheelPower);
                    wheels[1].motorTorque = (verticalInput * wheelPower);
                }
                else
                {
                    wheels[0].brakeTorque = (brakeForce * verticalInput) * frontBrakeBias;
                    wheels[1].brakeTorque = (brakeForce * verticalInput) * frontBrakeBias;
                    wheels[2].brakeTorque = (brakeForce * verticalInput) * (1 - frontBrakeBias);
                    wheels[3].brakeTorque = (brakeForce * verticalInput) * (1 - frontBrakeBias);
                }
                break;

            case DriveType.RWD:
                //TractionControl(rearLeftWheelCollider, rearRightWheelCollider);
                float wheelPowerRWD = motorForce * tractionMultiplier;
                print(wheelPowerRWD);
                wheels[2].motorTorque = verticalInput * wheelPowerRWD;
                wheels[3].motorTorque = verticalInput * wheelPowerRWD;
                break;

            case DriveType.AWD:
                //TractionControl(rearLeftWheelCollider, frontRightWheelCollider);
                float wheelPowerAWD = motorForce * tractionMultiplier;
                //print(wheelPowerAWD);
                if (isDriving)
                {
                    if (verticalInput >= 0 && rawSpeed >= 0)
                    {
                        wheels[0].motorTorque = verticalInput * (wheelPowerAWD * awdFrontBias);
                        wheels[1].motorTorque = verticalInput * (wheelPowerAWD * awdFrontBias);
                        wheels[2].motorTorque = verticalInput * (wheelPowerAWD * (1 - awdFrontBias));
                        wheels[3].motorTorque = verticalInput * (wheelPowerAWD * (1 - awdFrontBias));
                        wheels[0].brakeTorque = 0;
                        wheels[1].brakeTorque = 0;
                        wheels[2].brakeTorque = 0;
                        wheels[3].brakeTorque = 0;
                    }
                    else if (verticalInput < 0 && rawSpeed > 0)
                    {
                        wheels[0].brakeTorque = (brakeForce * -verticalInput) * frontBrakeBias;
                        wheels[1].brakeTorque = (brakeForce * -verticalInput) * frontBrakeBias;
                        wheels[2].brakeTorque = (brakeForce * -verticalInput) * (1 - frontBrakeBias);
                        wheels[3].brakeTorque = (brakeForce * -verticalInput) * (1 - frontBrakeBias);
                    }
                    else if (verticalInput < 0 && rawSpeed < 0)
                    {
                        wheels[0].motorTorque = verticalInput * (wheelPowerAWD * awdFrontBias);
                        wheels[1].motorTorque = verticalInput * (wheelPowerAWD * awdFrontBias);
                        wheels[2].motorTorque = verticalInput * (wheelPowerAWD * (1 - awdFrontBias));
                        wheels[3].motorTorque = verticalInput * (wheelPowerAWD * (1 - awdFrontBias));
                        wheels[0].brakeTorque = 0;
                        wheels[1].brakeTorque = 0;
                        wheels[2].brakeTorque = 0;
                        wheels[3].brakeTorque = 0;
                    }
                    else if (verticalInput > 0 && rawSpeed < -0)
                    {
                        wheels[0].brakeTorque = (brakeForce * -verticalInput) * frontBrakeBias;
                        wheels[1].brakeTorque = (brakeForce * -verticalInput) * frontBrakeBias;
                        wheels[2].brakeTorque = (brakeForce * -verticalInput) * (1 - frontBrakeBias);
                        wheels[3].brakeTorque = (brakeForce * -verticalInput) * (1 - frontBrakeBias);
                    }
                }
                else
                {
                    if (rawSpeed < 0.001 && rawSpeed > -0.001)
                    {
                        wheels[0].brakeTorque = 0;
                        wheels[1].brakeTorque = 0;
                        wheels[2].brakeTorque = 0;
                        wheels[3].brakeTorque = 0;
                    }
                    else
                    {
                        wheels[0].brakeTorque = 3000;
                        wheels[1].brakeTorque = 3000;
                        wheels[2].brakeTorque = 3000;
                        wheels[3].brakeTorque = 3000;
                    }
                }
                //else if (verticalInput > 0 && rawSpeed < -0.05)
                //{
                //    wheels[0].brakeTorque = (brakeForce * verticalInput) * frontBrakeBias;
                //    wheels[1].brakeTorque = (brakeForce * verticalInput) * frontBrakeBias;
                //    wheels[2].brakeTorque = (brakeForce * verticalInput) * (1 - frontBrakeBias);
                //    wheels[3].brakeTorque = (brakeForce * verticalInput) * (1 - frontBrakeBias);
                //}
                break;
        }

        //currentbrakeForce = isBraking ? brakeForce : 0f;

    }

    private void HandleSteering()
    {
        switch (steerType)
        {
            case SteeringType.FrontSteer:
                if (horizontalInput >0)
                {
                    currentSteerAngle = maxSteerAngle * horizontalInput;
                    currentSteerAngleAckermann = ackermannAngle * horizontalInput;
                    wheels[0].steerAngle = currentSteerAngle;
                    wheels[1].steerAngle = currentSteerAngleAckermann;
                }
                else if (horizontalInput <0)
                {
                    currentSteerAngle = maxSteerAngle * horizontalInput;
                    currentSteerAngleAckermann = ackermannAngle * horizontalInput;
                    wheels[0].steerAngle = currentSteerAngleAckermann;
                    wheels[1].steerAngle = currentSteerAngle;
                }
                else
                {
                    wheels[0].steerAngle = 0;
                    wheels[1].steerAngle = 0;
                }
                break;
            case SteeringType.RearSteer:
                if (horizontalInput > 0)
                {
                    currentSteerAngle = maxSteerAngle * horizontalInput;
                    currentSteerAngleAckermann = ackermannAngle * horizontalInput;
                    wheels[2].steerAngle = -currentSteerAngle;
                    wheels[3].steerAngle = -currentSteerAngleAckermann;
                }
                else if (horizontalInput < 0)
                {
                    currentSteerAngle = maxSteerAngle * horizontalInput;
                    currentSteerAngleAckermann = ackermannAngle * horizontalInput;
                    wheels[2].steerAngle = -currentSteerAngleAckermann;
                    wheels[3].steerAngle = -currentSteerAngle;
                }
                else
                {
                    wheels[2].steerAngle = 0;
                    wheels[3].steerAngle = 0;
                }
                break;
            case SteeringType.AllSteer:
                if (horizontalInput > 0)
                {
                    currentSteerAngle = maxSteerAngle * horizontalInput;
                    currentSteerAngleAckermann = ackermannAngle * horizontalInput;
                    wheels[0].steerAngle = currentSteerAngle;
                    wheels[1].steerAngle = currentSteerAngleAckermann;
                    wheels[2].steerAngle = -currentSteerAngle;
                    wheels[3].steerAngle = -currentSteerAngleAckermann;
                }
                else if (horizontalInput < 0)
                {
                    currentSteerAngle = maxSteerAngle * horizontalInput;
                    currentSteerAngleAckermann = ackermannAngle * horizontalInput;
                    wheels[0].steerAngle = currentSteerAngleAckermann;
                    wheels[1].steerAngle = currentSteerAngle;
                    wheels[2].steerAngle = -currentSteerAngleAckermann;
                    wheels[3].steerAngle = -currentSteerAngle;
                }
                else
                {
                    wheels[0].steerAngle = 0;
                    wheels[1].steerAngle = 0;
                    wheels[2].steerAngle = 0;
                    wheels[3].steerAngle = 0;
                }
                break;
        }
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(wheels[0], wheelMeshes[0].transform);
        UpdateSingleWheel(wheels[1], wheelMeshes[1].transform);
        UpdateSingleWheel(wheels[2], wheelMeshes[2].transform);
        UpdateSingleWheel(wheels[3], wheelMeshes[3].transform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}