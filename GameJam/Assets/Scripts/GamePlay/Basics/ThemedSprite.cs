using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ThemedSprite : MonoBehaviour
{
    [Header("Sprite Variations")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite altSprite;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        SpriteThemeManager.OnThemeChanged += HandleThemeChanged;

        if (SpriteThemeManager.Instance != null)
        {
            ApplyTheme(SpriteThemeManager.Instance.IsAlternate());
        }
    }

    void OnDisable()
    {
        SpriteThemeManager.OnThemeChanged -= HandleThemeChanged;
    }

    private void HandleThemeChanged(bool isAlt)
    {
        ApplyTheme(isAlt);
    }

    private void ApplyTheme(bool isAlt)
    {
        if (spriteRenderer == null) return;

        if (isAlt && altSprite != null)
        {
            spriteRenderer.sprite = altSprite;
        }
        else if (!isAlt && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
}