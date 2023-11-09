using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //ブロックの配置座標保持
    [HideInInspector]
    public int row;
    [HideInInspector]
    public int col;

    //ブロックが持っている元素
    public Elements blockElement;

    public bool isFrozen;

    public int blockHp = 2;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.tag = "Block";

        //blockの反射用planeにタグをつける
        AddTagToChildren(this.gameObject, "BlockPlane");

        isFrozen = false;

        //輪郭の設定
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
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            //tagを変更
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
