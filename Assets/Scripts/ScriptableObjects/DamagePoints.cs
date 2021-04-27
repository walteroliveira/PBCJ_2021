/// <summary>
/// Scriptable object para gerar a quantidade de dano causada no jogo
/// </summary>
using UnityEngine;

[CreateAssetMenu(menuName ="DamagePoints")]
public class DamagePoints : ScriptableObject
{
    public float value;     // armazena quanto vale o objeto script
}
