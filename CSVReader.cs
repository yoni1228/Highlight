﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<List<int>> Read(string file)
    {
        var list = new List<List<int>>();  //string으로 구성된 dic
        TextAsset data = Resources.Load(file) as TextAsset;

        string[] lines = Regex.Split(data.text, LINE_SPLIT_RE);  //텍스트를 line by line으로 나눔

        if (lines.Length <= 1)
            return list;

//        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 0; i < lines.Length; i++)
        {
            List<int> listLine = new List<int>();
            list.Add(listLine);

            string[] values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            for (var j = 0; j < values.Length; j++)
            {
                int v = System.Convert.ToInt32(values[j]);

                listLine.Add(v);
                

            }

                //var entry = new Dictionary<string, object>();
                //for (var j = 0; j < header.Length && j < values.Length; j++)
                //{
                //    string value = values[j];
                //    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                //    object finalvalue = value;
                //    int n;
                //    float f;
                //    if (int.TryParse(value, out n))
                //    {
                //        finalvalue = n;
                //    }
                //    else if (float.TryParse(value, out f))
                //    {
                //        finalvalue = f;
                //    }
                //    entry[header[j]] = finalvalue;
                //}
               
        }
       
        return list;
    }
}
