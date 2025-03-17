
using UnityEngine;

public interface Attackable
{
    /// <summary>
    /// Method to attack the player.
    /// </summary>
    /// <param name="playerRef">The player GameObject who will attack.</param>
    void Attack(GameObject playerRef);
    
}
