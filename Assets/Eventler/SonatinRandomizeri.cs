using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonatinRandomizeri : MonoBehaviour
{
    public GameObject Meteor;
    public GameObject Karadelik;
    public GameObject SuperNova;



    public bool guvenliBolge = true;
    private bool eventVar;
    // Start is called before the first frame update
    void Start()
    {
        
        eventVar = false;

        Debug.Log("Mesaj");

        Randomizer();
    }

    // Update is called once per frame
    private void Randomizer()
    {
        if(guvenliBolge)
        {   

            if(GameObject.FindGameObjectsWithTag("Event") != null)
            {
                Invoke("DestroyerEvent", 3f);
            }

            Invoke("Randomizer", 30f);

            
        }
        else
        {
            if(eventVar) 
            {
                Invoke("Randomizer", 30f);
            }
            else
            {   
                int index = UnityEngine.Random.Range(0,3);
                float x1;
                float y1;
                float z1;

                eventVar = true;

                switch(index)
                {
                    case 0:
                        //4000-5000
                        x1 = UnityEngine.Random.Range(-2500f, 2500f);
                        z1 = (float)Mathf.Sqrt(2500*2500-x1*x1);
                        if(UnityEngine.Random.Range(0,2) == 0)
                        {
                            z1 *= -1;
                        }
                        y1 = 0f;


                        Vector3 pozisyon1 = new Vector3(gameObject.transform.position.x+ (float)x1 ,gameObject.transform.position.y+ (float)y1, gameObject.transform.position.z+(float)z1);
                        
                        

                        Instantiate(Meteor, pozisyon1, Quaternion.identity);

                        Invoke("NormaleDondurucu",50f);

                        break;

                    case 1:
                        //1000
                        x1 = UnityEngine.Random.Range(-1600, 1600);
                        z1 = (float)Mathf.Sqrt(1600 * 1600 - x1 * x1);
                        if (UnityEngine.Random.Range(0, 2) == 0)
                        {
                            z1 *= -1;
                        }
                        y1 = 0f;

                        Vector3 pozisyon2 = new Vector3(gameObject.transform.position.x + (float)x1, gameObject.transform.position.y + (float)y1, gameObject.transform.position.z + (float)z1);
                        Instantiate(Karadelik, pozisyon2, Quaternion.identity);

                        Invoke("NormaleDondurucu", 60f);

                        break;

                    case 2:
                        //1000
                        x1 = UnityEngine.Random.Range(-1500, 1500);
                        z1 = (float)Mathf.Sqrt(1500 * 1500 - x1 * x1);
                        if (UnityEngine.Random.Range(0, 2) == 0)
                        {
                            z1 *= -1;
                        }
                        y1 = 0f;

                        Vector3 pozisyon3 = new Vector3(gameObject.transform.position.x + (float)x1, gameObject.transform.position.y + (float)y1, gameObject.transform.position.z + (float)z1);
                        Instantiate(SuperNova, pozisyon3, Quaternion.identity);

                        Invoke("NormaleDondurucu", 55f);

                        break;


                }

                Invoke("Randomizer", 30f);
            }
        }
    }

    private void NormaleDondurucu()
    {
        eventVar = false;
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("GuvenliBolge")){
            guvenliBolge = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("GuvenliBolge")){
            guvenliBolge = false;
        }
    }

    void DestroyerEvent(){
        GameObject obje = GameObject.FindGameObjectWithTag("Event");
        Destroy(obje);
    }
}
