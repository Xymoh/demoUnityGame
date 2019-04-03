using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLightTrigger : MonoBehaviour {
    public GameObject light;

    private void Start()
    {
        light.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        light.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        light.SetActive(false);
    }
}
