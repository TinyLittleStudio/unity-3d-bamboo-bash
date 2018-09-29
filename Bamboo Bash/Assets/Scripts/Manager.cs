using UnityEngine;
using TMPro;
using System.Collections;

public class Manager : MonoBehaviour
{
    private static Manager defaultInstance;

    [Header("Profiles")]
    [SerializeField] private Profile[] data;

    [Header("Profiles UI")]
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Transform preview;

    [Header("Screens")]
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject endScreen;

    private GameObject previewGameObject;

    private int index = 0;

    private void Awake()
    {
        if (defaultInstance == null)
        {
            defaultInstance = this;
        }
        else
        {
            Destroy(this);
        }
        StartScreen.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(Late());
    }

    private void Update()
    {

        if (previewGameObject != null)
        {
            previewGameObject.transform.Rotate(new Vector3(0, 1, 0), 0.5f);
        }
    }

    private IEnumerator Late()
    {
        yield return new WaitForEndOfFrame();

        OnProfileChange();
    }

    private void OnProfileChange()
    {
        CurrentProfile = data[index];

        if (label != null)
        {
            label.text = CurrentProfile.Name;
        }

        if (preview != null)
        {
            Clear();

            previewGameObject = Instantiate(CurrentProfile.Prefab, preview);

            if (previewGameObject != null)
            {
                previewGameObject.transform.SetParent(preview);
                previewGameObject.transform.localScale = new Vector3(1, 1, 1);
                previewGameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5.0f;
            }
        }
    }

    public void Clear()
    {
        if (previewGameObject != null)
        {
            Destroy(previewGameObject);
        }
    }

    public void Next()
    {
        index++;

        if (index > data.Length - 1)
        {
            index = 0;
        }
        OnProfileChange();
    }

    public void Prev()
    {
        index--;

        if (index < 0)
        {
            index = data.Length - 1;
        }
        OnProfileChange();
    }

    public void Change(int index)
    {
        if (index > -1 && index < data.Length - 1)
        {
            this.index = index;
        }
        OnProfileChange();
    }

    public Profile CurrentProfile { get; private set; }

    public string Username { get; set; }

    public GameObject StartScreen => startScreen;

    public GameObject EndScreen => endScreen;

    public static Manager DefaultInstance => Manager.defaultInstance;
}
