using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _attackKey = KeyCode.E;

    public event Action<Vector2> MoveInputChanged;
    public event Action ClickedJump;
    public event Action ClicedAttack;

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis(Horizontal), 0f);

        MoveInputChanged?.Invoke(input);

        if (Input.GetKeyDown(_jumpKey))
            ClickedJump?.Invoke();

        if(Input.GetKeyDown(_attackKey))
            ClicedAttack?.Invoke();
    }
}