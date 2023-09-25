using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendlumController : MonoBehaviour
{
    public float speed = 1.5f; 
    public float angle = 45.0f; 
    private Quaternion _qStart, _qEnd;

    void Start()
    {
        _qStart = Quaternion.AngleAxis(angle, Vector3.forward);
        _qEnd = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    void Update()
    {
        transform.localRotation = Quaternion.Lerp(_qStart, _qEnd, (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f);
    }


    public void MoveYPendlum(float yMovement)
    {
        var ingredientSpawner = gameObject.transform.GetChild(0).gameObject;
        var pendlumTween = LeanTween.moveY(gameObject, gameObject.transform.position.y +yMovement, 2f).setEase(LeanTweenType.easeInOutQuad);
        pendlumTween.setOnComplete(()=> { ingredientSpawner.transform.localPosition = new Vector3(0, -12.67f, 0); });
    }


    public void ResetPendlumForLvlStart()
    {
        var ingredientSpawner = gameObject.transform.GetChild(0).gameObject;
        var pendlumTween = LeanTween.moveY(gameObject, 4.05f, 2f).setEase(LeanTweenType.easeInOutQuad);      
        pendlumTween.setOnComplete(() => { ingredientSpawner.transform.localPosition = new Vector3(0, -12.67f, 0); });
    }
}
