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
    public Locomotor[] tanks;
    public GameObject[] players;
    public TextMeshProUGUI numberLivesText;
    // Start is called before the first frame update
    void Start()
    {
        tanks = GameObject.FindObjectsOfType<Locomotor>();
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < tanks.Length; i++) {
            if (tanks[i].gameObject.tag != "Player")
            {
                tanks[i].gameObject.GetComponent<CanPathFind>().setPlayers(players);
            }
            else {

                tanks[i].gameObject.GetComponent<Perishable>().setManager(this.gameObject);

            }
        }
        if (PlayerPrefs.GetInt("lives") == 0)
        {
            PlayerPrefs.SetInt("lives" ,3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("lives"));
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Restargame() {

        SceneManager.LoadScene(1); 
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
