using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] Camera pl_cam;
    // Floors
    public Transform[] Building = null;
    public Transform[] Prefab = null;

    LayerMask layerMask;
    Ray ray;
    RaycastHit hit2;
    RaycastHit hit;
    int initializer;
    public bool IsBuilding = false;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 9;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Destroyer();
        }
        if(Input.mouseScrollDelta.y == 1.0f)
        {
            Building[initializer].position = new Vector3(0.0f,-9999.0f,0.0f);
            initializer--;
            Caller();
        }

        if(Input.mouseScrollDelta.y == -1.0f)
        {
            Building[initializer].position = new Vector3(0.0f,-9999.0f,0.0f);
            initializer++;
            Caller();
        }

        if(initializer > Building.Length -1)
        {
            initializer = 0;
        }

        if(initializer < 0)
        {
            initializer = Building.Length -1;
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(IsBuilding == true)
            {
                IsBuilding = false;
                Building[initializer].position = new Vector3(0.0f,-9999.0f,0.0f);
                return;
            }
            if(IsBuilding == false)
            {
                IsBuilding = true;
                return;
            }
        }
        Caller();
        Mathf.RoundToInt(initializer);
    }

    void Caller()
    {
        if(IsBuilding == true)
        {
            Instantiotor();
        }
    }

    void Instantiotor()
    {
        if(Physics.Raycast(pl_cam.transform.position,pl_cam.transform.forward,out hit, 4.0f,layerMask))
        {
            Building[initializer].position = hit.point;
            Building[initializer].position -= Vector3.one * 1;
            Building[initializer].position /= Building[initializer].localScale.y / 2;
            Building[initializer].position = new Vector3(Mathf.Round(Building[initializer].position.x),Mathf.Round(Building[initializer].position.y),Mathf.Round(Building[initializer].position.z));
            Building[initializer].position *= Building[initializer].localScale.y / 2;
            Building[initializer].position +=Vector3.one * 1;

            if(initializer != 20 || initializer != 21 || initializer != 22 || initializer != 23)
            {
                Building[initializer].eulerAngles = new Vector3(Building[initializer].eulerAngles.x,Mathf.RoundToInt(transform.eulerAngles.y) != 0 ? Mathf.RoundToInt(transform.eulerAngles.y /90) * 90 : 0,0);
            }

            if (initializer == 20 || initializer == 21 || initializer == 22 || initializer == 23)
            {
               Building[initializer].eulerAngles = new Vector3(Building[initializer].eulerAngles.x,(Mathf.RoundToInt(transform.eulerAngles.y) != 0 ? Mathf.RoundToInt(transform.eulerAngles.y /90) * 90 : 0) + 180,0);
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(Prefab[initializer],Building[initializer].position,Building[initializer].rotation);
        }
    }


    void Destroyer()
    {
        ray = pl_cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit2))
        {
            if(hit2.transform.name  != "Terrain")
            {
                Destroy(hit2.transform.gameObject);                
            }
            else
            {
                // DON'T
            }
        }
    }
}
