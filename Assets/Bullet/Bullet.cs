using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject trace;
    private Rigidbody rb;
    private int bounceCounter = 0;
    public int maxBounces;
    public GameObject explosionEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("instanciarTrace");
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
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Wall")
        {
            bounceCounter++;
            if (bounceCounter > maxBounces) {

                var explosioneff = Instantiate(explosionEffect);
                explosioneff.transform.position = GetComponent<Transform>().position;
                Destroy(gameObject);
                Destroy(explosioneff, 1.5f);
            }
        }


    }
}
