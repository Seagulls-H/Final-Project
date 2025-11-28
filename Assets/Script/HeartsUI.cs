using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    [Header("Prefab heart")]
    public GameObject heartPrefab;     

    private List<Image> hearts = new List<Image>();

    public void SetupHearts(int maxHearts)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        hearts.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, transform);
            Image img = heartObj.GetComponent<Image>();
            if (img != null)
            {
                hearts.Add(img);
            }
        }
    }

    public void UpdateHearts(int currentHearts)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].enabled = (i < currentHearts);
        }
    }
}
