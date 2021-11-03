using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCompareByName : IComparer
{
    public int Compare(object x, object y)
    {
        return (new CaseInsensitiveComparer()).Compare(((Item)x).itemName, ((Item)y).itemName);
    }
}
