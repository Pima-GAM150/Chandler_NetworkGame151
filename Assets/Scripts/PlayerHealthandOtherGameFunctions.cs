using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthandOtherGameFunctions : MonoBehaviour
{
    public Text healthText;
    public int playerHealth = 10;
    int amount = 1;
    public bool isDead;
    public GameObject player;
    PlayerCollision playerCollision;
  public bool reduceHealth = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCollision = player.GetComponentInChildren<PlayerCollision>();
        healthText.text = "10" ;   
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(waitForTrigger());
 
            if (PlayerCollision.find.isTriggered == true)
            {
                takeDamage(amount);

            }

        
        StopAllCoroutines();
        
    }

    public void takeDamage(int amount) {

        reduceHealth = true;

        playerHealth -= amount;

        reduceHealth = false;

    }

    IEnumerator waitForTrigger()
    {

        yield return new WaitUntil(() => PlayerCollision.find.isTriggered == true);
        
    }
    IEnumerator waitForEndOfFrame() {
        yield return new WaitForEndOfFrame();
    }
}
