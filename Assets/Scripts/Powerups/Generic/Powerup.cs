using UnityEngine;

public interface Powerup
{
    public enum powerupType
    {
        Q,
        Paw,
        Augment
    }
    
    public string GetName();
    public string GetDescription();
    public powerupType GetSlot();
    public int GetCost();
    public Sprite GetSprite();
}
