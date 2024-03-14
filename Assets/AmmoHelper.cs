using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHelper : MonoBehaviour
{
    [SerializeField] Image[] bulletUi;
    [SerializeField] TextMeshProUGUI[] ReserveUI;

    public void ChangeAmmoColor(Color col)
    {

        Debug.Log("woidmsodmsdpspsdsmpsodmpsopows");
        foreach(Image i in bulletUi)
        {
            i.color = col;
        }

        foreach (TextMeshProUGUI t in ReserveUI)
        {
            t.color = col;
        }
    }
}
