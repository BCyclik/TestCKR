using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class ViewDogDetails : MonoBehaviour
{
    [SerializeField] private TMP_Text txt_Name;
    [SerializeField] private TMP_Text txt_Description;
    [SerializeField] private Button btn_Ok;
    private void Awake()
    {
        btn_Ok.onClick.AddListener(Ok);
    }
    private void Ok()
    {
        gameObject.SetActive(false);
    }
    public void Show(Breed breed)
    {
        txt_Name.SetText(breed.Attributes.Name);
        txt_Description.SetText(breed.Attributes.Description);

        gameObject.SetActive(true);
    }
}
