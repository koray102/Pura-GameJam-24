using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerScriptKaradelik : MonoBehaviour
{

    public EnemyWarning uyar�Kodu;
    public GameObject Ucanvas;
    public Text mesaj;
    public float etkinlikSuresi;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            Ucanvas.SetActive(true);
            mesaj.text = "KARADEL�K";
            //uyar�Kodu = other.GetComponent<EnemyWarning>();
            uyar�Kodu.enemyTransform = gameObject.transform;
            
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("Durdur", etkinlikSuresi);
        }
    }

    void Durdur()
    {
        uyar�Kodu.enemyTransform = null;
        transform.GetChild(0).gameObject.SetActive(false);
        Ucanvas.SetActive(false);
    }
}
