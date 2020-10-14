using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scene Data/Level")]
public class Level : GameScene
{
    //Settings specific to level only
    [Header("Level specific")]
    public int entriesCount;

    public List<int> steps;

    //this is really the sum of all step scale values
    public int maxScale;

    public Transform startingPosition;
}
