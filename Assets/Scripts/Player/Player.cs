using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    private void Start()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
    }
}
