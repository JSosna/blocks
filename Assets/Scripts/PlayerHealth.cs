using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int playerHealth = 10;

    private const float healthReduceThreshold = 60f;
    private float timeLeftToReduceHealth;

    // Start is called before the first frame update
    void Start()
    {
        timeLeftToReduceHealth = healthReduceThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftToReduceHealth -= Time.deltaTime;

        if(timeLeftToReduceHealth < 0) {
            timeLeftToReduceHealth = healthReduceThreshold;
            playerHealth--;
            DisplayHealthStatus();
        }
    }

    private void DisplayHealthStatus() {
        if(playerHealth <= 0) {
            //TODO: Death screen
        }

        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }

        for (int i = 0; i < playerHealth; i++) {
            transform.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void AddHealth(int health) {
        playerHealth += health;
        if (playerHealth > 10) playerHealth = 10;
        timeLeftToReduceHealth = healthReduceThreshold;
    }
}
