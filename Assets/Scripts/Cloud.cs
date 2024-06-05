using UnityEngine;
using UnityEngine.Pool;

public class Cloud : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolCapaciti = 40;
    [SerializeField] private int _poolMaxSize = 100;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
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
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.SetActive(true);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    public void ReturnCubeInCloud(GameObject cube)
    {
        _pool.Release(cube);
    }

    private void Start()
    {
        float rainStartTime = 0f;
        float cubeRepeatTime = 0.05f;

        InvokeRepeating(nameof(GetCube), rainStartTime, cubeRepeatTime);
    }
}
