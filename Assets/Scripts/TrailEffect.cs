using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject trail;
    [SerializeField]
    private float timeBtwSpawns;
    [SerializeField]
    private float startTimeBtwSpawns;
    [SerializeField]
    private bool destroy = false;
    private bool emit = false;
    [SerializeField]
    private float destroyTime;

    // Update is called once per frame
    void Update()
    {
        if(timeBtwSpawns <= 0 && emit)
        {
            GameObject instance = (GameObject)Instantiate(trail, new Vector3(transform.position.x, transform.position.y + 0.06f, transform.position.z), transform.rotation);
            if(destroy)
                Destroy(instance, destroyTime);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else if(emit)
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }

    public void setEmitter(bool set) { emit = set; }
}
