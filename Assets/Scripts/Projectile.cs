using System.Collections;
using System.Collections.Generic;
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
        if (otherPerishable && otherPerishable.name !="DWall")
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
                if (portal)
                {
                    trace.GetComponent<Perishable>().setTarget(null);
                    this.transform.position = portal.getLinkedPortal().GetComponent<Portal>().getSpawnLocation();
                    Bounce(collision.GetContact(0).normal);
                    Start();

                }
                else
                {
                    bounceCount++;
                    Bounce(collision.contacts[0].normal);
                }
                Debug.Log(collision.GetContact(0).normal);
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

    private Vector3 Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        rb.transform.forward = direction;
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
        return direction;
    }
}
