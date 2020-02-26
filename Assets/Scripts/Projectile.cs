using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public bool hasTrace = true;
    public GameObject trace;
    public int damage = 1;
    public int maxBounce = 1;
    private GameObject shooter;

    private int bounceCount = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if(hasTrace)
            StartCoroutine("instanciarTrace");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator instanciarTrace()
    {
        while (true)
        {
            var actualtrace = Instantiate(trace);
            actualtrace.transform.position = gameObject.transform.position;
            yield return new WaitForSeconds(.05f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Perishable thisPerishable = this.GetComponent<Perishable>();
        Perishable otherPerishable = collision.gameObject.GetComponent<Perishable>();
        if (otherPerishable)
        {
            if(otherPerishable.name == "Player")
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
                bounceCount++;
            else
            {
                thisPerishable.killEntity();
                if(shooter)
                    shooter.GetComponent<Turret>().addBullet();
            }
        }
    }

    public GameObject getShooter() { return shooter; }
    public void setShooter(GameObject shooter) { this.shooter = shooter; }
}
