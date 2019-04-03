using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTorch : MonoBehaviour
{
    public GameObject torch;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            torch.gameObject.SetActive(true);
        }
    }
}
