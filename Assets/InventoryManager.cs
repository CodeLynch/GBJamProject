using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<string> items = new List<string>();

    public Transform ItemGrid;
    public GameObject itemtext;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            instance = this;
        }
        else
        {
            instance = this;
        }
        
    }

    public void ListItems()
    {
        clearGrid();
        foreach(string item in items)
        {
            itemtext.GetComponent<TextMeshProUGUI>().text = item;
            Instantiate(itemtext, ItemGrid);
        }       
    }

    private void clearGrid()
    {
        foreach(Transform item in ItemGrid)
        {
            Destroy(item.gameObject);
        }
    }
}
