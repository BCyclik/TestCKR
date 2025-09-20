using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class ViewDog : MonoBehaviour
{
    private Button button => GetComponent<Button>();
    [SerializeField] private TMP_Text txt_Index;
    [SerializeField] private TMP_Text txt_Name;
    [SerializeField] private GameObject Loader;
    public string Id { get; set; } = string.Empty;
    public event Action<ViewDog> OnClick;
    public void Init(Breed breed)
    {
        int index = transform.GetSiblingIndex();
        txt_Index.SetText(index.ToString());

        txt_Name.SetText(breed.Attributes.Name);
        Loader.SetActive(false);
        Id = breed.Id;
    }
    private void Awake()
    {
        button.onClick.AddListener(Click);
    }
    private void Click()
    {
        OnClick?.Invoke(this);
    }
    public void Loading(bool value)
    {
        Loader.SetActive(value);
    }
}