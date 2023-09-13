using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public GameObject throwingPlayer;

    protected float forceMultiplier = 15f;
    protected float maxThrowAngle = 80f;
    protected float flightCurvatureDuration = 0.5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public IEnumerator ThrowCoroutine(float forceRation)
    {
        float force = forceRation * forceMultiplier;

        Rigidbody rb = GetComponent<Rigidbody>();
        
        Quaternion startRotation = throwingPlayer.transform.rotation * Quaternion.Euler(Vector3.right * -maxThrowAngle);
        Quaternion endRotation = throwingPlayer.transform.rotation * Quaternion.Euler(Vector3.right * maxThrowAngle);

        float timer = 0;

        while (timer < flightCurvatureDuration)
        {
            timer += Time.deltaTime;
            float lerp = timer / flightCurvatureDuration;
            gameObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, lerp);

            rb.AddForce(gameObject.transform.forward * force, ForceMode.Force);

            yield return null;
        }
    }
}
