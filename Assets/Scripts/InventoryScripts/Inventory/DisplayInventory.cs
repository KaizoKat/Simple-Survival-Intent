using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// public class DisplayInventory : MonoBehaviour
// {
//     public GameObject inventoryPrefab;
//     public InventoryObject inventory;

//     public int X_START;
//     public int Y_START;
//     public int X_SPACE_BETWEEN_ITEM;
//     public int Y_SPACE_BETWEEN_ITEM;
//     public int NUMBER_OF_COLUMS;

//     Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

//     void Start()
//     {
//         CreateDisplay();
//     }

//     void Update()
//     {
//         UpdateDisplay();
//     }

//     public void UpdateDisplay()
//     {
//         for (int i = 0; i < inventory.Container.Count; i++)
//         {
//             if(itemsDisplayed.ContainsKey(inventory.Container[i]))
//             {
//                 itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].ammount.ToString("") ;
//             }
//             else
//             {
//                 var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
//                 obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = 
//                 obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
//                 obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].ammount.ToString("");
//                 itemsDisplayed.Add(inventory.Container[i], obj);
//             }
//         }
//     }

//     public void CreateDisplay()
//     {
//         for (int i = 0; i < inventory.Container.Count; i++)
//         {

//             itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
//             var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
//             obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
//             obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].ammount.ToString("");
//             itemsDisplayed.Add(inventory.Container[i], obj);
//         }
//     }

//     public Vector3 GetPosition(int i)
//     {
//         return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMS)),Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMS)) , 0f);
//     }
// }