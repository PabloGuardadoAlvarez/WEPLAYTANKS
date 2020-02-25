using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotor : MonoBehaviour
{
    public float acceleration, maxSpeed, rotationSpeed = 1;
    [SerializeField]
    private float speed = 0;
    protected Transform transform;
    private float activationThreshold = 2f;
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

    private Quaternion TransformToQuaternion(Vector3 direction)
    {
        Quaternion rotation = new Quaternion(0,0,0,1);
        float z = direction.z;
        float x = direction.x;
        switch (z)
        {
            case 1:
                rotation.y = 0;
                break;
            case -1:
                rotation.y = 180;
                break;
        }
        switch (x)
        {
            case 1:
                rotation.y = 90;
                break;
            case -1:
                rotation.y = -90;
                break;
        }

        return rotation;
    }

    private void ApplyLocomotion(Vector3 direction)
    {
        Vector3 targetDir = direction;
        float rotationAngle = Vector3.Angle(targetDir, Vector3.forward);
        Debug.Log(transform.rotation.eulerAngles + " : " + rotationAngle + " : " + direction);
        if (direction.magnitude > 0)
        {
            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, Quaternion.Euler(0, rotationAngle, 0), Time.deltaTime * rotationSpeed);

            if (transform.rotation.eulerAngles.y >= rotationAngle - activationThreshold 
                && transform.rotation.eulerAngles.y <= rotationAngle + activationThreshold)
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
