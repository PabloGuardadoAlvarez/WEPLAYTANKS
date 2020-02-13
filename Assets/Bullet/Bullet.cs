using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject trace,bluePortal,orangePortal;
    private Rigidbody rb;
    private bool canTp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        canTp = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
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
        else if (collision.gameObject.tag == "portalO" && canTp)
        {
            gameObject.transform.position = bluePortal.transform.position;
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.z,0,0);
            canTp = false;

        }
        else if (collision.gameObject.tag == "portalB" && canTp)
        {
            gameObject.transform.position = orangePortal.transform.position;
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.z, 0, 0);
            canTp = false;

        }

    }
}
