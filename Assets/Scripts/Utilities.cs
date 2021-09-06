/// ----------------------------------------------------------------------
/// Contains various helpful utility methods to be used in other scripts
/// 
/// @project Sanctum Adrift
/// @version 1.0
/// @organization Lightsea Studio
/// @author Owen Hellum
/// @date September 2021
/// ----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    /// <param name="givenArray">Array to be selected from</param>
    /// <returns>A random element from any given array</returns>
    public static T getRandomFromArray<T>(T[] givenArray)
    {
        T temp = givenArray[Random.Range(0, givenArray.Length)];
        return temp;
    }
}
