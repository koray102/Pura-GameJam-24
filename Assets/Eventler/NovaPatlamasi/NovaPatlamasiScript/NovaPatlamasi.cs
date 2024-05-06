using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaPatlamasi : MonoBehaviour
{
    public GameObject oyuncu;
    public GameObject spawnpointtr;


    private ParticleSystem patlamaOncesi1;
    private ParticleSystem patlamaOncesi2;
    private ParticleSystem patlamaAni;
    private ParticleSystem patlamaGenisleme;
    private ParticleSystem patlamaSonrasi;
    private ParticleSystem patlamaDalgasi;

    private AudioSource PatlamaSesi;

    private bool icerde = false;

    // Start is called before the first frame update
    void Start()
    {
        patlamaOncesi1 = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        patlamaOncesi2 = gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        patlamaAni = gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();
        patlamaGenisleme = gameObject.transform.GetChild(3).GetComponent<ParticleSystem>();
        patlamaSonrasi = gameObject.transform.GetChild(4).GetComponent<ParticleSystem>();
        patlamaDalgasi = gameObject.transform.GetChild(5).GetComponent<ParticleSystem>();

        PatlamaSesi = gameObject.GetComponent<AudioSource>();

        patlamaOncesi1.Play();
        Invoke("PatlamaOncesi2", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PatlamaOncesi2()
    {
        patlamaOncesi2.Play();
        Invoke("Patlama", 5);
    }

    void Patlama()
    {
        patlamaOncesi1.Stop();
        patlamaOncesi2.Stop();
        patlamaAni.Play();
        PatlamaSesi.Play();
        Invoke("SuperNovaOncesi", 0.5f);
    }

    void SuperNovaOncesi()
    {
        patlamaAni?.Stop();

        patlamaDalgasi.Play();
        patlamaGenisleme.Play();

        Invoke("SuperNova", 1);
    }

    void SuperNova()
    {   

        patlamaSonrasi.Play();
        Invoke("OlduMu", 3f);
    }

    private void OlduMu()
    {
        if (icerde)
        {
            oyuncu.gameObject.transform.position = spawnpointtr.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            icerde = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            icerde = false;
        }

    }
}
