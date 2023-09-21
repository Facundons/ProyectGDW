using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlController : MonoBehaviour
{
    public GameObject[] LvlsSandwich;
    private int currentLvl;


    public void ShowLvl(int lvl)
    {
        currentLvl = lvl;
        LvlsSandwich[lvl].SetActive(true);
    }

    public void HideLvl(int lvl)
    {
        LvlsSandwich[lvl].SetActive(false);
    }

    public GameObject getCurrentLvl()
    {
        return LvlsSandwich[currentLvl];
    }

}
