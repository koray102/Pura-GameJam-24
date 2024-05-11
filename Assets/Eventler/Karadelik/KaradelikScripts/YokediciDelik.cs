using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YokediciDelik : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
        else
        {
            collision.gameObject.transform.position = new Vector3(-295.9141f,-22.35335f,-4.568902f);
        }
    }
}
