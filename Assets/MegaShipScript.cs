using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MegaShipScript : MonoBehaviour
{
    public Canvas bossCanvas;
    public Text hpText;
    int maxHp;
    public Slider hpSlider;
    // Update is called once per frame


    private void Start()
    {
        maxHp = GetComponent<EnemyShip>().hp;
    }
    void Update()
    {
        if(GetComponent<EnemyShip>().isActiveAndEnabled)
        {
            bossCanvas.enabled = true;
            hpSlider.value = GetComponent<EnemyShip>().hp;
            hpText.text = GetComponent<EnemyShip>().hp.ToString() + " / " + maxHp.ToString();
        }
        else
        {
            bossCanvas.enabled = false;
        }
    }
}
