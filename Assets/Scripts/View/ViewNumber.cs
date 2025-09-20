using UnityEngine;
using TMPro;

public class ViewNumber : MonoBehaviour
{
    public TMP_Text txt => GetComponent<TMP_Text>();
    [SerializeField] private float Speed = 15f;
    [SerializeField] private float Delay = 1.5f;

    private void Awake()
    {
        Destroy(gameObject, Delay);
    }
    private void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
    }
}