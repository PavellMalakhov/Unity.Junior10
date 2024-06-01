using System.Collections;
using UnityEngine.Pool;
using UnityEngine;

public class GameObjecstSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolCapaciti = 25;
    [SerializeField] private int _poolMaxSize = 50;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        Vector3 startPosition = new Vector3(0, 10, 0);
        float sizeCube = 0.1f;

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = startPosition;
        cube.AddComponent<Rigidbody>();
        cube.transform.localScale = new Vector3(sizeCube, sizeCube, sizeCube);
        cube.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        _prefab = cube;

        _pool = new ObjectPool<GameObject>(
        createFunc: () => Instantiate(_prefab),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: _poolCapaciti,
        maxSize: _poolMaxSize);
    }

    private void ActionOnGet(GameObject obj)
    {
        float cloudSize = 5f;
        Color cubeColor = new Color(1, 1, 1);

        obj.transform.position = new Vector3(Random.Range(-cloudSize, cloudSize), 10, Random.Range(-cloudSize, cloudSize));
        obj.GetComponent<Renderer>().material.color = cubeColor;
        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.SetActive(true);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void Start()
    {
        float rainStartTime = 0f;
        float cubeRepeatTime = 0.05f;

        InvokeRepeating(nameof(GetCube), rainStartTime, cubeRepeatTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().isKinematic = true;

        other.GetComponent<Renderer>().material.color = GetRandomColor();

        float lifetimeMinCube = 2f;
        float lifetimeMaxCube = 5f;
        float delay = Random.Range(lifetimeMinCube, lifetimeMaxCube);

        StartCoroutine(CountUp(delay, other));
    }

    private IEnumerator CountUp(float delay, Collider other)
    {
        var wait = new WaitForSeconds(delay);

        yield return wait;

        _pool.Release(other.gameObject);
    }

    private Color GetRandomColor()
    {
        float colorChannelR = Random.value;
        float colorChannelG = Random.value;
        float colorChannelB = Random.value;

        return new Color(colorChannelR, colorChannelG, colorChannelB);
    }
}
