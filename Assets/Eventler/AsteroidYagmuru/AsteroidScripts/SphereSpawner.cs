using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject[] spherePrefabs; // Olu�turulacak k�re objesi
    public float spawnInterval = 1f; // Olu�turma aral��� (saniye)
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime; // Zaman� g�ncelle

        if (timer >= spawnInterval)
        {
            SpawnSphere(); // K�re olu�tur
            timer = 0f; // Zaman� s�f�rla
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

        Vector3 spawnPosition = new Vector3(gameObject.transform.position.x+randomX, gameObject.transform.position.y+randomY, gameObject.transform.position.z + randomZ); // Y�kseklik 0.5f olarak ayarland�

        // K�reyi olu�tur
        int index = Random.Range(0, spherePrefabs.Length);
        GameObject sphere = Instantiate(spherePrefabs[index], spawnPosition,transform.rotation);
    }
}