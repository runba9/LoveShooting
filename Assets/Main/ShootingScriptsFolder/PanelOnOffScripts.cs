using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOnOffScripts : MonoBehaviour
{
    //パネル
    [SerializeField]
    public GameObject OptionPanel;
    public void OnOptionPanel()         //表示させたいパネル
    {
        OptionPanel.SetActive(true);
    }
    public void OffOptionPanel()         //非表示させたいパネル
    {
        OptionPanel.SetActive(false);
    }
}
