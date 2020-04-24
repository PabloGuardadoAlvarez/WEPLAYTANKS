using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject bullet;
    public Locomotor[] tanks;
    public GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        tanks = GameObject.FindObjectsOfType<Locomotor>();
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < tanks.Length; i++) {
            if(tanks[i].gameObject.tag != "Player")
            {
                tanks[i].gameObject.GetComponent<CanPathFind>().setPlayers(players);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
