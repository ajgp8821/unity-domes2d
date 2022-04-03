using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3L3 : MonoBehaviour {

    public int myInteger = 5;
    public float myFloat = 1.5f;
    public bool myBoolean = true;
    public string mystring = "Hello World";
    public int[] myArrayOfInts;

    private int _myPrivateInteger = 10;
    // float _myPrivateFloat = -5.0f;

    private void Start() {
        // Operators
        // Math oparators: +, -, *, /, %
        // Last operator is 'module', it returns the rest of a division
        Debug.Log("Let's sum 10 to my integer. Right now its value is " + myInteger);
        myInteger = myInteger + 10;
        Debug.Log("After the sum the value is " + myInteger);

        // Calling a function
        IsEven(myInteger);

        // Calling inside and if
        if (IsEven(myInteger)) {
            MyDebug("myInteger is even!!!");
        }
        else {
            MyDebug("myInteger is odd!!!");
        }

        // Comparisons
        // Operators ==, >, <, >=, <= (equal to, greather than, less than, greather or equal to, less or equal to)
        if (myFloat >= 2f) {
            MyDebug("myFloat is greather or equal than 2");
        }

        // Besides, we can combine comarisons
        if (IsEven(_myPrivateInteger) && _myPrivateInteger == 10) { // Operator AND
            MyDebug("_myPrivateInteger is 10!!!");
        }

        if (IsEven(myInteger) || IsEven(_myPrivateInteger)) { // Operator OR
            MyDebug("Any of the two variables is ok to me and want to execute some code");
        }

        // Flow Control
        for (int i = 0; i < 10; i++) {
            Debug.Log(i); // Print numbers from 0 to 9 (Check i < 10, never 10)
        }

        for (int i = 0; i < myArrayOfInts.Length; i++) {
            Debug.Log(myArrayOfInts[i]);
        }

        // Unity's utilities in programming!

        // Get a component from THIS object
        SpriteRenderer myspriteRenderer = GetComponent<SpriteRenderer>(); // This can be null and the game can crash

        if (myspriteRenderer != null) {
            MyDebug("I can use myspriteRenderer");
        }
        else {
            MyDebug("The game will crash if you try to use the component!");
        }

        // Find object in the scene
        GameObject anObjectWithSpriteRenderer = FindObjectOfType<SpriteRenderer>().gameObject;
        GameObject anObjectWithTag = GameObject.FindWithTag("Player");
        GameObject anObjectWithName = GameObject.Find("Whatever name you now");

        // Enable or disable components
        if (myspriteRenderer != null) {
            myspriteRenderer.enabled = false;
            MyDebug("I can use myspriteRenderer");
        }

        // Enable disable gameobjects
        if (anObjectWithName != null) {
            anObjectWithName.SetActive(false);
        }
    }

    private void MyDebug(string message) {
        Debug.Log(message);
    }

    bool IsEven(int num) {
        if (num % 2 == 0) {
            return true;
        }
        else {
            return false;
        }
    }
}
