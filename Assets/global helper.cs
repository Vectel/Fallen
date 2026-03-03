using UnityEngine;

public static class GlobalHelper 
{
    public static string GenerateuniqueID( GameObject obj)
    {
        return $"{obj.scene.name}_{obj.tarnsform.position.x}_{obj.position.y}";
    }
}
