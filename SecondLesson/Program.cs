using System.Collections;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

public enum PlayerType { Tank, Healer, Archer }
public enum AbilityType { Heal }
public class UniqueAbilityEventArgs: EventArgs
{
    public double AbilityValue { get; set; }
    public AbilityType Type { get; set; }
}

public class Player
{
    public double Health { get; private set; } = 100;
    public double Armor { get; private set; } = 100;
    public PlayerType Type { get; private set; }

    public event EventHandler<UniqueAbilityEventArgs> UniqueAbilityRealization;
    private void OnUniqueAbilityRealization(UniqueAbilityEventArgs e)
    {
        EventHandler<UniqueAbilityEventArgs> handler = UniqueAbilityRealization;
        if (handler != null)
            handler(this, e);
    }
    public Player(PlayerType playerType)
    {
        Type = playerType;
    }
    public void TakingDamage(double damage)
    {
        Health -= damage / Armor;
    }
    public void AbilityHandler(object sender, UniqueAbilityEventArgs e)
    {
        switch (e.Type)
        {
            case AbilityType.Heal: Health += e.AbilityValue; break;
        }
    }

    public void UniqueAbility()
    {
        switch (Type)
        {
            case PlayerType.Healer: 
                OnUniqueAbilityRealization(new UniqueAbilityEventArgs() { 
                    AbilityValue = 100, Type = AbilityType.Heal 
                }); 
                break;
        }
    }
}

public class Team
{
    private Player[] players;
    public Team(Player[] players)
    {
        this.players = players;
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = 0; j < players.Length; j++)
            {
                if (i != j)
                    this.players[i].UniqueAbilityRealization += this.players[j].AbilityHandler;
            }
        }
    }
}


/*
class Enemy
{
    public static double attackLimit { get; private set; } = 100.0;
    public EnemyType _enemyType;
    public double _attack = 1.5;
    public double _armor;
    public double Health { get; private set; } = 100;
    private byte _defense;
    public byte Defense { 
        get { return _defense; } 
        set { _defense = value; } 
    }
    public Enemy(EnemyType enemyType, double attack, byte health)
    {
        _enemyType = enemyType;
        _attack = attack; 
        _armor = health;
    }
    public static void PrintDescription(EnemyType _enemyType)
    {
        switch (_enemyType)
        {
            case EnemyType.Tank: Console.WriteLine("Очень сильный враг, имеет большой урон."); break;
            case EnemyType.Wizard: Console.WriteLine("Использует магические атаки для нанесения урона."); break;
            case EnemyType.Archer: Console.WriteLine("Атакует врага издалека с помощью лука."); break;
        }
    }

    public void PrintState()
    {
        Console.WriteLine($"Здоровье: {Health} \nБроня: {_armor}");
    }
    public void Hurt(double damage)
    {
        Health -= damage;
    }
}


static class Fire
{
    public static double FireDamage { get; private set; } = 0.5;
    public static void Damage(Enemy enemy)
    {
        enemy.Hurt(FireDamage);
    }
    public static void Damage(Protagonist gg)
    {
        gg.Hurt(FireDamage);
    }
}


class Protagonist
{
    public EnemyType EnemyType { get; private set; } = EnemyType.Tank;
    public double Attack { get; private set; } = 1.0;
    public double Armor { get; private set; } = 100.0;
    public double Health { get; private set; } = 100.0;
    public byte Defense { get; private set; } = 100;

    private static Protagonist? _instance = null;

    private Protagonist() { }

    public static Protagonist GetInstance()
    {
        if (_instance == null)
            _instance = new Protagonist();
        return _instance;
    }
    public void Hurt(double damage)
    {
        Health -= damage;
    }
    public void PrintState()
    {
        Console.WriteLine($"Здоровье: {Health} \nБроня: {Armor}");
    }
}
*/

partial class Program
{
    static void Main()
    {
        Player tank = new Player(PlayerType.Tank);
        Player healer = new Player(PlayerType.Healer);
        Player archer = new Player(PlayerType.Archer);

        Dictionary<PlayerType, Player> dict = new Dictionary<PlayerType, Player>();
        dict.Add(PlayerType.Tank, tank);
        dict[PlayerType.Healer] = healer;

        foreach (PlayerType p in dict.Keys)
        {

        }


        
    }
}

 