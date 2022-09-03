/// <summary>
/// The base class for all interractables
/// </summary>
public interface IInterractable
{
    /// <summary>
    /// Try to interract
    /// <param name="player">The player to check</param>
    /// </summary>
    public void Interract(Prefabs.Player.Scripts.Player player);

    /// <summary>
    /// Determine if the specified player can interract
    /// </summary>
    /// <param name="player">The player to check</param>
    /// <returns>TRUE if the player can interract, FALSE otherwise</returns>
    public bool CanInterract(Prefabs.Player.Scripts.Player player);
}