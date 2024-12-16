using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour {
    GameObject bat1, bat2, bat3;

    // Start is called before the first frame update
    void Start() {
        // "3 different sprites instead of just 1."
        bat1 = GameObject.Find("Bat1");
        bat2 = GameObject.Find("Bat2");
        bat3 = GameObject.Find("Bat3");
    }

    // Update is called once per frame
    void Update() {

    }

    public void ScaleDown() {
        /* "Sizes redirect to 150 pixels; only works once.
            Could not work out Vector2 despite 2D." */
        bat1.gameObject.transform.localScale = new Vector3(150, 150, 0.0f);
        bat2.gameObject.transform.localScale = new Vector3(150, 150, 0.0f);
        bat3.gameObject.transform.localScale = new Vector3(150, 150, 0.0f);
    }
}