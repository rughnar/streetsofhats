using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public float velocity = 1f;
    public AudioClip moveAudioClip;
    private InputAction _move;
    private AudioManager _audioManager;
    private Rigidbody2D _rb;
    private PlayerInputActions _playerActions;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _moveInput;
    private Animator _animator;

    private void Awake()
    {
        _playerActions = new PlayerInputActions();
        _audioManager = FindObjectOfType<AudioManager>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        _move = _playerActions.Player.Move;
        _move.Enable();
        //_moverse.performed += Moverse;


    }

    private void OnDisable()
    {
        _move.Disable();
    }

    void FixedUpdate()
    {
        _moveInput = _playerActions.Player.Move.ReadValue<Vector2>();

        if (_moveInput != Vector2.zero)
        {
            _rb.MovePosition(new Vector2(_rb.position.x + _moveInput.x * 0.01f * velocity, _rb.position.y + _moveInput.y * 0.01f * velocity));
            _animator.SetBool("isWalking", true);
            _spriteRenderer.flipX = _moveInput.x > 0;
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

    }
}
