using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class TriggerScriptAsteroid : MonoBehaviour
{
 
    private GameObject Ucanvas;
    private GameObject image1;

    private GameObject eskiYazi;


    public float etkinlikSuresi = 20f;
    public float rotationSpeed = 5.0f;

    private void Start()
    {
        Ucanvas = GameObject.FindGameObjectWithTag("UyariCanvasi");
        if (Ucanvas == null)
        {
            Debug.LogError("UyariCanvasi bulunamad�!");
        }
        else
        {
            Debug.Log(Ucanvas.transform.childCount);
            image1 = Ucanvas.transform.GetChild(0).gameObject;
            
            Debug.Log(image1.transform.childCount);
            image1.SetActive(true);
            eskiYazi = Ucanvas.transform.GetChild(1).gameObject;

            eskiYazi.SetActive(true);
            int sayi1 = UnityEngine.Random.Range(1,12);
            int sayi2 = UnityEngine.Random.Range(1,60);

            Ucanvas.transform.GetChild(1).gameObject.gameObject.GetComponent<Text>().text = "Saat " + sayi1.ToString() +":" + sayi2.ToString() + " Yönünde Asteroid Yağmuru Yaklaşıyor Dikkatli Ol";

        }
        
    }


    private void OnTriggerEnter(Collider other)
    {   
        
        if (other.CompareTag("Player"))
        {

           
            
            //uyar�Kodu = other.GetComponent<EnemyWarning>();

            transform.GetChild(0).gameObject.SetActive(true); 

            Invoke("Durdur", etkinlikSuresi);
            
        }
    }

    void Durdur()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        image1.SetActive(false);
        Ucanvas.transform.GetChild(1).gameObject.SetActive(false);
        Destroy(gameObject);
    }





}
