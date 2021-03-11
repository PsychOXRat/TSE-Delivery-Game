using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(WheelCollider))]
public class WheelControls : MonoBehaviour
{
    private WheelCollider _wheelCollider;
    public WheelCollider wheelCollider
    {
        get
        {
            if (_wheelCollider == null)
            {
                _wheelCollider = GetComponent<WheelCollider>();
            }
            return _wheelCollider;
        }
    }
    public WheelHit wheelHit;
    public bool isGrounded = false;

    private CarController carController;
    private Rigidbody rb;

    private List<WheelControls> allWheelColliders = new List<WheelControls>();
    public Transform wheelModel;

    public bool isPowered = false;
    [Range(-1f, 2f)] public float powerMultiplier = 1f;
    public bool isSteerable = false;
    [Range(-1f, 1f)] public float steeringMultiplier = 1f;
    public bool isBrake = false;
    [Range(0f, 1f)] public float brakeMultiplier = 1f;
    public bool isHandbrake = false;
    [Range(0f, 1f)] public float handbrakeMultiplier = 1f;

    internal float wheelRPMToSpeedConversion = 0f;

    private float wheelSlipAmountForwards = 0f;
    private float wheelSlipAmountSideways = 0f;
    private float wheelSlipTotal = 0f;

    private WheelFrictionCurve forwardFrictionCurve;
    private WheelFrictionCurve sidewaysFrictionCurve;

    private AudioSource audioSource;
    private AudioClip audioClip;
    private float audioVolume = 1f;

    public float bumpForce;
    public float oldForce;
    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponentInParent<CarController>();
        rb = GetComponent<Rigidbody>();
        allWheelColliders = carController.GetComponentsInChildren<WheelControls>().ToList();
        wheelCollider.mass = rb.mass / 15f;

        GameObject newPivot = new GameObject("Pivot_" + wheelModel.transform.name);
        newPivot.transform.position = GetCenter(wheelModel.transform);
        newPivot.transform.rotation = transform.rotation;
        newPivot.transform.SetParent(wheelModel.transform.parent, true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Vector3 GetCenter(Transform obj)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();

        Bounds bounds = new Bounds();
        bool initBounds = false;

        foreach (Renderer r in renderers)
        {

            if (!((r is TrailRenderer) || (r is ParticleSystemRenderer)))
            {

                if (!initBounds)
                {

                    initBounds = true;
                    bounds = r.bounds;

                }
                else
                {

                    bounds.Encapsulate(r.bounds);

                }

            }

        }

        Vector3 center = bounds.center;
        return center;
    }
}
