using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 targetZoomIn = new Vector3(0f, -2f, -1f); // The target position to zoom in on
    public Vector3 targetZoomOut = new Vector3(0f, 0f, -7f); // The target position to zoom out to
    public float zoomTime = 1.0f;

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

    }
}
