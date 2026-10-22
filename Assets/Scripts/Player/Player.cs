using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TargetDetector))]
[RequireComponent(typeof(PickupHandler))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animations _playerAnimation;
    [SerializeField] private AnimationEvents _animationEvents;

    private InputReader _inputReader;
    private Jumper _jumper;
    private Mover _mover;
    private PlayerAttack _playerAttack;
    private Health _health;
    private PlayerDeath _playerDeath;
    private TargetDetector _raycaster;
    private PickupHandler _pickupHandler;

    private float _speed;

    public float FacingfDeretion => _mover.GetFacingDerection();

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _playerAttack = GetComponent<PlayerAttack>();
        _health = GetComponent<Health>();
        _playerDeath = GetComponent<PlayerDeath>();
        _raycaster = GetComponent<TargetDetector>();
        _pickupHandler = GetComponent<PickupHandler>();
    }

    private void OnEnable()
    {
        _inputReader.MoveInputChanged += OnMove;
        _inputReader.ClickedJump += OnJump;
        _inputReader.ClicedAttack += OnAttackAnimation;

        _health.Damaged += OnDamaged;
        _animationEvents.Attacing += OnAttack;
    }

    private void OnDisable()
    {
        _inputReader.MoveInputChanged -= OnMove;
        _inputReader.ClickedJump -= OnJump;
        _inputReader.ClicedAttack -= OnAttackAnimation;
        _health.Damaged -= OnDamaged;
        _animationEvents.Attacing -= OnAttack;
    }

    private void FixedUpdate()
    {
        if (_jumper.IsTouchedGround == false)
        {
            _playerAnimation.SetJumping(true);
        }
        else
        {
            _playerAnimation.SetSpeed(_speed);
            _playerAnimation.SetJumping(false);
        }
    }

    private void OnAttackAnimation()
    {
        _playerAnimation.TriggerKick();
    }

    private void OnAttack()
    {
        _playerAttack.TryAttack(_raycaster, FacingfDeretion);
    }

    private void OnMove(Vector2 input)
    {
        _speed = _mover.MovePlayer(input);
    }

    private void OnJump()
    {
        _jumper.Jump();
    }

    private void OnDamaged()
    {
        _playerAnimation.TriggerHit();

        Debug.Log($"Здоровья осталось: {_health.CurrentHealth}");
    }
}