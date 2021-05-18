using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    NavMeshAgent MyNMA;
    NavMeshPath path;
    Material MyMaterial;
    SkinnedMeshRenderer MySMR;
    // Start is called before the first frame update
    void Start()
    {
        //Get all necessary components and create copy of material
        MyNMA = GetComponent< NavMeshAgent > ();
        MyNMA.SetDestination(new Vector3(0, 0, 0));
        MySMR = transform.GetChild(transform.childCount - 1).gameObject.GetComponent<SkinnedMeshRenderer>();
        MyMaterial = new Material(MySMR.material);
        MySMR.material = MyMaterial;
    }

    public void Kill()
    {
        //Start death animation (in shader) and destroy enemy
        MyNMA.isStopped = true;
        MyMaterial.SetFloat("_TriggerTime", Time.time);
        MyMaterial.SetInt("_Kill", 1);
        Destroy(gameObject, MyMaterial.GetFloat("_Speed")+0.25f);
        Destroy(this);
    }
}
