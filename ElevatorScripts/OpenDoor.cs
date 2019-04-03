using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {
    public Animator animatorLD; //animator dla lewych drzwi
    public Animator animatorRD; //animator dla prawych drzwi
    public GameObject openPanel; //GameObject do pokazania panelu z tekstem aby nacisnac "myszke0" do aktywacji drzwi

    private bool isTriggerActive = false;
    private bool isDoorOpened = false;

	void Start ()
    {
        openPanel.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        //kiedy gracz aktywuje trigger ukazujemy text
        isTriggerActive = true;
        openPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        //kiedy gracz wychodzi z triggeru chowamy panel z tekstem
        isTriggerActive = false;
        openPanel.SetActive(false);
    }

    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Mouse0) && isTriggerActive==true)       
        {          
            if (isDoorOpened==false)
            {
                //otwarcie lewych drzwi
                animatorLD.SetBool("isopen", true);
                animatorLD.Play("LeftDoorOpen");
                //otwarcie prawych drzwi
                animatorRD.SetBool("isopen", true);
                animatorRD.Play("RightDoorOpen");
                isDoorOpened = true;
            }
            else
            {
                //zamkniecie prawych i lewych drzwi
                animatorLD.SetBool("isopen", false);
                animatorLD.Play("LeftDoorClose");
                animatorRD.SetBool("isopen", false);
                animatorRD.Play("RightDoorClose");
                isDoorOpened = false;
            }
        }
	}
}
