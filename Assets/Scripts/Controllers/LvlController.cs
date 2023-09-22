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
        for (int i = 0; i < amountOfChildsInLvl; i++)
        {
            var child = LvlsSandwich[currentLvl].transform.GetChild(i).gameObject;
            StartCoroutine(MoveObject(child, 2f, amountOfChildsInLvl));
        }
    }

    IEnumerator MoveObject(GameObject ingredient, float time, int ammountOfIngredients)
    {
        var SandwichDestination = new Vector3(ingredient.transform.position.x + 3.1f, ingredient.transform.position.y + 7.3f - ammountOfIngredients * 0.055f, ingredient.transform.position.z);
        Vector3 startPosition = ingredient.transform.position;
        float startTime = Time.time;
        float endTime = startTime + time;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / time;
            ingredient.transform.position = Vector3.Lerp(startPosition, SandwichDestination, t);
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

}
