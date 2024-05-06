using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowInventory : MonoBehaviour
{
    private InventorySystem inventorySystem;
    private bool isActive;
    [SerializeField] private TMP_Text emerald;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private TMP_Text ruby;
    [SerializeField] private TMP_Text safir;
    [SerializeField] private TMP_Text ice;
    [SerializeField] private TMP_Text diamond;
    [SerializeField] private TMP_Text carbon;
    [SerializeField] private TMP_Text opel;
    // Start is called before the first frame update
    void Start()
    {
        inventorySystem = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(isActive)
            {
                isActive = false;
            }else
            {
                isActive = true;
            }
        }

        if(isActive)
        {
            inventoryUI.SetActive(true);
        }else
        {
            inventoryUI.SetActive(false);
        }

        emerald.text = inventorySystem.GetItemCount("Emerald").ToString();
        safir.text = inventorySystem.GetItemCount("Sapphire").ToString();
        ice.text = inventorySystem.GetItemCount("Ice").ToString();
        diamond.text = inventorySystem.GetItemCount("Diamond").ToString();
        carbon.text = inventorySystem.GetItemCount("Carbon").ToString();
        opel.text = inventorySystem.GetItemCount("Opel").ToString();
        ruby.text = inventorySystem.GetItemCount("Ruby").ToString();
    }
}
