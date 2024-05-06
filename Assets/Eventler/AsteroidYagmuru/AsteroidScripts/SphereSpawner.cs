using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject[] spherePrefabs; // Oluþturulacak küre objesi
    public float spawnInterval = 1f; // Oluþturma aralýðý (saniye)
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime; // Zamaný güncelle

        if (timer >= spawnInterval)
        {
            SpawnSphere(); // Küre oluþtur
            timer = 0f; // Zamaný sýfýrla
        }
    }
    
    void SpawnSphere()
    {
        float katsayi = gameObject.transform.localScale.x;

        float yuzeyKonumKatsayisi = katsayi * 0.5f;
        // Rastgele bir konum belirle
        float randomX = Random.Range(-yuzeyKonumKatsayisi, yuzeyKonumKatsayisi);
        float randomY = Random.Range(-yuzeyKonumKatsayisi, yuzeyKonumKatsayisi);
        float randomZ = Random.Range(-yuzeyKonumKatsayisi, yuzeyKonumKatsayisi);

        Vector3 spawnPosition = new Vector3(gameObject.transform.position.x+randomX, gameObject.transform.position.y+randomY, gameObject.transform.position.z + randomZ); // Yükseklik 0.5f olarak ayarlandý

        // Küreyi oluþtur
        int index = Random.Range(0, spherePrefabs.Length);
        GameObject sphere = Instantiate(spherePrefabs[index], spawnPosition,transform.rotation);
    }
}