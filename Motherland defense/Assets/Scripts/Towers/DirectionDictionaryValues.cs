using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionDictionaryValues 
{
    public static readonly Dictionary<DirectionEnum.Directions, float> DirectionValues = new Dictionary<DirectionEnum.Directions, float>
    {
        {DirectionEnum.Directions.Top, 0 },
        {DirectionEnum.Directions.Left, -90 },
        {DirectionEnum.Directions.Bottom, 180 },
        {DirectionEnum.Directions.Right, 90 },
    };
}
