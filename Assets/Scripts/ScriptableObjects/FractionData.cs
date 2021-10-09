using UnityEngine;

[CreateAssetMenu(fileName = "New FractionData", menuName = "ScriptableObjects/FractionData", order = 51)]
public class FractionData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Color _color;

    public string Name
    {
        get => _name;
    }

    public Color Color
    {
        get => _color;
    }
}
