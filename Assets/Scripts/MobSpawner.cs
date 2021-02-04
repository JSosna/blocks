using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private GameObject sheepPrefab;

    private int maximumMobCount = 8;

    private readonly float mobRespawnCooldown = 10f;

    private float currentMobRespawnCooldown = 10f;


    // Update is called once per frame
    void Update()
    {
        // Destroy sheep if it is too far from the player
        for(int i=0; i<transform.childCount; i++) {
            if(Vector3.Distance(transform.GetChild(i).position, playerTransform.position) > 250) {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        if(transform.childCount < maximumMobCount) {
            currentMobRespawnCooldown -= Time.deltaTime;

            if(currentMobRespawnCooldown <= 0)
                SpawnSheep();
        }
    }

    private void SpawnSheep() {
        var sheepPosition = new Vector3(
                playerTransform.position.x + Random.Range(-120, 120),
                100,
                playerTransform.position.z + Random.Range(-120, 120)
            );

        // Find a position of the ground and move there
        RaycastHit groundInfo;
        if (Physics.Raycast(sheepPosition, -transform.up, out groundInfo, 150f, groundLayer)) {
            sheepPosition = new Vector3(
                    groundInfo.point.x,
                    groundInfo.point.y + 2,
                    groundInfo.point.z
                );
        } 
        else { return; }


        // Check if we can see spawn point
        Vector3 screenPoint = camera.WorldToViewportPoint(sheepPosition);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        // If we can't - spawn sheep
        if (!onScreen) {
            var sheep = Instantiate(sheepPrefab, transform);
            sheep.GetComponent<Sheep>().SetInventory(inventory);
            sheep.transform.position = sheepPosition;
            currentMobRespawnCooldown = mobRespawnCooldown;
        }
    }
}
