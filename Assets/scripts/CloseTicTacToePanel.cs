using UnityEngine;

public class CloseTicTacToePanel : MonoBehaviour
{
    public void ClosePanel()
    {
        // Detach from parent if necessary
        transform.SetParent(null);
        // Disable this gameobject (the panel itself)
        gameObject.SetActive(false);
    }
}
