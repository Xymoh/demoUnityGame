using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElele : MonoBehaviour {
    public GameObject up;
    public GameObject elevator;
    public float speed = 2f;

    private void OnTriggerEnter(Collider other)
    {
        elevator.transform.position = Vector3.Lerp(elevator.transform.position, up.transform.position, Time.deltaTime * speed);
    }
}
