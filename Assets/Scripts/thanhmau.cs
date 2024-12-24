using UnityEngine;
using UnityEngine.UI;

public class thanhmau : MonoBehaviour
{
    public UnityEngine.UI.Slider manaSlider;

    public void capnhatthanhmau(float currentMana, float maxMana)
    {
        manaSlider.value = currentMana / maxMana;
    }

    private void Update()
    {
        capnhatthanhmau(SharedManaSystem.Instance.currentMana, SharedManaSystem.Instance.maxMana);
    }
}
