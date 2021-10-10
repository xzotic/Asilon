using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GlobalManager : ScriptableObject
{
    public int SceneIndex;
    public int InitialHP;
    public int money;
    public Vector3 InitialValue;
    public Vector3 VectorValue;
}
