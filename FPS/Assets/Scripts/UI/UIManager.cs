using UnityEngine;
using TMPro; // Assuming you use TextMeshPro for UI

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        WeaponBase.OnAmmoChanged += UpdateAmmoUI;
        PlayerHealth.OnPlayerHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        WeaponBase.OnAmmoChanged -= UpdateAmmoUI;
        PlayerHealth.OnPlayerHealthChanged -= UpdateHealthUI;
    }

    private void UpdateAmmoUI(int currentAmmo, int totalAmmo)
    {
        ammoText.text = $"Ammo: {currentAmmo}/{totalAmmo}";
    }

    private void UpdateHealthUI(int currentHealth)
    {
        healthText.text = $"Health: {currentHealth}";
    }
}
