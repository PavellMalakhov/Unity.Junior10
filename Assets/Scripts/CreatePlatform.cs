using UnityEngine;

public class CreatePlatform : MonoBehaviour
{
    private void Awake()
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 platformSize = new Vector3(10, 1, 10);
        string platformName = "Platform";

        platform.name = platformName;
        platform.GetComponent<Renderer>().material.color = new Color();
        platform.transform.localScale = platformSize;
        platform.AddComponent<GameObjecstSpawner>();
        platform.GetComponent<Collider>().isTrigger = true;
        platform.AddComponent<BoxCollider>();
    }
}
