using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 targetZoomIn = new Vector3(0f, -2f, -1.4f);
    private Vector3 targetZoomOut = new Vector3(0f, 0f, -7f);
    private bool hasTriggered = false;
    public float zoomTime;
    public delegate void OnIngredientReachingTheTop();
    public static OnIngredientReachingTheTop onIngredientReachingTheTop;
    public delegate void OnCameraZoomedIn();
    public static OnCameraZoomedIn onCameraZoomedIn;
    public delegate void OnCameraZoomedOut();
    public static OnCameraZoomedOut onCameraZoomedOut;


    private void Awake()
    {
        UiController.onCombo += shakeCamera;
    }

    private void shakeCamera(float combo)
    {
        if (combo != 1) StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera()
    {
        var twistTween = LeanTween.rotate(gameObject, new Vector3(0f, 0f, Random.Range(-5, +5)), 0.5f).setEase(LeanTweenType.easeInOutCubic).setLoopPingPong();
        yield return new WaitForSeconds(1f);
        LeanTween.cancel(twistTween.uniqueId);
    }

    public void ZoomCameraIn()
    {
        StartCoroutine(ZoomIn());
    }

    public void ZoomCameraOut()
    {
        StartCoroutine(ZoomOut());
    }

    public IEnumerator ZoomIn()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;
        onCameraZoomedIn?.Invoke();
        while (elapsedTime < zoomTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetZoomIn, elapsedTime / zoomTime);
            yield return null;
        }

    }

    public IEnumerator ZoomOut()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < zoomTime)
        {
            elapsedTime += Time.deltaTime;            
            transform.position = Vector3.Lerp(startPosition, targetZoomOut, elapsedTime / zoomTime);
            yield return null;
        }
        if (elapsedTime >= zoomTime) onCameraZoomedOut?.Invoke();
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Trigger") && other.transform.parent.GetComponent<Rigidbody>().velocity.y > 0 && !hasTriggered )
        {
            onIngredientReachingTheTop?.Invoke();
            LeanTween.moveY(gameObject, gameObject.transform.position.y + 0.5f, 2f).setEase(LeanTweenType.easeInOutQuad);
            hasTriggered = true;
            StartCoroutine(WaitForCameraMovement());
        }
    }

    IEnumerator WaitForCameraMovement()
    {
        yield return new WaitForSeconds(2f);
        hasTriggered = false;
    }

}
