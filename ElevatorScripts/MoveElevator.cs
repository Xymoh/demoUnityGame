using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveElevator : MonoBehaviour
{
    public Animator elevatorMoveUp;
    public Animator elevatorMoveUpF0;
    public Animator elevatorMoveDown;
    public Animator elevatorMoveDownF2;

    public GameObject openPanel;

    static public bool isElevatorMid = true;
    static public bool isElevatorUp = false;
    static public bool isElevatorDown = false;
    static public bool isTriggerActive = false;

    void Start()
    {
        openPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggerActive = true;
        openPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggerActive = false;
        openPanel.SetActive(false);
    }

    void ElevatorUp()
    {
        if (isElevatorMid == true && Input.GetKeyDown(KeyCode.E))
        {
            elevatorMoveUp.Play("ElevatorUp");
            isElevatorUp = true;
            isElevatorMid = false;
        }
    }

    void ElevatorDownF2()
    {
        if (isElevatorUp == true && Input.GetKeyDown(KeyCode.E))
        {
            elevatorMoveDownF2.Play("ElevatorDownF2");
            isElevatorUp = false;
            isElevatorMid = true;
        }
    }

    //void Update()
    //{
    //    if (isTriggerActive == true)
    //    {
    //        ElevatorUp();
    //    }
    //    if (isTriggerActive == true)
    //    {
    //        ElevatorDownF2();
    //    }
    //}
}
