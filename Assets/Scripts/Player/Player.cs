using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(CollectibleDetector))]
[RequireComponent(typeof(CoinWallet))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TargetDetector))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animations _playerAnimation;
    [SerializeField] private AnimationEvents _animationEvents;

    private Rigidbody2D _rigidbody;
    private InputReader _inputReader;
    private CollectibleDetector _collectibleDetector;
    private CoinWallet _coinWallet;
    private Jumper _jumper;
    private Mover _mover;
    private PlayerAttack _playerAttack;
    private Health _health;
    private PlayerDeath _playerDeath;
    private TargetDetector _raycaster;

    private float _speed;

    public float FacingfDeretion => _mover.GetFacingDerection();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 1f;
        _rigidbody.freezeRotation = true;
        _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;

        _inputReader = GetComponent<InputReader>();
        _collectibleDetector = GetComponent<CollectibleDetector>();
        _coinWallet = GetComponent<CoinWallet>();
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
        _playerAttack = GetComponent<PlayerAttack>();
        _health = GetComponent<Health>();
        _playerDeath = GetComponent<PlayerDeath>();
        _raycaster = GetComponent<TargetDetector>();
    }

    private void OnEnable()
    {
        _inputReader.MoveInputChanged += OnMove;
        _inputReader.ClickedJump += OnJump;
        _inputReader.ClicedAttack += OnAttackAnimation;
        _collectibleDetector.CoinDetected += OnCollectibleDetected;
        _health.Damaged += OnDamaged;
        _animationEvents.Attacing += OnAttack;
    }

    private void OnDisable()
    {
        _inputReader.MoveInputChanged -= OnMove;
        _inputReader.ClickedJump -= OnJump;
        _inputReader.ClicedAttack -= OnAttackAnimation;
        _collectibleDetector.CoinDetected -= OnCollectibleDetected;
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
        _speed = _mover.Move(input, _rigidbody);
    }

    private void OnJump()
    {
        _jumper.Jump();
    }

    private void OnCollectibleDetected(ICollectible collectible)
    {
        switch (collectible)
        {
            case Coin coin:
                OnCoinDetected(coin);
                break;

            case MedKit medKit:
                OnMedKitDetected(medKit);
                break;
        }
    }

    private void OnMedKitDetected(MedKit medKit)
    {
        bool ishealed = _health.Heal(medKit.HealthUnits);

        Debug.Log($"Çהמנמגüÿ: {_health.CurrentHealth}");

        if (ishealed)
            medKit.Collect();
    }

    private void OnCoinDetected(Coin coin)
    {
        _coinWallet.AddCoin(coin);

        coin.Collect();
    }

    private void OnDamaged()
    {
        _playerAnimation.TriggerHit();
        Debug.Log($"Çהמנמגüÿ מסעאכמסü: {_health.CurrentHealth}");
    }
}