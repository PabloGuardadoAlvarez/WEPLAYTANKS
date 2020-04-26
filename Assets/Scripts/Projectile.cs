using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public bool hasTrace = true;
    public float traceScale = 0.5f;
    public GameObject trace;
    public int damage = 1;
    public int maxBounce = 1;
    private GameObject shooter;
    private bool teleported = false;
    public float minVelocity = 10f;

    private Vector3 lastFrameVelocity;

    private int bounceCount = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (hasTrace)
        {
            trace = Instantiate(trace);
            trace.transform.position = transform.position;
            trace.transform.localScale = trace.transform.localScale * traceScale;
            trace.GetComponent<Perishable>().setTarget(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Perishable thisPerishable = this.GetComponent<Perishable>();
        Perishable otherPerishable = collision.gameObject.GetComponent<Perishable>();
        if (otherPerishable && !otherPerishable.onlyBomb)
        {
            if (otherPerishable.name == "Player")
                otherPerishable.doDamage(damage, "self");
            else
                otherPerishable.doDamage(damage, "bullet");
            if(shooter)
                shooter.GetComponent<Turret>().addBullet();
            thisPerishable.killEntity();

        }
        else
        {
            if (bounceCount < maxBounce)
            {
                Portal portal = collision.gameObject.GetComponent<Portal>();
                if (portal && !teleported)
                {

                    Portal otherPortal = portal.getLinkedPortal().GetComponent<Portal>();
                    trace.GetComponent<Perishable>().setTarget(null);
                    Vector3 bulletToPortal = transform.position - portal.transform.position;
                    float rotationDiff = -Quaternion.Angle(portal.transform.rotation, otherPortal.transform.rotation);
                    transform.Rotate(otherPortal.transform.up, rotationDiff);

                    transform.position = otherPortal.getSpawnLocation();
                    changeDirection(transform.forward);
                    teleported = true;
                    Start();
                }
                else
                {
                    bounceCount++;
                    changeDirection(bounce(collision.GetContact(0).normal));
                }
            }
            else
            {
                thisPerishable.killEntity();
                if (shooter)
                {
                    shooter.GetComponent<Turret>().addBullet();
                }
            }
        }
    }

    public GameObject getShooter() { return shooter; }
    public void setShooter(GameObject shooter) { this.shooter = shooter; }

    private void changeDirection(Vector3 newDirection)
    {
        var speed = lastFrameVelocity.magnitude;

        rb.transform.forward = newDirection;
        rb.velocity = newDirection * Mathf.Max(speed, minVelocity);
    }

    private Vector3 bounce(Vector3 collisionNormal)
    {
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        return direction;
    }
}
