using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleButton : MonoBehaviour {
    public GameObject elevator;

    public float speed = 1f;

    public Vector3 floor0Position;
    public Vector3 floor1Position;
    public Vector3 floor2Position;
    private Vector3 currentElePosition;
    private Vector3 targetElePosition;

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

    private void OnMouseDown()
    {
        if (currentElePosition == floor0Position || currentElePosition==floor2Position)
        {
            targetElePosition = floor1Position;
        }
        else
        {
            System.Random random = new System.Random();
            int rint = random.Next(0, 100);

            if (rint<50)
            {
                targetElePosition = floor0Position;
            }
            else
            {
                targetElePosition = floor2Position;
            }
        }
    }
}
