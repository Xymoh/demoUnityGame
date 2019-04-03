using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnHUDnMenu : MonoBehaviour
{
    
    public GameObject HUD;
    private bool aBool = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && aBool == true)
        {
            HUD.gameObject.SetActive(false);
            aBool = false;
        }
        else if (Input.GetKeyDown(KeyCode.H) && aBool == false)
        {
            HUD.gameObject.SetActive(true);
            aBool = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
