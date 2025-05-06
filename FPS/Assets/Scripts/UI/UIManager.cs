using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Gameplay UI")]
    public TextMeshProUGUI ammoText;
    public Image gunIcon;
    public Slider healthBar;
    public GameObject crosshair;

    [Header("Weapon Inventory UI")]
    public TextMeshProUGUI weaponInventoryText; 

    [Header("Win/Lose Screens")]
    public GameObject winScreen;
    public GameObject loseScreen;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        WeaponBase.OnAmmoChanged += UpdateAmmoUI;
        WeaponInventory.OnWeaponSwitched += UpdateGunIcon;
        WeaponInventory.OnInventoryUpdated += UpdateWeaponInventoryUI; 
        PlayerHealth.OnPlayerHealthChanged += UpdateHealthBar;
        PlayerHealth.OnPlayerDeath += ShowLoseScreen;
        EnemyManager.OnAllEnemiesDead += ShowWinScreen;
    }

    private void OnDisable()
    {
        WeaponBase.OnAmmoChanged -= UpdateAmmoUI;
        WeaponInventory.OnWeaponSwitched -= UpdateGunIcon;
        WeaponInventory.OnInventoryUpdated -= UpdateWeaponInventoryUI; 
        PlayerHealth.OnPlayerHealthChanged -= UpdateHealthBar;
        PlayerHealth.OnPlayerDeath -= ShowLoseScreen;
        EnemyManager.OnAllEnemiesDead -= ShowWinScreen;
    }

    private void UpdateAmmoUI(int currentAmmo, int totalAmmo)
    {
        ammoText.text = $"{currentAmmo}/{totalAmmo}";
    }

    private void UpdateGunIcon(Sprite icon)
    {
        gunIcon.sprite = icon;
    }

    private void UpdateHealthBar(int currentHealth)
    {
        healthBar.value = currentHealth;
    }

    private void UpdateWeaponInventoryUI(List<WeaponData> weapons) 
    {
        
        weaponInventoryText.text = "";

        
        int weaponIndex = 1;
        foreach (WeaponData weapon in weapons)
        {
            weaponInventoryText.text += $"{weaponIndex} - {weapon.weaponName}\n"; 
            weaponIndex++;
        }
    }

    private void ShowWinScreen()
    {
        GameplayUIActive(false);
        winScreen.SetActive(true);
    }

    private void ShowLoseScreen()
    {
        GameplayUIActive(false);
        loseScreen.SetActive(true);
    }

    private void GameplayUIActive(bool isActive)
    {
        ammoText.gameObject.SetActive(isActive);
        gunIcon.gameObject.SetActive(isActive);
        healthBar.gameObject.SetActive(isActive);
        crosshair.SetActive(isActive);
        weaponInventoryText.gameObject.SetActive(isActive); 
    }
}
