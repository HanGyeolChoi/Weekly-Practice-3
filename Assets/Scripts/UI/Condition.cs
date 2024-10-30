using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = curValue / maxValue;
    }

    public void Add(float amount)
    {
        curValue = Mathf.Clamp(curValue + amount, 0, maxValue);
    }
}