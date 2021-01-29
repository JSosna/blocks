using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] cloudPrefabs;

    [SerializeField]
    GameObject player;

    List<System.Tuple<GameObject, int>> cloudsWithSpeeds = new List<System.Tuple<GameObject, int>>();


    // Start is called before the first frame update
    void Start()
    {
        CreateCloud();
    }

    // Update is called once per frame
    void Update()
    {
        if (cloudsWithSpeeds.Count < 10)
            CreateCloud();

        for (int i = 0; i < cloudsWithSpeeds.Count; i++)
        {
            // Move the cloud
            cloudsWithSpeeds[i].Item1.transform.Translate(Vector3.forward * (Time.deltaTime * cloudsWithSpeeds[i].Item2));
            
            // Check if it should be destroyed
            if ((player.transform.position.x - cloudsWithSpeeds[i].Item1.transform.position.x) *
                (player.transform.position.x - cloudsWithSpeeds[i].Item1.transform.position.x) +
                (player.transform.position.z - cloudsWithSpeeds[i].Item1.transform.position.z) *
                (player.transform.position.z - cloudsWithSpeeds[i].Item1.transform.position.z) > 350 * 350)
            {
                
                var cloudToDestroy = cloudsWithSpeeds[i].Item1;
                cloudsWithSpeeds.Remove(cloudsWithSpeeds[i]);
                Destroy(cloudToDestroy);
            }
                
        }
    }

    void CreateCloud()
    {
        var startingPos = new Vector3(
            Random.Range(player.transform.position.x - 300, player.transform.position.x + 300), 
            Random.Range(110, 125),
            Random.Range(player.transform.position.z - 300, player.transform.position.z + 100));

        GameObject cloud = Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Length)]);
        cloud.transform.Translate(startingPos);

        cloudsWithSpeeds.Add(new System.Tuple<GameObject, int>(cloud, Random.Range(2, 6)));
    }
}
