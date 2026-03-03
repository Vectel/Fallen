using UnityEngine;

public class chest : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set;}
    public string ChestID { get; private set;}
    public GameObject itemPrefab;
    public sprite opendedSprite;


    public bool CanInteract()
    {
        return !IsOpened
    }

    public void Interact()
    {
       if (!CanInteract()) return;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChestID ??= GlobalHelper.GenerateuniqueID(GameObject);
    } 

    
    private void openchest()
    {
        setopened(true);
        if(itemPrefab)
        {
            GameObject droppedItem =Istantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
        }
    }

    
    private void setopened(bool opened)
    {
       
        if (IsOpened = opened)
        {
            getComponent<spriteRenderer>().sprite = opendedSprite
        }

    }
}