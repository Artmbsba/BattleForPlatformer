using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TargetDetector))]
[RequireComponent(typeof(PickupHandler))]
[RequireComponent(typeof(Vampirism))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animations _animation;
    [SerializeField] private AnimationEvents _animationEvents;

    private InputReader _inputReader;
    private Jumper _jumper;
    private Mover _mover;
    private PlayerAttack _attack;
    private Health _health;
    private PlayerDeath _death;
    private TargetDetector _raycaster;
    private PickupHandler _pickupHandler;
    private Vampirism _vampirism;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _attack = GetComponent<PlayerAttack>();
        _health = GetComponent<Health>();
        _death = GetComponent<PlayerDeath>();
        _raycaster = GetComponent<TargetDetector>();
        _pickupHandler = GetComponent<PickupHandler>();
        _vampirism = GetComponent<Vampirism>();
    }

    private void OnEnable()
    {
        _inputReader.ClicingVampirism += OnVampirism;
        _inputReader.MoveInputChanged += OnMove;
        _inputReader.ClickedJump += OnJump;
        _inputReader.ClicedAttack += OnAttackAnimation;

        _health.DamageReceived += OnDamaged;
        _animationEvents.Attacing += OnAttack;
        _jumper.Jumped += OnJumped;
        _jumper.Landed += OnLanded;
    }

    private void OnDisable()
    {
        _inputReader.ClicingVampirism -= OnVampirism;
        _inputReader.MoveInputChanged -= OnMove;
        _inputReader.ClickedJump -= OnJump;
        _inputReader.ClicedAttack -= OnAttackAnimation;

        _health.DamageReceived -= OnDamaged;
        _animationEvents.Attacing -= OnAttack;
        _jumper.Jumped -= OnJumped;
        _jumper.Landed -= OnLanded;
    }

    private void OnAttackAnimation()
    {
        _animation.TriggerKick();
    }

    private void OnAttack()
    {
        _attack.TryAttack(_raycaster);
    }

    private void OnMove(Vector2 input)
    {
        _animation.SetSpeed(_mover.Move(input));
    }

    private void OnJump()
    {
        _jumper.Jump();
    }

    private void OnJumped()
    {
        _animation.SetJumping(true);
    }

    private void OnLanded()
    {
        _animation.SetJumping(false);
    }

    private void OnVampirism()
    {
        _vampirism.TryVampirism(_health);
    }

    private void OnDamaged()
    {
        _animation.TriggerHit();
    }
}