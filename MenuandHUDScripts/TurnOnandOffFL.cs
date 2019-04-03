using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnandOffFL : MonoBehaviour
{
    public GameObject flashLightIcon;
    public GameObject flashLight;
    private bool aBool = true;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && aBool == true)
        {
            flashLightIcon.gameObject.SetActive(false);
            flashLight.gameObject.SetActive(false);
            aBool = false;
        }
        else if (Input.GetKeyDown(KeyCode.F) && aBool == false)
        {
            flashLightIcon.gameObject.SetActive(true);
            flashLight.gameObject.SetActive(true);
            aBool = true;
        }
    }
}
