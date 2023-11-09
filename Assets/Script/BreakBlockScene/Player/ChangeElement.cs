using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�o�[�̌��f��ύX����X�v���N�g
//1(��)2(��)3(�X)4(��)�ɕύX����

public class ChangeElement : MonoBehaviour
{
    private Player p;

    [SerializeField]
    private GameObject cautionText;

    private float time;
    private float changeCoolTime = 2.0f;
    private bool isChangeElement;

    // Start is called before the first frame update
    void Start()
    {
        p = Player.instance;
        isChangeElement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChangeElement)
        {
            time += Time.deltaTime;
            if (time > changeCoolTime)
            {
                isChangeElement = true;
                time = 0.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!isChangeElement)
            {
                StartCoroutine("ShowCautionText");
                return;
            }

            p.ChangeMyElement(Elements.Pyro);

            //Debug.Log("��");

            isChangeElement = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!isChangeElement)
            {
                StartCoroutine("ShowCautionText");
                return;
            }

            p.ChangeMyElement(Elements.Hydro);

            //Debug.Log("��");

            isChangeElement = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!isChangeElement)
            {
                StartCoroutine("ShowCautionText");
                return;
            }

            p.ChangeMyElement(Elements.Cryo);

            //Debug.Log("�X");

            isChangeElement = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!isChangeElement)
            {
                StartCoroutine("ShowCautionText");
                return;
            }

            p.ChangeMyElement(Elements.Electro);

            //Debug.Log("��");

            isChangeElement = false;
        }
    }

    IEnumerator ShowCautionText()
    {
        if (cautionText.activeSelf)
        {
            yield break;
        }

        float waitTime = 0.5f;

        cautionText.SetActive(true);

        //��~
        yield return new WaitForSeconds(waitTime);

        cautionText.SetActive(false);
    }
}
