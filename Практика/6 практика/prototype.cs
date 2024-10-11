using System;
using System.Collections.Generic;

public interface ICloneable
{
    object Clone();
}


public class Skill : ICloneable
{
    public string Name { get; set; }
    public int Power { get; set; }

    public Skill(string name, int power)
    {
        Name = name;
        Power = power;
    }

    public object Clone()
    {
        return new Skill(Name, Power);
    }
}

public class Weapon : ICloneable
{
    public string Name { get; set; }
    public int Damage { get; set; }

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }

    public object Clone()
    {
        return new Weapon(Name, Damage);
    }
}


public class Armor : ICloneable
{
    public string Name { get; set; }
    public int Defense { get; set; }

    public Armor(string name, int defense)
    {
        Name = name;
        Defense = defense;
    }

    public object Clone()
    {
        return new Armor(Name, Defense);
    }
}




public class Character : ICloneable
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public Weapon Weapon { get; set; }
    public Armor Armor { get; set; }
    public List<Skill> Skills { get; set; }

    public Character(string name, int health, int strength, int agility, int intelligence)
    {
        Name = name;
        Health = health;
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
        Skills = new List<Skill>();
    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }

    public object Clone()
    {
       
        Character clone = (Character)this.MemberwiseClone();
        clone.Weapon = (Weapon)this.Weapon.Clone();
        clone.Armor = (Armor)this.Armor.Clone();
        clone.Skills = new List<Skill>();
        foreach (var skill in Skills)
        {
            clone.Skills.Add((Skill)skill.Clone());
        }
        return clone;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        
        Character worm1 = new Character("Worm1", 100, 15, 10, 5)
        {
            Weapon = new Weapon("Shotgun", 30),
            Armor = new Armor("Light Armor", 5)
        };
        worm1.AddSkill(new Skill("Grenade", 25));
        worm1.AddSkill(new Skill("Air Strike", 50));

        Character worm2 = (Character)worm1.Clone();
        worm2.Name = "Worm2"; 

        

        worm2.Skills.Clear();
        worm2.AddSkill(new Skill("Banana Bomb", 60));
        worm2.AddSkill(new Skill("Sheep rocket", 80));

  


        Console.WriteLine($"Original: {worm1.Name}, Health: {worm1.Health}, Weapon: {worm1.Weapon.Name}, Skills: {string.Join(", ", worm1.Skills.Select(s => s.Name))}");
        Console.WriteLine($"Cloned: {worm2.Name}, Health: {worm2.Health}, Weapon: {worm2.Weapon.Name}, Skills: {string.Join(", ", worm2.Skills.Select(s => s.Name))}");
    }
}
