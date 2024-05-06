
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float attractionRange = 20f;
    public float attractionForce = 10f;

    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attractionRange);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                float forceMagnitude = 1f - (distance / attractionRange); // Mesafe ile ters orantýlý bir çekme kuvveti hesaplayýn
                Vector3 forceDirection = (transform.position - collider.transform.position).normalized;
                Vector3 force = forceDirection * attractionForce * forceMagnitude;
                rb.AddForce(force);
            }
        }
    }
}