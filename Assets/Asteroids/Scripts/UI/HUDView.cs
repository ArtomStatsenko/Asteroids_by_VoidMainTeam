using UnityEngine;
using UnityEngine.UI;

public abstract class HUDView : MonoBehaviour
{
    public Slider HealthSlider;
    public Slider ArmorSlider;

    public void SetMaxHealth(int health)
    {
        HealthSlider.maxValue = health;
        HealthSlider.value = health;
    }
    public void SetHealth(int health)
    {
        HealthSlider.value = health;
    }

    public void SetMaxArmor(int armor)
    {
        ArmorSlider.maxValue = armor;
        ArmorSlider.value = armor;
    }

    public void SetArmor(int armor)
    {
        ArmorSlider.value = armor;
    }
}
