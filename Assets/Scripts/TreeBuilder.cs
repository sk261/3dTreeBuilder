using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBuilder : MonoBehaviour
{
    public string filePath;
    public GameObject branch;
    public GameObject leaf;

    private loadTree tree;

    // Start is called before the first frame update
    void Start()
    {
        tree = new loadTree(filePath);
        GameObject parent = drawBranch(tree.trunk);
        drawBranch(tree.trunk.branches[0], parent);
    }

    private GameObject drawBranch(loadTree.branch br, GameObject parent = null)
    {
        GameObject obj;
        if (br.isFile) obj = leaf;
        else obj = branch;

        Vector3 parentLoc = new Vector3(0f,0f,0f);
        Quaternion parentRotation = new Quaternion(1f,1f,1f,1f);
        if (parent != null)
        {
            float tipOffset = parent.transform.localScale.y * 6;
            parentLoc = parent.transform.position + obj.transform.up * tipOffset;
            parentRotation = new Quaternion();
        }
        obj = Instantiate(obj, parentLoc, parentRotation);
        
        obj.SetActive(true);

        return obj;
    }

}
