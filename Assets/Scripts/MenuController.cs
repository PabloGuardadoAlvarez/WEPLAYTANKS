using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject mainMenu, optionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void changeToOptions() {

        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }


}
