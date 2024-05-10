using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestKoray : MonoBehaviour, IInteractable
{
    private InventorySystem inventorySystem;
    private int questCounter = 0;
    private bool questActive = false;
    private string questDef = "Uzay istasyonuna ulaşman gerek.";
    [SerializeField] private TMP_Text infoText;

    [SerializeField] private Animator animator;
    private bool questCompleted;

    // Start is called before the first frame update
    void Start()
    {
        inventorySystem = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventorySystem>();

    }

    public void OnInteract()
    {

        if (questCounter == 0)
        {
            questCompleted = false;
            animator.SetBool("questComp", questCompleted);
            questDef = "3 adet Zümrüt ve 4 adet Yakut topla.";
            if (!questActive)
            {
                questActive = true;
                animator.SetBool("questActive", questActive);
                StartCoroutine(WriteToCanvas(questDef));
            }

            if (inventorySystem.HasItem("Emerald") && inventorySystem.GetItemCount("Emerald") >= 3 && inventorySystem.HasItem("Ruby") && inventorySystem.GetItemCount("Ruby") >= 4)
            {
                questActive = false;
                animator.SetBool("questActive", questActive);
                questCompleted = true;
                animator.SetBool("questComp", questCompleted);
                questCounter++;

            }
        }
        else if (questCounter == 1)
        {
            questCompleted = false;
            animator.SetBool("questComp", questCompleted);
            questDef = "3 adet safir ve 5 Adet Buz topla.";
            if (!questActive)
            {
                questActive = true;
                animator.SetBool("questActive", questActive);
                questCompleted = false;
                animator.SetBool("questComp", questCompleted);
                StartCoroutine(WriteToCanvas(questDef));
            }

            if (inventorySystem.HasItem("Sapphire") && inventorySystem.GetItemCount("Sapphire") >= 3 && inventorySystem.HasItem("Ice") && inventorySystem.GetItemCount("Ice") >= 5)
            {
                questCounter++;
                questActive = false;
                animator.SetBool("questActive", questActive);
                questCompleted = false;
                animator.SetBool("questComp", questCompleted);
            }
        }
        else if (questCounter == 2)
        {
            questCompleted = false;
            animator.SetBool("questComp", questCompleted);
            questDef = "7 adet Elmas ve 8 adet Karbon topla.";
            if (!questActive)
            {
                questActive = true;
                animator.SetBool("questActive", questActive);
                StartCoroutine(WriteToCanvas(questDef));
            }

            if (inventorySystem.HasItem("Diamond") && inventorySystem.GetItemCount("Diamond") >= 7 && inventorySystem.HasItem("Carbon") && inventorySystem.GetItemCount("Carbon") >= 5)
            {

                questCounter++;
                questActive = false;
                animator.SetBool("questActive", questActive);
                questCompleted = false;
                animator.SetBool("questComp", questCompleted);
            }
        }
        else if (questCounter == 3)
        {
            questCompleted = false;
            animator.SetBool("questComp", questCompleted);
            questDef = "Uzayda serbestçe dolaşan 2077 model Opel Astra sag ön farini bul";
            if (!questActive)
            {
                questActive = true;
                animator.SetBool("questActive", questActive);
                StartCoroutine(WriteToCanvas(questDef));
            }

            if (inventorySystem.HasItem("Opel") && inventorySystem.GetItemCount("Opel") >= 1)
            {
                StartCoroutine(WriteToCanvas("Oyun bitti BB"));
            }
        }
    }


    IEnumerator WriteToCanvas(string questMessage)
    {
        if(questCounter > 0)
        {
            infoText.text = "GOREV BASARILI!";
            yield return new WaitForSeconds(5);
        }
        yield return null;

        infoText.text = questMessage;
    }
}
