using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOnOffScripts : MonoBehaviour
{
    //�p�l��
    [SerializeField]
    public GameObject OptionPanel;
    public void OnOptionPanel()         //�\�����������p�l��
    {
        OptionPanel.SetActive(true);
    }
    public void OffOptionPanel()         //��\�����������p�l��
    {
        OptionPanel.SetActive(false);
    }
}
