using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SliderTextValue : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Slider slider;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider = GetComponentInParent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
        SetTextValue(slider.value);
    }

    void OnValueChanged(float value)
    {
        SetTextValue(value);
    }

    void SetTextValue(float value)
    {
        text.text = Mathf.RoundToInt(value).ToString();
    }
}
