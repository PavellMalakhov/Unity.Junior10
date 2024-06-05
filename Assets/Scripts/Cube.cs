using System.Collections;
using UnityEngine;


public class Cube : MonoBehaviour
{
    [SerializeField] private Cloud _cloud;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        gameObject.GetComponent<Renderer>().material.color = GetRandomColor();

        float lifetimeMinCube = 2f;
        float lifetimeMaxCube = 5f;
        float delay = Random.Range(lifetimeMinCube, lifetimeMaxCube);

        StartCoroutine(CountUp(delay));
    }

    private IEnumerator CountUp(float delay)
    {
        var wait = new WaitForSeconds(delay);

        yield return wait;

        if (gameObject.activeSelf == true)
        {
            _cloud.ReturnCubeInCloud(gameObject);
            yield break;
        }
        else
        {
            yield break;
        }
    }

    private Color GetRandomColor()
    {
        float colorChannelR = Random.value;
        float colorChannelG = Random.value;
        float colorChannelB = Random.value;

        return new Color(colorChannelR, colorChannelG, colorChannelB);
    }
}
