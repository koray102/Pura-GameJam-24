using UnityEngine;

public class SphereController : MonoBehaviour
{
    
    public float destroyDelay = 10f; // Yok olma gecikmesi (saniye)
    public float rotateYatkinlik = 10f;

    private Vector3 torque = new Vector3(0, 0, 0);
    private float launchForce; // F�rlatma kuvveti

    void Start()
    {
        launchForce = Random.Range(400, 500);

        Rigidbody rb = GetComponent<Rigidbody>();
        
        rb.AddForce(transform.forward * launchForce*1.5f, ForceMode.Impulse); // K�reyi f�rlat


        torque.x = Random.Range(-1 - rotateYatkinlik, 1+ rotateYatkinlik);
        torque.y = Random.Range(-1 - rotateYatkinlik, 1+ rotateYatkinlik);
        torque.z = Random.Range(-1 - rotateYatkinlik, 1+ rotateYatkinlik);


        rb.AddTorque(torque, ForceMode.Impulse);// D�nd�r

        // Yok olma i�lemini ba�lat
        Invoke("DestroySphere", destroyDelay);
    }



    void DestroySphere()
    {
        Destroy(gameObject); // K�reyi yok et
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("sdfsdf");
            collision.gameObject.transform.position = new Vector3(-295.9141f,-22.35335f,-4.568902f);
        }


    }
}