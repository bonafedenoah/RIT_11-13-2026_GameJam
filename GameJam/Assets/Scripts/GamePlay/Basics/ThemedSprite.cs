using UnityEngine;


public class ThemedSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        SpriteThemeManager.OnThemeChanged += UpdateSprite;

        if (SpriteThemeManager.Instance != null)
        {
            UpdateSprite(SpriteThemeManager.Instance.IsAlternate());
        }
    }

    void OnDisable()
    {
        SpriteThemeManager.OnThemeChanged -= UpdateSprite;
    }

    void UpdateSprite(bool altTheme)
    {
        if (spriteRenderer == null || SpriteThemeManager.Instance == null) return;

        if (CompareTag("Player"))
        {
            spriteRenderer.sprite = altTheme ? SpriteThemeManager.Instance.playerAltSprite : SpriteThemeManager.Instance.playerNormalSprite;
        }
        else if (CompareTag("EnemyBullet") || name.Contains("bullet"))
        {
            spriteRenderer.sprite = altTheme ? SpriteThemeManager.Instance.playerBulletAltSprite : 
                SpriteThemeManager.Instance.playerBulletNormalSprite;
        }
        else // Default to building/enemy
        {
            spriteRenderer.sprite = altTheme ? SpriteThemeManager.Instance.buildingAltSprite : SpriteThemeManager.Instance.buildingNormalSprite;
        }
    }
}