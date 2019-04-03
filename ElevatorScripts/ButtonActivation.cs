using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivation : MonoBehaviour {
    public GameObject button;

    private void Start()
    {
        button.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        button.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        button.SetActive(false);
    }
}
