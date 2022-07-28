using UnityEngine;
using UnityEngine.Events;

public class SpeedUpgrade : MonoBehaviour
{
    public event UnityAction<SpeedUpgrade> Started;
    public event UnityAction<SpeedUpgrade> Finished;

    [SerializeField] private float _estimateTime;
    [SerializeField] private float _speedFactor;

    private float _timer;

    public float SpeedFactor => _speedFactor;

    private void OnEnable()
    {
        Started?.Invoke(this);
        _timer = 0;
    }

    private void Update()
    {
        if (_timer >= _estimateTime)
        {
            Finished?.Invoke(this);
            enabled = false;
        }

        _timer += Time.deltaTime;
    }
}
