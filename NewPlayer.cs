using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float gravityVelocity = 10;

    public int coinsCollected;
    public TMP_Text coinsText;

    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    public Image inventoryItemImage;
    public Sprite keySprite;
    public Sprite keyGemSprite;
    public Sprite inventoryItemBlank;

    private int maxHealth = 100;
    public int health = 100;
    public Image healthBar;
    private Vector2 originalHealthBarSize;
    [SerializeField] private GameObject hitBox;
    [SerializeField] private float attackDuration;
    public int attackPower = 25;

    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        coinsText = GameObject.Find("Coins").GetComponent<TMP_Text>();
        healthBar = GameObject.Find("Health Bar").GetComponent<Image>();
        originalHealthBarSize = healthBar.rectTransform.sizeDelta;
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        //If player presses jump and grounded, set velocity to a jump power value.
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = gravityVelocity;
        }

        if (targetVelocity.x < -0.01)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (targetVelocity.x > 0.01)
        {
            transform.localScale = new Vector2(1, 1);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());
        }
        
    }

    public IEnumerator ActivateAttack()
    {
        hitBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        hitBox.SetActive(false);
    }
    
    

    //Update UI elements
    public void UpdateUI()
    {
        coinsText.text = coinsCollected.ToString();
        //If (float) wasn't added, the calculation below wouldn't work because you cannot divide ints for some reason.
        healthBar.rectTransform.sizeDelta = new Vector2(originalHealthBarSize.x * ((float)health / (float)maxHealth), healthBar.rectTransform.sizeDelta.y);

    }

    public void AddInventoryItem(string inventoryName, Sprite image)
    {
        inventory.Add(inventoryName, image);
        inventoryItemImage.sprite = inventory[inventoryName];
    }

    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        inventoryItemImage.sprite = inventoryItemBlank;
    }
}
