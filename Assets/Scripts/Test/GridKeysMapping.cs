using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GridOfFieldsMap",menuName = "Create Grid of Fields map")]
public class GridKeysMapping : ScriptableObject
{
    public RowOfFields[] GridOfFields;
}
