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
            Vector3 _wallNormal = collision.contacts[0].normal;
            var m_dir = Vector3.Reflect(rb.transform.forward, _wallNormal).normalized;
            transform.eulerAngles = m_dir;
            gameObject.transform.position = bluePortal.transform.position;
            rb.AddForce(gameObject.transform.forward * 300f);
            canTp = false;

        }
        else if (collision.gameObject.tag == "portalB" && canTp)
        {
            Vector3 _wallNormal = collision.contacts[0].normal;
            var m_dir = Vector3.Reflect(rb.transform.forward, _wallNormal).normalized;
            transform.eulerAngles = m_dir;
            gameObject.transform.position = orangePortal.transform.position;
            rb.AddForce(gameObject.transform.forward * 300f);
            canTp = false;

        }

    }
}
