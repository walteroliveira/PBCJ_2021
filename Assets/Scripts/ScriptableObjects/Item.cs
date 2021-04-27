/// <summary>
/// Scriptable Object para a criação dos itens
/// </summary>
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]

public class Item : ScriptableObject
{
    public string ObjectName;       // nome do item
    public Sprite Sprite;           // sprite do item  
    public int Quantity;            // quantidade do item
    public bool Stackable;          // verifica se pode ser empilhado no inventario
    public enum ItemType            // Lista de tipos de item
    {
        COIN,
        HEALTH,
        DIAMOND,
        SAPPHIRE
    }

    public ItemType itemType;       // variavel do tipo de item
}
