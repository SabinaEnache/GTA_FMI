using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBreakForce;
    private bool isBreaking;

    private bool applyingImpulseFront = false;
    private bool applyingImpulseBack = false;
    private float impulseDuration = 0.2f;
    private float impulseTimer = 0f;
    private float impulseDurationBack = 0.12f;

    [SerializeField] private float motorForce = 800f;
    [SerializeField] private float maxBreakForce = 5000f;
    [SerializeField] private float maxSteerAngle = 55f;
    [SerializeField] private float impulseForce = 0.36f;
    [SerializeField] private float additionalBreakForce = 20f;

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    public bool isSittingInThisCar = false;
    public GameObject carCam;
    public GameObject characterCam;
    public GameObject playerObject;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 1500f;
        rb.centerOfMass = new Vector3(0, -0.9f, 0);
        AdjustWheelFriction();
    }

    void FixedUpdate()
    {
        if (isSittingInThisCar)
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
            ApplyAerodynamicDrag();
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        if (verticalInput > 0 && rearLeftWheelCollider.rpm < 20f && RigidbodyIsAlmostStopped() && !applyingImpulseFront && !isBreaking)
        {
            applyingImpulseFront = true;
            impulseTimer = 0f;
        }
        if (verticalInput < 0 && rearLeftWheelCollider.rpm < 20f && RigidbodyIsAlmostStopped() && !applyingImpulseBack && !isBreaking)
        {
            applyingImpulseBack = true;
            impulseTimer = 0f;
        }
        if (applyingImpulseBack)
        {
            rb.AddForce(-transform.forward * impulseForce * motorForce * 0.24f, ForceMode.Impulse);
            impulseTimer += Time.fixedDeltaTime;
            if (impulseTimer >= impulseDurationBack)
                applyingImpulseBack = false;
        }

        if (applyingImpulseFront)
        {
            rb.AddForce(transform.forward * impulseForce * motorForce, ForceMode.Impulse);
            impulseTimer += Time.fixedDeltaTime;
            if (impulseTimer >= impulseDuration)
                applyingImpulseFront = false;
        }

        if (isBreaking)
        {
            currentBreakForce = Mathf.Lerp(currentBreakForce, maxBreakForce, Time.fixedDeltaTime * 5f);
            ApplyBreaking();
            ApplyLinearBreaking();
        }
        else
        {
            currentBreakForce = 0f;
            ReleaseBrakes();
        }
    }

    private bool RigidbodyIsAlmostStopped()
    {
        return rb.linearVelocity.magnitude < 0.5f;
    }

    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }

    private void ReleaseBrakes()
    {
        frontLeftWheelCollider.brakeTorque = 0f;
        frontRightWheelCollider.brakeTorque = 0f;
        rearLeftWheelCollider.brakeTorque = 0f;
        rearRightWheelCollider.brakeTorque = 0f;
    }

    private void ApplyLinearBreaking()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            Vector3 velocity = rb.linearVelocity;
            rb.AddForce(-velocity.normalized * additionalBreakForce, ForceMode.Acceleration);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void HandleSteering()
    {
        float speedFactor = Mathf.Clamp01(rb.linearVelocity.magnitude / 20f);
        float dynamicSteerAngle = Mathf.Lerp(maxSteerAngle, maxSteerAngle * 0.5f, speedFactor);
        currentSteerAngle = dynamicSteerAngle * horizontalInput;

        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void ApplyAerodynamicDrag()
    {
        Vector3 dragForce = -rb.linearVelocity.normalized * rb.linearVelocity.sqrMagnitude * 0.05f;
        rb.AddForce(dragForce);
    }

    private void AdjustWheelFriction()
    {
        foreach (WheelCollider wheel in new WheelCollider[] { frontLeftWheelCollider, frontRightWheelCollider, rearLeftWheelCollider, rearRightWheelCollider })
        {
            WheelFrictionCurve forwardFriction = wheel.forwardFriction;
            forwardFriction.stiffness = 1.5f;
            wheel.forwardFriction = forwardFriction;

            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.stiffness = 2.0f;
            wheel.sidewaysFriction = sidewaysFriction;
        }
    }

    public void GetOutOfCar()
    {
        if (playerObject != null)
        {
            var meshRenderers = playerObject.GetComponentsInChildren<Renderer>();
            foreach (var renderer in meshRenderers)
            {
                renderer.enabled = true;
            }
            playerObject.transform.position = transform.position + new Vector3(0f, 0f, 5f);
            isSittingInThisCar = false;
            this.enabled = false;
            SwitchToCharacterCamera();
            playerObject = null;
        }
    }

    void SwitchToCarCamera()
    {
        carCam.SetActive(true);
        characterCam.SetActive(false);
    }

    void SwitchToCharacterCamera()
    {
        carCam.SetActive(false);
        characterCam.SetActive(true);
    }
}
