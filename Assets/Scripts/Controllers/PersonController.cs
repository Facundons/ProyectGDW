using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    [SerializeField] private GameObject[] goodLvlEmojis;
    [SerializeField] private GameObject[] midLvlEmojis;
    [SerializeField] private GameObject[] badLvlEmojis;
    [SerializeField] private ParticleSystem confetiParticles;

    public void ShowEmoji(int score) {
        var emoji = new GameObject();
        switch (score)
        {
            case int val when score >= 75:
                emoji = goodLvlEmojis[Random.Range(0, goodLvlEmojis.Length - 1)];
                emoji.SetActive(true);
                StartCoroutine(Yoyo(emoji));
                StartCoroutine(JumpPerson());
                confetiParticles.Play();
                break;

            case int val when score >= 50 && score < 75 :
                emoji = goodLvlEmojis[Random.Range(0, goodLvlEmojis.Length - 1)];
                emoji.SetActive(true);
                StartCoroutine(Yoyo(emoji));
                StartCoroutine(JumpPerson());
                break;

            case int val when score >= 25 && score < 50:
                emoji = midLvlEmojis[Random.Range(0, midLvlEmojis.Length - 1)];
                emoji.SetActive(true);
                StartCoroutine(Yoyo(emoji));
                StartCoroutine(LeanPerson());
                break;

            case int val when score <= 25:
                emoji = badLvlEmojis[Random.Range(0, badLvlEmojis.Length - 1)];
                emoji.SetActive(true);
                StartCoroutine(Yoyo(emoji));
                StartCoroutine(ShakePerson());
                break;

            default:
                break;
        }

    }


    private IEnumerator Yoyo(GameObject emoji)
    {
        var yoyoTween = LeanTween.moveY(emoji, emoji.transform.position.y + 0.2f, 1f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
        yield return new WaitForSeconds(3f);
        LeanTween.cancel(yoyoTween.uniqueId);
        emoji.SetActive(false);
    }

    private IEnumerator ShakePerson()
    {
        var shakeTween = LeanTween.moveY(gameObject, gameObject.transform.position.y + 0.05f, 0.05f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
        yield return new WaitForSeconds(1.5f);
        LeanTween.cancel(shakeTween.uniqueId);
    }

    private IEnumerator JumpPerson()
    {
        var jumpTween = LeanTween.moveY(gameObject, gameObject.transform.position.y + 1f, 1.5f).setEase(LeanTweenType.easeInBounce).setLoopPingPong();
        yield return new WaitForSeconds(3f);
        LeanTween.cancel(jumpTween.uniqueId);
    }


    private IEnumerator LeanPerson()
    {
        Vector3 finalRotation = gameObject.transform.localRotation.eulerAngles;
        finalRotation.x -= 20f;
        finalRotation.y += 10f;

        var leanTween = LeanTween.rotateLocal(gameObject, finalRotation, 1.5f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
        yield return new WaitForSeconds(3f);
        LeanTween.cancel(leanTween.uniqueId);
    }
}
