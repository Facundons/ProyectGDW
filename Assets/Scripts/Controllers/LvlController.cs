using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LvlController : MonoBehaviour
{
    public GameObject[] LvlsSandwich;
    [SerializeField] GameObject chatBubble;
    private int currentLvl;


    public void ShowLvl(int lvl)
    {
        if(lvl >= LvlsSandwich.Length) lvl = Random.Range(0, LvlsSandwich.Length - 1);       
        chatBubble.SetActive(true);
        currentLvl = lvl;
        LvlsSandwich[lvl].SetActive(true);
        var amountOfChildsInLvl = LvlsSandwich[lvl].transform.childCount;
        StartCoroutine(Yoyo(LvlsSandwich[lvl], 2f));
    }

    public void HideLvl(int lvl)
    {
        LvlsSandwich[lvl].SetActive(false);
    }

    public GameObject getCurrentLvl()
    {
        return LvlsSandwich[currentLvl];
    }

    public void MoveLvlSandwichToTop()
    {
        var amountOfChildsInLvl = LvlsSandwich[currentLvl].transform.childCount;
        chatBubble.SetActive(false);
        StartCoroutine(MoveObject(LvlsSandwich[currentLvl], 2f, amountOfChildsInLvl));
    }

    IEnumerator MoveObject(GameObject GO, float time, int ammountOfIngredients)
    {
        var SandwichDestination = new Vector3(GO.transform.position.x + 3.1f, GO.transform.position.y + 7.3f - ammountOfIngredients * 0.055f, GO.transform.position.z);
        Vector3 startPosition = GO.transform.position;
        float startTime = Time.time;
        float endTime = startTime + time;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / time;
            GO.transform.position = Vector3.Lerp(startPosition, SandwichDestination, t);
            yield return null;
        }
    }

    private IEnumerator Yoyo(GameObject sandwich, float time)
    {
        var yoyoTween = LeanTween.moveY(sandwich, sandwich.transform.position.y + 0.1f, 1f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
        yield return new WaitForSeconds(time);
        LeanTween.cancel(yoyoTween.uniqueId);
    }

    public void MoveLvlSandwich()
    {
        var tween = LeanTween.moveY(LvlsSandwich[currentLvl], LvlsSandwich[currentLvl].transform.position.y + 0.5f, 2f).setEase(LeanTweenType.easeInOutQuad);
    }


    public void DisableLastSandwichLvlOnUi()
    {
        this.LvlsSandwich[currentLvl].SetActive(false);
        var amountOfChildsInLvl = LvlsSandwich[currentLvl].transform.childCount;
        this.LvlsSandwich[currentLvl].transform.position = new Vector3(this.LvlsSandwich[currentLvl].transform.position.x - 3.1f, this.LvlsSandwich[currentLvl].transform.position.y - 7.3f + amountOfChildsInLvl * 0.055f, this.LvlsSandwich[currentLvl].transform.position.z);
    }
}
