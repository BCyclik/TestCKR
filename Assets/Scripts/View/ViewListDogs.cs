using System.ComponentModel;
using UnityEngine;
using Zenject;
using System;
using UnityEngine.UI;

public class ViewListDogs : MonoBehaviour
{
    //[Inject] private DiContainer diContainer;

    [SerializeField] public GameObject Loader;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private ViewDog prefabViewDog;
    [Space]
    [SerializeField] private ViewDogDetails viewDogDetails;

    private void OnEnable()
    {
        Loader.SetActive(true);
    }
    public ViewDog AddDog(Breed breed)
    {
        //var myPrefab = diContainer.InstantiatePrefabForComponent<ViewDog>(viewDogPrefab);
        //myPrefab.transform.SetParent(scrollRect.content);

        var myPrefab = Instantiate(prefabViewDog, scrollRect.content);
        myPrefab.Init(breed);
        return myPrefab;
    }

    public void ShowPopup(Breed breed)
    {
        viewDogDetails.Show(breed);
    }

    public void AllLoaders() // костыль
    {
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            var item = scrollRect.content.GetChild(i).GetComponent<ViewDog>();
            item.Loading(false);
        }
    }
}