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
            //�q�v�f�����Ȃ���ΏI��
            if (children.childCount == 0)
            {
                return;
            }

            foreach (Transform ob in children)
            {
                if (ob.CompareTag("Ball"))
                {
                    //�v���C���[�̌��f�擾
                    Elements playerElem = p.GetPlayerElem();
                    //�{�[���N���X�擾
                    Ball b = ob.GetComponent<Ball>();
                    //Debug.Log("kitemoutemasu");
                    //�{�[�����f=�v���C���[���f
                    b.ChangeBallElement(playerElem);
                    b.ShowParticle();
                    //����
                    ob.GetComponent<Rigidbody>().AddForce((transform.forward + transform.right) * b.ballSpeed, ForceMode.VelocityChange);
                    //ShotPos�Ƃ̐e�q�֌W����
                    ob.gameObject.transform.parent = null;
                }
            }
        }
    }
}
