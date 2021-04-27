/// <summary>
/// Script para a criação do inventario
/// Adiciona o slots de inventario 
/// Atualiza a adição de novos itens
/// </summary>
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject SlotPrefab;               // objeto que recebe o prefab Slot
    public const int numSlots = 5;              // Numero fixo de Slots;
    Image[] itemImages = new Image[numSlots];   // array de imagens
    Item[] items = new Item[numSlots];          // array de itens
    GameObject[] slots = new GameObject[numSlots];
    // Start is called before the first frame update
    void Start()
    {
        CreateSlot();   // Cria os slots do inventário
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* Função que cria os slots de inventario
     * Se o prefab dos slots não for nulo
     * Cria numero de slots igual ao numero de slots setado

     */
    public void CreateSlot()
    {
        if (SlotPrefab != null)
        {
            for( int i = 0; i < numSlots; i++)
            {
                GameObject novoSlot = Instantiate(SlotPrefab);  // Instancia o novo slot
                novoSlot.name = "ItemSlot_" + i;
                novoSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);   // Transforma o slot como filho do inventario
                slots[i] = novoSlot;        // Adiciona na lista de slots
                itemImages[i] = novoSlot.transform.GetChild(1).GetComponent<Image>();   // Pega o item image que é filho de slot e armazena
            }
        }
    }

    /* Função que adiciona o item no inventário
    * Verifica se o item ainda não foi adicionado, caso tenha sido, aumenta a quantidade
    * Se não for, adiciona a imagem no item no primeiro slot vazio e seta a quantidade para 1 
    */
    public bool AddItem(Item itemToAdd)
    {
        for(int i=0; i <items.Length; i++)  
        {
            if (items[i]!=null && items[i].itemType == itemToAdd.itemType && itemToAdd.Stackable == true) // Item já está no inventário
            {
                items[i].Quantity = items[i].Quantity + 1;
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>(); // Armazena o Scriptable Object Slot
                Text TextQtd = slotScript.TextQtd;                          // Variavel que contem o objeto do tipo texto que armazena a quantidade de itens
                TextQtd.enabled = true;                                 // Ativa o texto
                TextQtd.text = items[i].Quantity.ToString();            // Atualiza com a quantidade de itens
                return true;                                            // Retorna que foi possivel adicionar o item
            }
            if(items[i] == null)    // Novo item no inventário
            {
                items[i] = Instantiate(itemToAdd);
                items[i].Quantity = 1;
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text TextQtd = slotScript.TextQtd;
                itemImages[i].sprite = itemToAdd.Sprite;
                itemImages[i].enabled = true;
                TextQtd.enabled = true;
                TextQtd.text = items[i].Quantity.ToString();
                return true;
            }
        }
        return false;
    }
}
