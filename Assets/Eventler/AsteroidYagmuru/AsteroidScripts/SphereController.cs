using UnityEngine;

public class SphereController : MonoBehaviour
{
    
    public float destroyDelay = 10f; // Yok olma gecikmesi (saniye)
    public float rotateYatkinlik = 10f;

    private Vector3 torque = new Vector3(0, 0, 0);
    private float launchForce; // Fýrlatma kuvveti

    void Start()
    {
        launchForce = Random.Range(6000, 12000);

        Rigidbody rb = GetComponent<Rigidbody>();
        
        rb.AddForce(transform.forward * launchForce*1.5f, ForceMode.Impulse); // Küreyi fýrlat


        torque.x = Random.Range(-1 - rotateYatkinlik, 1+ rotateYatkinlik);
        torque.y = Random.Range(-1 - rotateYatkinlik, 1+ rotateYatkinlik);
        torque.z = Random.Range(-1 - rotateYatkinlik, 1+ rotateYatkinlik);


        rb.AddTorque(torque, ForceMode.Impulse);// Döndür

        // Yok olma iþlemini baþlat
        Invoke("DestroySphere", destroyDelay);
    }



    void DestroySphere()
    {
        Destroy(gameObject); // Küreyi yok et
    }

}