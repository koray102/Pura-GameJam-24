using UnityEngine;

public class HedefeDondurucu : MonoBehaviour
{
    public string hedefTag = "Player"; // Hedefin etiketi

    void Update()
    {
        // Hedef nesneyi bul
        GameObject hedefNesne = GameObject.FindGameObjectWithTag(hedefTag);

        if (hedefNesne != null)
        {
            // Hedef nesneye doðru dönme
            Vector3 hedefYon = hedefNesne.transform.position - transform.position;
            Quaternion yeniRotasyon = Quaternion.LookRotation(hedefYon);
            transform.rotation = Quaternion.Slerp(transform.rotation, yeniRotasyon, Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Hedef nesne bulunamadý!");
        }
    }
}