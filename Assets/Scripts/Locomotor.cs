using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotor : MonoBehaviour
{
    public float acceleration, maxSpeed, rotationSpeed = 1;
    public float rotationThreshold = 1f;
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private bool canRotate = true;
    protected Rigidbody _rb;
    private InputHandler _ih;
    private TrailEffect trail;
    public float distToGround = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _ih = this.GetComponent<InputHandler>();
        trail = GetComponent<TrailEffect>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveTo(Vector3 direction)
    {
        var directionNormalized = direction.normalized;
        if (_ih && _ih.getIsPC())
            ApplyLocomotionPC(directionNormalized);
        else
        {
            ApplyLocomotion(directionNormalized);
        }
    }

    public void MoveTo(float x, float z)
    {
        var direction = new Vector3(x, 0, z);
        var directionNormalized = direction.normalized;
        if (_ih.getIsPC() && _ih)
            ApplyLocomotionPC(directionNormalized);
        else
            ApplyLocomotion(directionNormalized);
    }

    private void ApplyLocomotion(Vector3 direction)
    {
        float rotationAngle = Vector3.Angle(direction, Vector3.forward);
        Vector3 cross = Vector3.Cross(direction, Vector3.forward);

        if (cross.y > 0) rotationAngle = 360 - rotationAngle;

        Vector3 inverseDirection = new Vector3(direction.x * -1, 0, direction.z * -1);

        float angleChecker = Vector3.Angle(direction, transform.forward);
        if ((angleChecker >= 180 - rotationThreshold && angleChecker <= 180 + rotationThreshold) ||
            (angleChecker >= 0 - rotationThreshold && angleChecker <= 0 + rotationThreshold))
            canRotate = false;
        else
            canRotate = true;

        if (canRotate)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rotationAngle, 0), rotationSpeed);
        else
        {
            if (direction.magnitude > 0 && speed < maxSpeed)
            {
                speed += acceleration;
                trail.setEmitter(true);
            }
            else if (direction.magnitude < 1 && speed > 0)
            {
                speed -= acceleration;
                trail.setEmitter(false);
            }
            else if (speed < 0)
            {
                speed = 0;
            }
            _rb.velocity = direction * speed;
        }
    }
    private void ApplyLocomotionPC(Vector3 direction)
    {
            transform.Rotate(new Vector3(0, direction.x, 0) * rotationSpeed);
            if (direction.z > 0 || direction.z < 0)
            {
                _rb.velocity = transform.forward * direction.z * acceleration;
                trail.setEmitter(true);
            }
            else
            {
                trail.setEmitter(false);
            }
    }
    public bool isGrounded()
    {
        return Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Vector3.down, distToGround);
    }
}
