using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBall : MonoBehaviour
{
    private Player p;

    [SerializeField]
    GameObject shotPos;

    // Start is called before the first frame update
    void Start()
    {
        p = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform children = shotPos.GetComponentInChildren<Transform>();
            //子要素がいなければ終了
            if (children.childCount == 0)
            {
                return;
            }

            foreach (Transform ob in children)
            {
                if (ob.CompareTag("Ball"))
                {
                    //プレイヤーの元素取得
                    Elements playerElem = p.GetPlayerElem();
                    //ボールクラス取得
                    Ball b = ob.GetComponent<Ball>();
                    //Debug.Log("kitemoutemasu");
                    //ボール元素=プレイヤー元素
                    b.ChangeBallElement(playerElem);
                    b.ShowParticle();
                    //発射
                    ob.GetComponent<Rigidbody>().AddForce((transform.forward + transform.right) * b.ballSpeed, ForceMode.VelocityChange);
                    //ShotPosとの親子関係卒業
                    ob.gameObject.transform.parent = null;
                }
            }
        }
    }
}
