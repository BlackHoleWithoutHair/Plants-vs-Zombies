using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ZombieData", order = 2)]
public class ZombieDataScriptableObject : ScriptableObject
{
    public List<ZombieShareAttr> ZombieShareAttrs;
}

