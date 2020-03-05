using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public bool hasTrace = true;
    public float traceScale = 0.5f;
    public GameObject trace;
    public int damage = 1;
    public int maxBounce = 1;
    private GameObject shooter;
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
        if (otherPerishable)
        {
            if (otherPerishable.name == "Player")
                otherPerishable.doDamage(damage, "self");
            else if(otherPerishable.name !="DWall")
                otherPerishable.doDamage(damage, "bullet");
            if(shooter)
                shooter.GetComponent<Turret>().addBullet();
            transform.DetachChildren();
            thisPerishable.killEntity();

        }
        else
        {
            if (bounceCount < maxBounce)
            {
                bounceCount++;
                Bounce(collision.contacts[0].normal);
                Debug.Log(collision.contacts[0].normal);
            }
            else
            {
                transform.DetachChildren();
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

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }
}
