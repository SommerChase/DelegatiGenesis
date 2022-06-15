using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed;
    
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private float raycastLength = 1f;
    [SerializeField] private Vector2 raycastOffset;
    [SerializeField] private int attackPower = 10;
    
    private int direction = 1;
    public int health = 100;
    private int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction,0);

        //Ledges
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * raycastLength, Color.red);
        if(rightLedgeRaycastHit.collider == null) direction = -1;

        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * raycastLength, Color.yellow);
        if(leftLedgeRaycastHit.collider == null) direction = 1;

        //Walls
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, raycastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * raycastLength, Color.blue);
        if(rightWallRaycastHit.collider != null) direction = -1;

        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, raycastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * raycastLength, Color.green);
        if(leftWallRaycastHit.collider != null) direction = 1;
        
        //Death and destruction of enemies
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

        // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            Debug.Log("Player hit");
            NewPlayer.Instance.health -= attackPower;
            NewPlayer.Instance.UpdateUI();
        }
    }
}
