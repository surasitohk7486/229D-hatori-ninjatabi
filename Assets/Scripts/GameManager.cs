using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cat;
    public GameObject dog;

    public bool isCatTurn = true;

    private Player catController;
    private Player_NinjaM dogController;

    private void Start()
    {
        catController = cat.GetComponent<Player>();
        dogController = dog.GetComponent<Player_NinjaM>();
    }

    private void Update()
    {
        // Check if both players are still alive
        if (catController.HP > 0 && dogController.HP > 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (isCatTurn)
                {
                    isCatTurn = false;
                    
                }
                else
                {
                    
                    
                }
            }
        }
    }
    private void ThrowObject(GameObject player)
    {
        // Implement throwing logic here, instantiate and throw object
        // Example: Instantiate projectile and apply force
    }
}
