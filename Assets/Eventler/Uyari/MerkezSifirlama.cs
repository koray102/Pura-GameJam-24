using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MerkezSifirlama : MonoBehaviour
{
    private GameObject Ucanvas;

    private void Start()
    {
        Ucanvas = GameObject.FindGameObjectWithTag("UyariCanvasi");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Ucanvas.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
