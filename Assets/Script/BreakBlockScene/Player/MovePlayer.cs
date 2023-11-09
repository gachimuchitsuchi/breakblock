using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Player p;

    private void Start()
    {
        p = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (this.gameObject.transform.position.x > -8)
                this.gameObject.transform.position += Vector3.left * p.playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (this.gameObject.transform.position.x < 8)
                this.gameObject.transform.position += Vector3.right * p.playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (this.gameObject.transform.position.z < -6)
            {
                this.gameObject.transform.position += Vector3.forward * p.playerSpeed * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (this.gameObject.transform.position.z > -9)
            {
                this.gameObject.transform.position += Vector3.back * p.playerSpeed * Time.deltaTime;
            }
        }
    }
}
