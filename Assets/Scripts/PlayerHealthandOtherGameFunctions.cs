using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthandOtherGameFunctions : MonoBehaviour
{
    public Text healthText;
    public int playerHealth = 10;
    public bool isDead;
    public PlayerCollision playerCollision;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "10" ;   
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollision.isTriggered == true) {
            playerHealth--;
            


        }
    }
}
