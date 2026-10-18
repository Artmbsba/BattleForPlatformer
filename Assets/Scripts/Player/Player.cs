using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(CoinWallet))]
[RequireComponent(typeof(CoinDetector))]
[RequireComponent(typeof(MedKitDetector))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TargetDetector))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private InputReader _inputReader;
    private Animation _playerAnimation;
    private CoinWallet _coinWallet;
    private CoinDetector _coinDetector;
    private MedKitDetector _medKitDetector;
    private GroundDetector _groundDetector;
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
        _playerAnimation = GetComponent<Animation>();
        _coinWallet = GetComponent<CoinWallet>();
        _coinDetector = GetComponent<CoinDetector>();
        _medKitDetector = GetComponent<MedKitDetector>();
        _groundDetector = GetComponent<GroundDetector>();
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
        _inputReader.ClicedAttack += OnAttack;
        _coinDetector.CoinDetected += OnCoinDetected;
        _medKitDetector.MedKitDetected += OnMedKitDetected;
        _health.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _inputReader.MoveInputChanged -= OnMove;
        _inputReader.ClickedJump -= OnJump;
        _inputReader.ClicedAttack -= OnAttack;
        _coinDetector.CoinDetected -= OnCoinDetected;
        _medKitDetector.MedKitDetected -= OnMedKitDetected;
        _health.Damaged -= OnDamaged;
    }

    private void FixedUpdate()
    {
        if (_groundDetector.IsGround(_rigidbody) == false)
        {
            _playerAnimation.SetJumping(true);
        }
        else
        {
            _playerAnimation.SetSpeed(_speed);
            _playerAnimation.SetJumping(false);
        }
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
        if (_groundDetector.IsGround(_rigidbody))
            _jumper.ResetJumps();

        _jumper.Jump(_rigidbody);
    }

    private void OnMedKitDetected(MedKit medKit)
    {
        _health.Heal(medKit);

        Debug.Log($"Çהמנמגüÿ: {_health.CurrentHealth}");
    }

    private void OnCoinDetected(Coin coin)
    {
        _coinWallet.AddCoin(coin);
    }

    private void OnDamaged()
    {
        _playerAnimation.TriggerHit();
        Debug.Log($"Çהמנמגüÿ מסעאכמסü: {_health.CurrentHealth}");
    }
}