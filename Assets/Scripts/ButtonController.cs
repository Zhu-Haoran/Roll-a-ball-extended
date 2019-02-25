using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    public void OnClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
