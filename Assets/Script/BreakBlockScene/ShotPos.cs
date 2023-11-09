using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPos : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    Vector3 vec = new(0f, 0f, 0.8f);

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + vec;
    }
}
