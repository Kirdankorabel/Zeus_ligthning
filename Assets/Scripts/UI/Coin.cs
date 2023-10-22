using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _dispersionTime = 1f;
    private float _speed;

    public event System.Action OnEnable;
    public static event System.Action OnWalletHitted;

    public void StartDispersion(Vector3 startPosition, Vector3 targetPosition)
    {
        transform.position = startPosition;
        StartCoroutine(DispersionCorutine(startPosition));
        StartCoroutine(MoveCorutine(targetPosition));
    }

    private IEnumerator DispersionCorutine(Vector3 startPosition)
    {
        var time = Time.time;
        var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        while (Time.time - time < _dispersionTime)
        {
            transform.position = transform.position + direction * _startSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MoveCorutine(Vector3 targetPosition)
    {
        while(transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            _speed += _acceleration * Time.deltaTime;
            yield return null;
        }
        _speed = 0f;
        OnWalletHitted?.Invoke();
        OnEnable?.Invoke();
    }
}
