using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour {
    public Animator animatorLD;
    public Animator animatorRD;

    public GameObject elevator;

    public float speed = 2f;
    public float timeToOpen = 6f;

    public Vector3 floor0Position;
    public Vector3 floor1Position;
    public Vector3 floor2Position;
    private Vector3 currentElePosition;
    private Vector3 targetElePosition;

    private bool isDoorOpened = true;

	void Start ()
    {
        currentElePosition = floor1Position;
        targetElePosition = currentElePosition;
        elevator.transform.position = floor1Position;
	}

	void Update ()
    {
        currentElePosition = elevator.transform.position;
        if(currentElePosition != targetElePosition)
        {
            elevator.transform.position = Vector3.MoveTowards(currentElePosition, targetElePosition, speed * Time.deltaTime);
        }
	}

    void DoorOpen()
    {
        if (isDoorOpened == false)
        {
            animatorLD.SetBool("isopen", true);
            animatorLD.Play("LeftDoorOpen");
            animatorRD.SetBool("isopen", true);
            animatorRD.Play("RightDoorOpen");
            isDoorOpened = true;
        }
    }

    private void OnMouseDown()
    {
        if (currentElePosition == floor0Position || currentElePosition == floor2Position)
        {
            targetElePosition = floor1Position;
        }
        else
        {
            System.Random random = new System.Random();
            int rint = random.Next(0, 10);

            if (rint<5)
            {
                targetElePosition = floor0Position;
            }
            else
            {
                targetElePosition = floor2Position;
            }
        }
        if (isDoorOpened == true)
        {
            animatorLD.SetBool("isopen", false);
            animatorLD.Play("LeftDoorClose");
            animatorRD.SetBool("isopen", false);
            animatorRD.Play("RightDoorClose");
            isDoorOpened = false;
            Invoke("DoorOpen", timeToOpen);
        }
        else
        {
            isDoorOpened = true;
        }
    }
}
