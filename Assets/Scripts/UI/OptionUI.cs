using UnityEngine;

public class OptionUI : MonoBehaviour
{
    public GameObject optionWindow;
    private PlayerController controller;
    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        controller.option += Toggle;
        optionWindow.SetActive(false);
    }

    private void Toggle()
    {
        if(IsOpen()) optionWindow.SetActive(false);
        else optionWindow.SetActive(true);
    }

    private bool IsOpen()
    {
        if (optionWindow.activeInHierarchy)
        {
            return true;
        }
        else return false;
    }
}