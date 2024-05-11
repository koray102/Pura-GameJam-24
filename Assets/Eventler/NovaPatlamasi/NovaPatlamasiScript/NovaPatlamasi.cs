using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NovaPatlamasi : MonoBehaviour
{
    private GameObject oyuncu;
    private Vector3 spawnpointtr = new Vector3(-295.9141f,-22.35335f,-4.568902f);


    private ParticleSystem patlamaOncesi1;
    private ParticleSystem patlamaOncesi2;
    private ParticleSystem patlamaAni;
    private ParticleSystem patlamaGenisleme;
    private ParticleSystem patlamaSonrasi;
    private ParticleSystem patlamaDalgasi;

    private AudioSource PatlamaSesi;

    private SphereCollider colliderTop;


    // Start is called before the first frame update
    void Start()
    {   
        oyuncu = GameObject.FindGameObjectWithTag("Player");

        patlamaOncesi1 = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        patlamaOncesi2 = gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        patlamaAni = gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();
        patlamaGenisleme = gameObject.transform.GetChild(3).GetComponent<ParticleSystem>();
        patlamaSonrasi = gameObject.transform.GetChild(4).GetComponent<ParticleSystem>();
        patlamaDalgasi = gameObject.transform.GetChild(5).GetComponent<ParticleSystem>();

        PatlamaSesi = gameObject.GetComponent<AudioSource>();

        colliderTop = GetComponent<SphereCollider>();
        colliderTop.radius = 0;

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
        Invoke("Buyu", 0f);
        
    }

    private void Buyu()
    { 
        colliderTop.radius += 0.6f;
        Invoke("Buyu", 0.01f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("Olum", 1f);
        }
    }

    private void Olum()
    {
        oyuncu.transform.position = spawnpointtr;
    }





}
