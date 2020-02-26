using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotor : MonoBehaviour
{
    public float acceleration, maxSpeed, rotationSpeed = 1;
    [SerializeField]
    private float speed = 0;
    protected Transform transform;
    [SerializeField]
    private bool canRotate = true;
    protected Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        _rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveTo(Vector3 direction)
    {
        var directionNormalized = direction.normalized;
        ApplyLocomotion(directionNormalized);
    }

    public void MoveTo(float x, float z)
    {
        var directionNormalized = new Vector3(x, 0, z).normalized;
        ApplyLocomotion(directionNormalized);
    }

    private void ApplyLocomotion(Vector3 direction)
    {
        float rotationAngle = Vector3.Angle(direction, Vector3.forward);
        Vector3 cross = Vector3.Cross(direction, Vector3.forward);

        if (cross.y > 0) rotationAngle = 360 - rotationAngle;
        Vector3 inverseDirection = new Vector3(direction.x * -1, 0, direction.z * -1);
        if (transform.forward == inverseDirection || transform.forward == direction)
            canRotate = false;
        else
            canRotate = true;

        //Debug.Log(transform.rotation.eulerAngles + " : " + direction + " : " + rotationAngle);
        //Debug.Log(transform.forward + " : " + direction + " : " + rotationAngle);
        if (direction.magnitude > 0)
        {
            if(canRotate)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rotationAngle, 0), rotationSpeed);
            else
            {
                if (direction.magnitude > 0 && speed < maxSpeed)
                    speed += acceleration;
                else if (direction.magnitude == 0 && speed > 0)
                    speed -= acceleration;
                else if (speed < 0)
                    speed = 0;
                _rb.velocity = direction * speed;
            }
        }

    }
}
