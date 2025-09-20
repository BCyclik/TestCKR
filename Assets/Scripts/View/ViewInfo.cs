using UnityEngine;
using TMPro;

public class ViewInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text txt_Text;

    public void SetInfo(string value)
    {
        txt_Text.SetText(value);
    }
    public void SetInfo(int value)
    {
        txt_Text.SetText(value.ToString());
    }
}