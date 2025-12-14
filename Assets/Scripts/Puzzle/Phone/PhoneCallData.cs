using UnityEngine;

[CreateAssetMenu(fileName = "PhoneCall", menuName = "Scriptable Objects/PhoneCallData")]
public class PhoneCallData : ScriptableObject
{
    public AudioClip    audio;
    public bool         isTrue;
}
