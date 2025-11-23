using UnityEngine;

public class VisualNoiseAnimation : MonoBehaviour
{
    public float changeInterval = 0.05f;
    private Material material;
    private float timer;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            Vector2 randomOffset = new Vector2(Random.value, Random.value);
            material.SetVector("_Offset", randomOffset);

            timer = 0f;
        }
    }
}
