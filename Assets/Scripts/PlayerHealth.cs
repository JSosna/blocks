using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int _health = 10;
    public int Health {
        get {
            return _health;
        }
        set {
            _health += value;
            if (_health > 10) _health = 10;
            timeLeftToReduceHealth = healthReduceThreshold;

            DisplayHealthStatus();
        }
    }

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
            _health--;
            DisplayHealthStatus();
        }
    }

    private void DisplayHealthStatus() {
        if(_health <= 0) {
            //TODO: Death screen
        }

        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }

        for (int i = 0; i < Health; i++) {
            transform.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
}
