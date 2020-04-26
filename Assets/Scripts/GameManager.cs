using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject bullet;
    public GameObject restarMenu;
    public GameObject gamerOverMenu;
    public GameObject nextLevelMenu;
    public Locomotor[] tanks;
    public GameObject[] players;
    public TextMeshProUGUI numberLivesText;
    private int numberOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        tanks = GameObject.FindObjectsOfType<Locomotor>();
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < tanks.Length; i++) {
            if (tanks[i].gameObject.tag != "Player")
            {
                Debug.Log(players.Length);
                tanks[i].gameObject.GetComponent<CanPathFind>().setPlayers(players);
                tanks[i].transform.GetChild(2).GetComponent<CanTrack>().setTarget(players[0].gameObject);
            }

            tanks[i].gameObject.GetComponent<Perishable>().setManager(this.gameObject);
        }

        if (PlayerPrefs.GetInt("lives") == 0)
        {
            PlayerPrefs.SetInt("lives" ,3);
        }

        numberOfEnemies = tanks.Length - players.Length;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Restargame() {

        SceneManager.LoadScene(1); 
    }

    public void nextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void enemyKill() {
        numberOfEnemies--;
        if (numberOfEnemies <= 0) {
            nextLevelMenu.SetActive(true);
        }
    }



    public void death() {
        PlayerPrefs.SetInt("lives", PlayerPrefs.GetInt("lives") - 1);

        if (PlayerPrefs.GetInt("lives") > 0)
        {
            numberLivesText.SetText("Vidas :X" + PlayerPrefs.GetInt("lives"));
            PlayerPrefs.SetInt("lives", PlayerPrefs.GetInt("lives"));
            restarMenu.SetActive(true);

        }
        else {

            gamerOverMenu.SetActive(true);

        }
    }
}
