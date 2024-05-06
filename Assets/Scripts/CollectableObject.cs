using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour, IInteractable
{
    private bool isCollected;
    private float scaleModifier = 1;
    private InventorySystem inventorySys;
    [SerializeField] private float timeToLerp;
    [SerializeField] private string itemName;
    [SerializeField] private ParticleSystem collectVFX;
    [SerializeField] private AudioSource collectSFX;

    public void OnInteract()
    {
        if(!isCollected)
        {
            inventorySys = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventorySystem>();
            inventorySys.AddItem(itemName);
            inventorySys.DisplayInventory();

            isCollected = true;
            collectVFX = Instantiate(collectVFX, transform.position, Quaternion.identity);
            collectVFX.Play();
            collectSFX = GetComponent<AudioSource>();
            collectSFX.PlayOneShot(collectSFX.clip);
            StartCoroutine(LerpFunction(0, timeToLerp));
        }
    }
    

    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = scaleModifier;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
            transform.localScale = startScale * scaleModifier;
            time += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = startScale * endValue;
        scaleModifier = endValue;
    }
}
