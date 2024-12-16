using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Methods : MonoBehaviour {
    void Hello() {
        Debug.Log("Hello Gamers!");
    }

    int ApplyDamage(int damageTaken, int health) {
        return health - damageTaken;
    }

    // Start is called before the first frame update
    void Start() {
        Hello();

        int x = ApplyDamage(5, 100);
        Debug.Log("Player health is: " + x);
    }

    // Update is called once per frame
    void Update() {

    }
}