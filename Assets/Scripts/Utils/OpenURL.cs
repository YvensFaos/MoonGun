using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenURLCall(string URL)
    {
        Application.OpenURL(URL);
    }
}
