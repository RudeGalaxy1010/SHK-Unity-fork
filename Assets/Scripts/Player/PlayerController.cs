using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpeedUpgrade))]
public class PlayerController : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField] private float _speed;
    [SerializeField] private EndGameController _endGameController;

    private SpeedUpgrade _speedUpgrade;

    private void OnEnable()
    {
        _endGameController.Won += () => gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _endGameController.Won -= () => gameObject.SetActive(false);
    }

    private void Start()
    {
        _speedUpgrade = GetComponent<SpeedUpgrade>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float xInput = Input.GetAxis(HorizontalAxis);
        float yInput = Input.GetAxis(VerticalAxis);

        Vector3 moveDirection = new Vector3(xInput, yInput) * _speed * Time.deltaTime;
        transform.Translate(moveDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.TryGetComponent(out SpeedUpgradeObject speedUpgradeObject))
        {
            Destroy(collision.gameObject);
            _speedUpgrade.Started += OnSpeedUpgradeStarted;
            _speedUpgrade.Finished += OnSpeedUpgradeFinished;
            _speedUpgrade.enabled = true;
        }
    }

    private void OnSpeedUpgradeStarted(SpeedUpgrade speedUpgrade)
    {
        speedUpgrade.Started -= OnSpeedUpgradeStarted;
        _speed *= speedUpgrade.SpeedFactor;
    }

    private void OnSpeedUpgradeFinished(SpeedUpgrade speedUpgrade)
    {
        speedUpgrade.Finished -= OnSpeedUpgradeFinished;
        _speed /= speedUpgrade.SpeedFactor;
    }
}
