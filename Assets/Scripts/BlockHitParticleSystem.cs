using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHitParticleSystem : MonoBehaviour
{

    private void Start() {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration + 0.8f);
    }
}
