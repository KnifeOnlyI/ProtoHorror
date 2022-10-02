using Prefabs.Player.Scripts;

/// <summary>
/// The base class for all interractables
/// </summary>
public interface IInterractable
{
    /// <summary>
    /// Try to interract
    /// <param name="player">The player to check</param>
    /// </summary>
    public void Interract(Player player);

    /// <summary>
    /// Try to uninterract
    /// </summary>
    /// <param name="player">The player to check</param>
    public void Uninterract(Player player);

    /// <summary>
    /// Determine if the specified player can interract
    /// </summary>
    /// <param name="player">The player to check</param>
    /// <returns>TRUE if the player can interract, FALSE otherwise</returns>
    public bool CanInterract(Player player);
}