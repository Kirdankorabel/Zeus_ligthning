using UnityEngine;
using UnityEngine.Pool;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private int _min;
    [SerializeField] private int _max;
    [SerializeField] private float _dropChance;
    [SerializeField] private float _dropChancePerPoint;
    [SerializeField] private int _defaultPoolCapacity = 10;
    [SerializeField] private int _maxPoolSize = 20;

    private ObjectPool<Coin> _pool;
    void Start()
    {
        _pool = new ObjectPool<Coin>(
            InstantiateCoin,
            OnGet,
            OnReleas,
            OnDestroyElement,
            false,
            _defaultPoolCapacity,
            _maxPoolSize);

        GameController.Instance.OnTargetTouch += (value) => SpawnCoins(value, Counter.Instance.Count);
    }

    private void SpawnCoins(Vector3 position, int pointCount)
    {
        var value = Random.Range(0f, 1f);
        if(value < (_dropChance + _dropChancePerPoint * pointCount) / 100f)
        {
            var coinCount = Random.Range(_min, _max);
            for(var i = 0; i < coinCount; i++)
            {
                SpawnCoin(position);
            }
        }
    }

    private void SpawnCoin(Vector3 position)
    {
        var coin = _pool.Get();
        coin.StartDispersion(position, _targetTransform.position);
    }

    #region pool methods
    private Coin InstantiateCoin()
    {
        var coin = Instantiate(_coinPrefab, transform);
        coin.OnEnable += () => _pool.Release(coin);
        return coin;
    }

    private void OnGet(Coin gameObject)
    {
        gameObject.gameObject.SetActive(true);
    }

    private void OnReleas(Coin gameObject)
    {
        gameObject.gameObject.SetActive(false);
    }

    private void OnDestroyElement(Coin gameObject) { }
    #endregion
}
