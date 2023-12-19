using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _rawInput;

    [SerializeField] private float _paddingLeft;
    [SerializeField] private float _paddingRight;
    [SerializeField] private float _paddingTop;
    [SerializeField] private float _paddingBottom;

    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    private Shooter _shooter;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitializedBounds();
    }

    private void Update()
    {
        Move();
    }

    public void OnMove(InputValue inputValue)
    {
        _rawInput = inputValue.Get<Vector2>();
    }

    public void OnFire(InputValue inputValue)
    {
        if(_shooter != null)
        {
            _shooter.isFiring = inputValue.isPressed;
        }
    }

    private void InitializedBounds()
    {
        Camera mainCamera = Camera.main;
        _minBounds = mainCamera.ViewportToWorldPoint(Vector2.zero);
        _maxBounds = mainCamera.ViewportToWorldPoint(Vector2.one);
    }

    private void Move()
    {
        Vector2 delta = _rawInput * _moveSpeed * Time.deltaTime;
        Vector2 newPosirion = new Vector2();

        newPosirion.x = Mathf.Clamp(transform.position.x + delta.x, _minBounds.x + _paddingLeft, _maxBounds.x - _paddingRight);
        newPosirion.y = Mathf.Clamp(transform.position.y + delta.y, _minBounds.y + _paddingBottom, _maxBounds.y - _paddingTop);

        transform.position = newPosirion;
    }
}