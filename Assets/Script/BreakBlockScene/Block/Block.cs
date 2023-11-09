using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //�u���b�N�̔z�u���W�ێ�
    [HideInInspector]
    public int row;
    [HideInInspector]
    public int col;

    //�u���b�N�������Ă��錳�f
    public Elements blockElement;

    public bool isFrozen;

    public int blockHp = 2;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "Block";

        //block�̔��˗pplane�Ƀ^�O������
        AddTagToChildren(this.gameObject, "BlockPlane");

        isFrozen = false;

        //�֊s�̐ݒ�
        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.black;
        outline.OutlineWidth = 8f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            BlockToDamage(1);
        }
    }

    private void AddTagToChildren(GameObject obj, string tagName)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //�q�v�f�����Ȃ���ΏI��
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            //tag��ύX
            ob.tag = tagName;
        }
    }

    public void BlockToDamage(int damage)
    {
        blockHp -= damage;
        if(blockHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.DecreaseBlockCnt();
    }

}
