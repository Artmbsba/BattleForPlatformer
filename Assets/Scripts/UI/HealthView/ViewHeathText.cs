using TMPro;
using UnityEngine;

public class ViewHeathText : ViewBar
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    
   protected override void DrawHealth( )
    {
        _textMeshPro.text = $"Çהמנמגüÿ\n{Health.CurrentHealth}/{Health.MaxHealth}";
    }
}
