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
        GameObject parent = drawBranch(tree.trunk, new Vector3(0,0,0));
    }

    private GameObject drawBranch(loadTree.branch br, Vector3 origin, float parent_zaxisR = 0, float parent_yaxisR = 0)
    {
        GameObject obj;
        if (br.isFile) obj = leaf;
        else obj = branch;

        /* Size = object length and width
         * Size ratio = Z-axis Rotation
         * Name ratio = Y-axis Rotation
         * Mod ratio = object colour intensity
         * 
         */
        float rY = (float)((br.name_ratio * 360 / 180 * Mathf.PI) + parent_yaxisR) % (Mathf.PI / 2);
        float rZ = (float)((br.size_ratio * 360 / 180 * Mathf.PI) + parent_zaxisR) % (Mathf.PI / 2);
        float x, y, z, length;
        // length is determined by file size in Megabytes
        length = (float)(br.size / Mathf.Pow(10, 6));
        z = Mathf.Sin(rY) * length + origin.z;
        y = Mathf.Sin(rZ) * length + origin.y;
        x = Mathf.Cos(rZ)*Mathf.Cos(rY) * length + origin.x;


        Vector3 end = new Vector3(x, y, z);
        // Create object
        obj = Instantiate(obj);
        obj.name = br.name + "_" + br.level.ToString();
        createBetweenPoints(obj, origin, end, length);

        // DEBUGGING
        /*
        GameObject debugOrigin = GameObject.CreatePrimitive(PrimitiveType.Cube);
        debugOrigin.name = obj.name + "_origin";
        debugOrigin.transform.position = origin;
        GameObject debugEnd = GameObject.CreatePrimitive(PrimitiveType.Cube);
        debugEnd.name = obj.name + "_end";
        debugEnd.transform.position = end;
        */


        obj.SetActive(true);
        // if (br.level >= 0)
        foreach (loadTree.branch n in br.branches)
                drawBranch(n, end, rZ, rY);

        return obj;
    }

    private void createBetweenPoints(GameObject obj, Vector3 p1, Vector3 p2, float length)
    {
        // Copied from Unity Forums
        Vector3 pos = Vector3.Lerp(p1, p2, (float).5);
        float dist = Vector3.Distance(p1, p2);
        Vector3 newScale = new Vector3(dist/20, dist/2, dist/20);
        obj.transform.localScale = newScale;
        obj.transform.position = pos;
        obj.transform.up = p2 - p1;
    }

}
