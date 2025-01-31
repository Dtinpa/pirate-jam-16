using System;

using UnityEngine;

public class Waves : MonoBehaviour {
    /// <summary>
    /// The amplitude of the wave (height from zero to crest, or trough to zero
    /// </summary>
    public float waveAmplitude;
    /// <summary>
    /// How fast should the wave animate the sine wave
    /// </summary>
    public float waveHertz;

    public  float elapsedTime = 0f;
    private float StartingHeight;

    public float modifiedHeight;

    // Start is called before the first frame update
    void Start() {
        StartingHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        elapsedTime += Time.deltaTime;

        modifiedHeight     = (float)(Math.Sin(elapsedTime) * waveAmplitude);
        transform.position = new Vector3(transform.position.x, StartingHeight + modifiedHeight, transform.position.z);
    }
}