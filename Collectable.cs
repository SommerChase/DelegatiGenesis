using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    enum ItemType { coin, health, ammo, inventoryItem}
    [SerializeField] private ItemType itemType;
    [SerializeField] private string inventoryStringName;
    [SerializeField] private Sprite inventorySprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            if (itemType == ItemType.coin)
            {
                NewPlayer.Instance.coinsCollected += 1;
            }
            else if (itemType == ItemType.health)
            {
                if (NewPlayer.Instance.health < 100)
                {
                    NewPlayer.Instance.health += 5;
                }
                
            }
            else if (itemType == ItemType.ammo)
            {
                
            }
            else if (itemType == ItemType.inventoryItem)
            {
                NewPlayer.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
            }
            else
            {
                
            }

            gameObject.SetActive(false); //Makes object appear as though it's been picked up.

            NewPlayer.Instance.UpdateUI();
            
        }

    }
}
