using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Snapper : MonoBehaviour
{
    void Update()
    {
        Vector3 snap;
        snap.x = Mathf.RoundToInt(transform.position.x);
        snap.y = Mathf.RoundToInt(transform.position.y);
        snap.z = Mathf.RoundToInt(transform.position.z);

        transform.position = new Vector3(snap.x, snap.y, snap.z);
    }
}
