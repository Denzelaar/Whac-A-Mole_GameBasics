using UnityEngine;
using WhacAMole.Interfaces;

public class StartScreen : MonoBehaviour, IMenuScreen
{
    public bool IsActive => content.activeSelf;

    [SerializeField] private GameObject content;

    public void Hide()
    {
        content.SetActive(false);
    }

    public void Show()
    {
        content.SetActive(true);
    }
}
