using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public void CreateEemy(GameObject enemy)
    {
        GameObject newObject = Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
