using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YokediciDelik : MonoBehaviour
{

    public GameObject spawnPoint1;
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
            collision.gameObject.transform.position = spawnPoint1.transform.position;
        }
    }
}
