using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class loadTree
{
    public branch trunk;
    
    public class branch
    {
        public double size, size_ratio, name_ratio, mod_ratio;
        public string name, fullpath;
        public List<branch> branches;
        public int level;
        public bool isFile;
        public branch(string val)
        {
            branches = new List<branch>();

            string[] vals = val.Split(',');
            level = -1;
            try
            {
                name = vals[0].TrimStart('\t');
                fullpath = vals[5];
                isFile = (Convert.ToInt16(vals[6]) == 1);
                size = Convert.ToDouble(vals[1]);
                size_ratio = Convert.ToDouble(vals[2]);
                name_ratio = Convert.ToDouble(vals[3]);
                mod_ratio = Convert.ToDouble(vals[4]);
                level = vals[0].Length - vals[0].Replace("\t","").Length;
            } catch {
                
            }
        }

        public int length 
        {
            get {
                int total = 1;
                foreach(branch b in branches)
                    total += b.length;
                return total;
            }
        }

    }
    public loadTree(string filename)
    {
        List<String> ligaments;
        ligaments = File.ReadAllText(filename).Split('\n').ToList();
        trunk = makeBranch(ref ligaments);
        Debug.Log(trunk.name);
        Debug.Log(trunk.length);
        Debug.Log(trunk);
    }

    public branch makeBranch(ref List<String> section)
    {
        branch ret = new branch(section[0]);
        section.RemoveAt(0);
        if (section.Count > 0)
        {
            branch next = new branch(section[0]);
            while (next.level > ret.level)
            {
                branch br = makeBranch(ref section);
                ret.branches.Add(br);
                if (section.Count > 0)
                {
                    next = new branch(section[0]);
                }
            }
        }
        return ret;
    }
}
