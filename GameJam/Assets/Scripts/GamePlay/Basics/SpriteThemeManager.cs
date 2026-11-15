using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteThemeManager : MonoBehaviour
{
    public static SpriteThemeManager Instance;

    private bool isAlternateTheme = false;


    public delegate void ThemeChangeHandler(bool altTheme);
    public static event ThemeChangeHandler OnThemeChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Keyboard.current != null && 
            Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleGlobalTheme();
        }
    }


    public void ToggleGlobalTheme()
    {
        isAlternateTheme = !isAlternateTheme;


        if (OnThemeChanged != null)
        {
            OnThemeChanged(isAlternateTheme);
        }
    }

    public bool IsAlternate()
    {
        return isAlternateTheme;
    }
}