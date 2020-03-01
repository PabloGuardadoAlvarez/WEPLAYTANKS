using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTrack : MonoBehaviour
{

    public GameObject player , mySelf;
    private bool canShot;
    // Start is called before the first frame update
    void Start()
    {
        canShot = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.tag);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);
            
            if (hit.collider.gameObject.tag == "player")
            {
                mySelf.GetComponent<CanPathFind>().canContinue = false;
                if (canShot) {
                    StartCoroutine(shot());
                }
            }
            else {
                mySelf.GetComponent<CanPathFind>().canContinue = true;
                StopCoroutine(shot());
            }
            
        }
    }

    IEnumerator shot()
    {
        gameObject.GetComponent<Turret>().doShot();
        canShot = false;
        yield return new WaitForSeconds(2f);
        canShot = true;
    }
}
