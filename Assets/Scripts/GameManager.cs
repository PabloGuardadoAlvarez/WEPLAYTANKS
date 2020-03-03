using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bullet;
    public GameObject[] tanks;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tanks.Length; i++) {
            for (int j = 0; j < tanks[i].GetComponent<Turret>().bullets.Length; j++)
            {
                Instantiate(bullet);
                bullet.SetActive(false);
                tanks[j].GetComponent<Turret>().bullets[i] = bullet;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
