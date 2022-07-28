using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    public event UnityAction<Enemy> Destoryed;

    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _moveRadius = 4f;

    private Vector3 _targetPosition;

    void Start()
    {
        _targetPosition = Random.insideUnitCircle * _moveRadius;
    }

    void Update()
    {
        if (transform.position == _targetPosition)
        {
            _targetPosition = Random.insideUnitCircle * _moveRadius;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Destoryed?.Invoke(this);
    }
}
