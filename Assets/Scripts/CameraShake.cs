using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public Transform playerCam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void ShakeCamera(float duration, float shakeSize)
    {
        StartCoroutine(Shake(duration, shakeSize));
    }

    private IEnumerator Shake(float duration, float shakeSize)
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = playerCam.localPosition;
        while (elapsedTime < duration) 
        {
            float x = Random.Range(-1f, 1f) * shakeSize; // make less if i want less shake on x
            float y = Random.Range(-1f, 1f) * shakeSize; // make less if i want less shake on y

            Vector3 pos = new Vector3(x, y, originalPosition.z);
            playerCam.localPosition = Vector3.Lerp(playerCam.localPosition, pos, Time.deltaTime * 5f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        while (Vector3.Distance(playerCam.position, originalPosition) > 0.01f)
        {
            playerCam.localPosition = Vector3.Lerp(playerCam.localPosition, originalPosition, Time.deltaTime * 5f);
            yield return null;
        }

        playerCam.localPosition = originalPosition;
    }
}
