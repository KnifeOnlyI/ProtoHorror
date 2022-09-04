using System;
using Utils;

namespace Prefabs.Player.Scripts
{
    [Serializable]
    public class PlayerData
    {
        public int maxLife;
        public int maxStamina;
        public int maxMana;

        public int life;
        public int stamina;
        public int mana;

        public float[] position;

        public PlayerData(Player player)
        {
            maxLife = player.initialLife;
            maxStamina = player.initialStamina;
            maxMana = player.initialMana;

            life = player.GetLife();
            stamina = player.GetStamina();
            mana = player.GetMana();

            position = SerializationUtils.Serialize(player.GetPosition());
        }
    }
}