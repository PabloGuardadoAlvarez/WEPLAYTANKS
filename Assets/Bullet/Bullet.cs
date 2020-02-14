using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject trace;
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
    }
}
