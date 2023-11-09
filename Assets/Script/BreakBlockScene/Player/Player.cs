using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance
    {
        get;
        private set;
    }

    private Renderer r;

    //プレイヤー色の変更のためのマテリアル
    [NamedArrayAttribute(new string[] { "None", "Pyro", "Hydro", "Cryo", "Electro" })]
    public Material[] elementMaterial;


    public float playerSpeed = 1.0f;

    private Elements playerElement;


    private void Awake()
    {
        CreateInstance();
        r = this.GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerElement = Elements.None;
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Elements GetPlayerElem()
    {
        return playerElement;
    }

    public void ChangeMyElement(Elements elem)
    {
        playerElement = elem;
        r.material = elementMaterial[(int)elem];
        ChangeChildrenColor(elem);
    }

    private void ChangeChildrenColor(Elements elem)
    {
        Transform children = this.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            //色を変更
            ob.GetComponent<Renderer>().material = elementMaterial[(int)elem];
        }
    }
}
