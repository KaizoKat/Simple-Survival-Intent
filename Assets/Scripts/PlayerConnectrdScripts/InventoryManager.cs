using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // private bool s1 = false;
    // private bool s2 = false;
    // private bool s3 = false;
    // private bool s4 = false;
    // private bool s5 = false;
    // private bool s6 = false;
    // private bool s7 = false;
    // private bool s8 = false;

    public InventoryObject inventory;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if(item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        // inventory.Container.Clear();
    }

    private void Update()
    {
        // InputCheatIndexer();
        if(Input.GetKeyDown(KeyCode.F9))
        {
            inventory.Save();
        }

        if(Input.GetKeyDown(KeyCode.F10))
        {
            inventory.Load();
        }
    }

    // private void InputCheatIndexer()
    // {
    //     if(Input.GetKeyDown(KeyCode.F5))
    //         s1 = true;

    //     if(s1 == true)
    //         if(Input.GetKeyDown(KeyCode.UpArrow))
    //             s2 = true;
        
    //     if(s2 == true)
    //         if(Input.GetKeyDown(KeyCode.DownArrow))
    //             s3 = true;

    //     if(s3 == true)
    //         if(Input.GetKeyDown(KeyCode.UpArrow))
    //             s4 = true;

    //     if(s4 == true)
    //         if(Input.GetKeyDown(KeyCode.DownArrow))
    //             s5 = true;

    //     if(s5 == true)
    //         if(Input.GetKeyDown(KeyCode.LeftArrow))
    //             s6 = true;
        
    //     if(s6 == true)
    //         if(Input.GetKeyDown(KeyCode.RightArrow))
    //             s7 = true;

    //     if(s7 == true)
    //         if(Input.GetKeyDown(KeyCode.B))
    //             s8 = true;

    //     if(s8 == true)
    //         if(Input.GetKeyDown(KeyCode.A))
    //             inventory.InputCodex = true;
    //     if((s1 == true && s2 == true && s3 == true && s4 == true && s5 == true && s6 == true && s7 == true && s8 == true && Input.GetKeyDown(KeyCode.F5)) | inventory.IsOnline == true)
    //     {
    //         s1= false;
    //         s2= false;
    //         s3= false;
    //         s4= false;
    //         s5= false;
    //         s6= false;
    //         s7= false;
    //         s8= false;
    //         inventory.InputCodex = false;
    //     }
    //}
}
