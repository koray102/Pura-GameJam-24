using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerScriptKaradelik : MonoBehaviour
{

    public EnemyWarning uyarýKodu;
    public GameObject Ucanvas;
    public Text mesaj;
    public float etkinlikSuresi;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            Ucanvas.SetActive(true);
            mesaj.text = "KARADELÝK";
            //uyarýKodu = other.GetComponent<EnemyWarning>();
            uyarýKodu.enemyTransform = gameObject.transform;
            
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("Durdur", etkinlikSuresi);
        }
    }

    void Durdur()
    {
        uyarýKodu.enemyTransform = null;
        transform.GetChild(0).gameObject.SetActive(false);
        Ucanvas.SetActive(false);
    }
}
