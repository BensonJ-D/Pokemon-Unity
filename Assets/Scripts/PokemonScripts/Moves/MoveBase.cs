using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using PokemonScripts.Moves.Effects;
using UnityEngine;
using UnityEngine.Events;

namespace PokemonScripts
{
    [CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public class MoveBase : ScriptableObject
    {
        [SerializeField] private int number;
        [SerializeField] private string moveName;

        // [TextArea] [SerializeField] private string description;

        [SerializeField] private PokemonType type;
        [SerializeField] private int power;
        [SerializeField] private int pp;
        [SerializeField] private int accuracy;
        [SerializeField] private MoveCategory category;
        [SerializeField] private List<PrimaryStatusEffect> primaryStatusEffects;
        [SerializeField] private List<StatModifierEffect> statModifierEffects;
        [SerializeField] private MoveTarget target;
        [SerializeField] private int effectChance;

        public int Number => number;
        public string Name => moveName;
        public PokemonType Type => type;
        public int Power => power;
        public int Accuracy => accuracy;
        public int Pp => pp;
        public MoveCategory Category => category;
        public MoveTarget Target => target;
        
        public int EffectChance => effectChance;
        public List<StatModifierEffect> StatModifierEffects => statModifierEffects;
        public List<PrimaryStatusEffect> PrimaryStatusEffects => primaryStatusEffects;

        public static readonly ReadOnlyDictionary<(PokemonType, PokemonType), float> TypeChart =
            new ReadOnlyDictionary<(PokemonType, PokemonType), float>(
                new Dictionary<(PokemonType, PokemonType), float>()
                {
                    {(PokemonType.Normal, PokemonType.None), 1.0f},
                    {(PokemonType.Normal, PokemonType.Normal), 1.0f},
                    {(PokemonType.Normal, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Normal, PokemonType.Flying), 1.0f},
                    {(PokemonType.Normal, PokemonType.Poison), 1.0f},
                    {(PokemonType.Normal, PokemonType.Ground), 1.0f},
                    {(PokemonType.Normal, PokemonType.Rock), 0.5f},
                    {(PokemonType.Normal, PokemonType.Bug), 1.0f},
                    {(PokemonType.Normal, PokemonType.Ghost), 0.0f},
                    {(PokemonType.Normal, PokemonType.Steel), 0.5f},
                    {(PokemonType.Normal, PokemonType.Fire), 1.0f},
                    {(PokemonType.Normal, PokemonType.Water), 1.0f},
                    {(PokemonType.Normal, PokemonType.Grass), 1.0f},
                    {(PokemonType.Normal, PokemonType.Electric), 1.0f},
                    {(PokemonType.Normal, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Normal, PokemonType.Ice), 1.0f},
                    {(PokemonType.Normal, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Normal, PokemonType.Dark), 1.0f},
                    {(PokemonType.Normal, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Fighting, PokemonType.None), 1.0f},
                    {(PokemonType.Fighting, PokemonType.Normal), 2.0f},
                    {(PokemonType.Fighting, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Fighting, PokemonType.Flying), 0.5f},
                    {(PokemonType.Fighting, PokemonType.Poison), 0.5f},
                    {(PokemonType.Fighting, PokemonType.Ground), 1.0f},
                    {(PokemonType.Fighting, PokemonType.Rock), 2.0f},
                    {(PokemonType.Fighting, PokemonType.Bug), 0.5f},
                    {(PokemonType.Fighting, PokemonType.Ghost), 0.0f},
                    {(PokemonType.Fighting, PokemonType.Steel), 2.0f},
                    {(PokemonType.Fighting, PokemonType.Fire), 1.0f},
                    {(PokemonType.Fighting, PokemonType.Water), 1.0f},
                    {(PokemonType.Fighting, PokemonType.Grass), 1.0f},
                    {(PokemonType.Fighting, PokemonType.Electric), 1.0f},
                    {(PokemonType.Fighting, PokemonType.Psychic), 0.5f},
                    {(PokemonType.Fighting, PokemonType.Ice), 2.0f},
                    {(PokemonType.Fighting, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Fighting, PokemonType.Dark), 2.0f},
                    {(PokemonType.Fighting, PokemonType.Fairy), 0.5f},
                    {(PokemonType.Flying, PokemonType.None), 1.0f},
                    {(PokemonType.Flying, PokemonType.Normal), 1.0f},
                    {(PokemonType.Flying, PokemonType.Fighting), 2.0f},
                    {(PokemonType.Flying, PokemonType.Flying), 1.0f},
                    {(PokemonType.Flying, PokemonType.Poison), 1.0f},
                    {(PokemonType.Flying, PokemonType.Ground), 1.0f},
                    {(PokemonType.Flying, PokemonType.Rock), 0.5f},
                    {(PokemonType.Flying, PokemonType.Bug), 2.0f},
                    {(PokemonType.Flying, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Flying, PokemonType.Steel), 0.5f},
                    {(PokemonType.Flying, PokemonType.Fire), 1.0f},
                    {(PokemonType.Flying, PokemonType.Water), 1.0f},
                    {(PokemonType.Flying, PokemonType.Grass), 2.0f},
                    {(PokemonType.Flying, PokemonType.Electric), 0.5f},
                    {(PokemonType.Flying, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Flying, PokemonType.Ice), 1.0f},
                    {(PokemonType.Flying, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Flying, PokemonType.Dark), 1.0f},
                    {(PokemonType.Flying, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Poison, PokemonType.None), 1.0f},
                    {(PokemonType.Poison, PokemonType.Normal), 1.0f},
                    {(PokemonType.Poison, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Poison, PokemonType.Flying), 1.0f},
                    {(PokemonType.Poison, PokemonType.Poison), 0.5f},
                    {(PokemonType.Poison, PokemonType.Ground), 0.5f},
                    {(PokemonType.Poison, PokemonType.Rock), 0.5f},
                    {(PokemonType.Poison, PokemonType.Bug), 1.0f},
                    {(PokemonType.Poison, PokemonType.Ghost), 0.5f},
                    {(PokemonType.Poison, PokemonType.Steel), 0.0f},
                    {(PokemonType.Poison, PokemonType.Fire), 1.0f},
                    {(PokemonType.Poison, PokemonType.Water), 1.0f},
                    {(PokemonType.Poison, PokemonType.Grass), 2.0f},
                    {(PokemonType.Poison, PokemonType.Electric), 1.0f},
                    {(PokemonType.Poison, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Poison, PokemonType.Ice), 1.0f},
                    {(PokemonType.Poison, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Poison, PokemonType.Dark), 1.0f},
                    {(PokemonType.Poison, PokemonType.Fairy), 2.0f},
                    {(PokemonType.Ground, PokemonType.None), 1.0f},
                    {(PokemonType.Ground, PokemonType.Normal), 1.0f},
                    {(PokemonType.Ground, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Ground, PokemonType.Flying), 0.0f},
                    {(PokemonType.Ground, PokemonType.Poison), 2.0f},
                    {(PokemonType.Ground, PokemonType.Ground), 1.0f},
                    {(PokemonType.Ground, PokemonType.Rock), 2.0f},
                    {(PokemonType.Ground, PokemonType.Bug), 0.5f},
                    {(PokemonType.Ground, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Ground, PokemonType.Steel), 2.0f},
                    {(PokemonType.Ground, PokemonType.Fire), 2.0f},
                    {(PokemonType.Ground, PokemonType.Water), 1.0f},
                    {(PokemonType.Ground, PokemonType.Grass), 0.5f},
                    {(PokemonType.Ground, PokemonType.Electric), 2.0f},
                    {(PokemonType.Ground, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Ground, PokemonType.Ice), 1.0f},
                    {(PokemonType.Ground, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Ground, PokemonType.Dark), 1.0f},
                    {(PokemonType.Ground, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Rock, PokemonType.None), 1.0f},
                    {(PokemonType.Rock, PokemonType.Normal), 1.0f},
                    {(PokemonType.Rock, PokemonType.Fighting), 0.5f},
                    {(PokemonType.Rock, PokemonType.Flying), 2.0f},
                    {(PokemonType.Rock, PokemonType.Poison), 1.0f},
                    {(PokemonType.Rock, PokemonType.Ground), 0.5f},
                    {(PokemonType.Rock, PokemonType.Rock), 1.0f},
                    {(PokemonType.Rock, PokemonType.Bug), 2.0f},
                    {(PokemonType.Rock, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Rock, PokemonType.Steel), 0.5f},
                    {(PokemonType.Rock, PokemonType.Fire), 2.0f},
                    {(PokemonType.Rock, PokemonType.Water), 1.0f},
                    {(PokemonType.Rock, PokemonType.Grass), 1.0f},
                    {(PokemonType.Rock, PokemonType.Electric), 1.0f},
                    {(PokemonType.Rock, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Rock, PokemonType.Ice), 2.0f},
                    {(PokemonType.Rock, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Rock, PokemonType.Dark), 1.0f},
                    {(PokemonType.Rock, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Bug, PokemonType.None), 1.0f},
                    {(PokemonType.Bug, PokemonType.Normal), 1.0f},
                    {(PokemonType.Bug, PokemonType.Fighting), 0.5f},
                    {(PokemonType.Bug, PokemonType.Flying), 0.5f},
                    {(PokemonType.Bug, PokemonType.Poison), 0.5f},
                    {(PokemonType.Bug, PokemonType.Ground), 1.0f},
                    {(PokemonType.Bug, PokemonType.Rock), 1.0f},
                    {(PokemonType.Bug, PokemonType.Bug), 1.0f},
                    {(PokemonType.Bug, PokemonType.Ghost), 0.5f},
                    {(PokemonType.Bug, PokemonType.Steel), 0.5f},
                    {(PokemonType.Bug, PokemonType.Fire), 0.5f},
                    {(PokemonType.Bug, PokemonType.Water), 1.0f},
                    {(PokemonType.Bug, PokemonType.Grass), 2.0f},
                    {(PokemonType.Bug, PokemonType.Electric), 1.0f},
                    {(PokemonType.Bug, PokemonType.Psychic), 2.0f},
                    {(PokemonType.Bug, PokemonType.Ice), 1.0f},
                    {(PokemonType.Bug, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Bug, PokemonType.Dark), 2.0f},
                    {(PokemonType.Bug, PokemonType.Fairy), 0.5f},
                    {(PokemonType.Ghost, PokemonType.None), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Normal), 0.0f},
                    {(PokemonType.Ghost, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Flying), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Poison), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Ground), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Rock), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Bug), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Ghost), 2.0f},
                    {(PokemonType.Ghost, PokemonType.Steel), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Fire), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Water), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Grass), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Electric), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Psychic), 2.0f},
                    {(PokemonType.Ghost, PokemonType.Ice), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Ghost, PokemonType.Dark), 0.5f},
                    {(PokemonType.Ghost, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Steel, PokemonType.None), 1.0f},
                    {(PokemonType.Steel, PokemonType.Normal), 1.0f},
                    {(PokemonType.Steel, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Steel, PokemonType.Flying), 1.0f},
                    {(PokemonType.Steel, PokemonType.Poison), 1.0f},
                    {(PokemonType.Steel, PokemonType.Ground), 1.0f},
                    {(PokemonType.Steel, PokemonType.Rock), 2.0f},
                    {(PokemonType.Steel, PokemonType.Bug), 1.0f},
                    {(PokemonType.Steel, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Steel, PokemonType.Steel), 0.5f},
                    {(PokemonType.Steel, PokemonType.Fire), 0.5f},
                    {(PokemonType.Steel, PokemonType.Water), 0.5f},
                    {(PokemonType.Steel, PokemonType.Grass), 1.0f},
                    {(PokemonType.Steel, PokemonType.Electric), 0.5f},
                    {(PokemonType.Steel, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Steel, PokemonType.Ice), 2.0f},
                    {(PokemonType.Steel, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Steel, PokemonType.Dark), 1.0f},
                    {(PokemonType.Steel, PokemonType.Fairy), 2.0f},
                    {(PokemonType.Fire, PokemonType.None), 1.0f},
                    {(PokemonType.Fire, PokemonType.Normal), 1.0f},
                    {(PokemonType.Fire, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Fire, PokemonType.Flying), 1.0f},
                    {(PokemonType.Fire, PokemonType.Poison), 1.0f},
                    {(PokemonType.Fire, PokemonType.Ground), 1.0f},
                    {(PokemonType.Fire, PokemonType.Rock), 0.5f},
                    {(PokemonType.Fire, PokemonType.Bug), 2.0f},
                    {(PokemonType.Fire, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Fire, PokemonType.Steel), 2.0f},
                    {(PokemonType.Fire, PokemonType.Fire), 0.5f},
                    {(PokemonType.Fire, PokemonType.Water), 0.5f},
                    {(PokemonType.Fire, PokemonType.Grass), 2.0f},
                    {(PokemonType.Fire, PokemonType.Electric), 1.0f},
                    {(PokemonType.Fire, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Fire, PokemonType.Ice), 2.0f},
                    {(PokemonType.Fire, PokemonType.Dragon), 0.5f},
                    {(PokemonType.Fire, PokemonType.Dark), 1.0f},
                    {(PokemonType.Fire, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Water, PokemonType.None), 1.0f},
                    {(PokemonType.Water, PokemonType.Normal), 1.0f},
                    {(PokemonType.Water, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Water, PokemonType.Flying), 1.0f},
                    {(PokemonType.Water, PokemonType.Poison), 1.0f},
                    {(PokemonType.Water, PokemonType.Ground), 2.0f},
                    {(PokemonType.Water, PokemonType.Rock), 2.0f},
                    {(PokemonType.Water, PokemonType.Bug), 1.0f},
                    {(PokemonType.Water, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Water, PokemonType.Steel), 1.0f},
                    {(PokemonType.Water, PokemonType.Fire), 2.0f},
                    {(PokemonType.Water, PokemonType.Water), 0.5f},
                    {(PokemonType.Water, PokemonType.Grass), 0.5f},
                    {(PokemonType.Water, PokemonType.Electric), 1.0f},
                    {(PokemonType.Water, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Water, PokemonType.Ice), 1.0f},
                    {(PokemonType.Water, PokemonType.Dragon), 0.5f},
                    {(PokemonType.Water, PokemonType.Dark), 1.0f},
                    {(PokemonType.Water, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Grass, PokemonType.None), 1.0f},
                    {(PokemonType.Grass, PokemonType.Normal), 1.0f},
                    {(PokemonType.Grass, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Grass, PokemonType.Flying), 0.5f},
                    {(PokemonType.Grass, PokemonType.Poison), 0.5f},
                    {(PokemonType.Grass, PokemonType.Ground), 2.0f},
                    {(PokemonType.Grass, PokemonType.Rock), 2.0f},
                    {(PokemonType.Grass, PokemonType.Bug), 0.5f},
                    {(PokemonType.Grass, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Grass, PokemonType.Steel), 0.5f},
                    {(PokemonType.Grass, PokemonType.Fire), 0.5f},
                    {(PokemonType.Grass, PokemonType.Water), 2.0f},
                    {(PokemonType.Grass, PokemonType.Grass), 0.5f},
                    {(PokemonType.Grass, PokemonType.Electric), 1.0f},
                    {(PokemonType.Grass, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Grass, PokemonType.Ice), 1.0f},
                    {(PokemonType.Grass, PokemonType.Dragon), 0.5f},
                    {(PokemonType.Grass, PokemonType.Dark), 1.0f},
                    {(PokemonType.Grass, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Electric, PokemonType.None), 1.0f},
                    {(PokemonType.Electric, PokemonType.Normal), 1.0f},
                    {(PokemonType.Electric, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Electric, PokemonType.Flying), 2.0f},
                    {(PokemonType.Electric, PokemonType.Poison), 1.0f},
                    {(PokemonType.Electric, PokemonType.Ground), 0.0f},
                    {(PokemonType.Electric, PokemonType.Rock), 1.0f},
                    {(PokemonType.Electric, PokemonType.Bug), 1.0f},
                    {(PokemonType.Electric, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Electric, PokemonType.Steel), 1.0f},
                    {(PokemonType.Electric, PokemonType.Fire), 1.0f},
                    {(PokemonType.Electric, PokemonType.Water), 2.0f},
                    {(PokemonType.Electric, PokemonType.Grass), 0.5f},
                    {(PokemonType.Electric, PokemonType.Electric), 0.5f},
                    {(PokemonType.Electric, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Electric, PokemonType.Ice), 1.0f},
                    {(PokemonType.Electric, PokemonType.Dragon), 0.5f},
                    {(PokemonType.Electric, PokemonType.Dark), 1.0f},
                    {(PokemonType.Electric, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Psychic, PokemonType.None), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Normal), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Fighting), 2.0f},
                    {(PokemonType.Psychic, PokemonType.Flying), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Poison), 2.0f},
                    {(PokemonType.Psychic, PokemonType.Ground), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Rock), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Bug), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Steel), 0.5f},
                    {(PokemonType.Psychic, PokemonType.Fire), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Water), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Grass), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Electric), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Psychic), 0.5f},
                    {(PokemonType.Psychic, PokemonType.Ice), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Psychic, PokemonType.Dark), 0.0f},
                    {(PokemonType.Psychic, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Ice, PokemonType.None), 1.0f},
                    {(PokemonType.Ice, PokemonType.Normal), 1.0f},
                    {(PokemonType.Ice, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Ice, PokemonType.Flying), 2.0f},
                    {(PokemonType.Ice, PokemonType.Poison), 1.0f},
                    {(PokemonType.Ice, PokemonType.Ground), 2.0f},
                    {(PokemonType.Ice, PokemonType.Rock), 1.0f},
                    {(PokemonType.Ice, PokemonType.Bug), 1.0f},
                    {(PokemonType.Ice, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Ice, PokemonType.Steel), 0.5f},
                    {(PokemonType.Ice, PokemonType.Fire), 0.5f},
                    {(PokemonType.Ice, PokemonType.Water), 0.5f},
                    {(PokemonType.Ice, PokemonType.Grass), 2.0f},
                    {(PokemonType.Ice, PokemonType.Electric), 1.0f},
                    {(PokemonType.Ice, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Ice, PokemonType.Ice), 0.5f},
                    {(PokemonType.Ice, PokemonType.Dragon), 2.0f},
                    {(PokemonType.Ice, PokemonType.Dark), 1.0f},
                    {(PokemonType.Ice, PokemonType.Fairy), 1.0f},
                    {(PokemonType.Dragon, PokemonType.None), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Normal), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Fighting), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Flying), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Poison), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Ground), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Rock), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Bug), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Steel), 0.5f},
                    {(PokemonType.Dragon, PokemonType.Fire), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Water), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Grass), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Electric), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Ice), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Dragon), 2.0f},
                    {(PokemonType.Dragon, PokemonType.Dark), 1.0f},
                    {(PokemonType.Dragon, PokemonType.Fairy), 0.0f},
                    {(PokemonType.Dark, PokemonType.None), 1.0f},
                    {(PokemonType.Dark, PokemonType.Normal), 1.0f},
                    {(PokemonType.Dark, PokemonType.Fighting), 0.5f},
                    {(PokemonType.Dark, PokemonType.Flying), 1.0f},
                    {(PokemonType.Dark, PokemonType.Poison), 1.0f},
                    {(PokemonType.Dark, PokemonType.Ground), 1.0f},
                    {(PokemonType.Dark, PokemonType.Rock), 1.0f},
                    {(PokemonType.Dark, PokemonType.Bug), 1.0f},
                    {(PokemonType.Dark, PokemonType.Ghost), 2.0f},
                    {(PokemonType.Dark, PokemonType.Steel), 1.0f},
                    {(PokemonType.Dark, PokemonType.Fire), 1.0f},
                    {(PokemonType.Dark, PokemonType.Water), 1.0f},
                    {(PokemonType.Dark, PokemonType.Grass), 1.0f},
                    {(PokemonType.Dark, PokemonType.Electric), 1.0f},
                    {(PokemonType.Dark, PokemonType.Psychic), 2.0f},
                    {(PokemonType.Dark, PokemonType.Ice), 1.0f},
                    {(PokemonType.Dark, PokemonType.Dragon), 1.0f},
                    {(PokemonType.Dark, PokemonType.Dark), 0.5f},
                    {(PokemonType.Dark, PokemonType.Fairy), 0.5f},
                    {(PokemonType.Fairy, PokemonType.None), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Normal), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Fighting), 2.0f},
                    {(PokemonType.Fairy, PokemonType.Flying), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Poison), 0.5f},
                    {(PokemonType.Fairy, PokemonType.Ground), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Rock), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Bug), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Ghost), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Steel), 0.5f},
                    {(PokemonType.Fairy, PokemonType.Fire), 0.5f},
                    {(PokemonType.Fairy, PokemonType.Water), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Grass), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Electric), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Psychic), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Ice), 1.0f},
                    {(PokemonType.Fairy, PokemonType.Dragon), 2.0f},
                    {(PokemonType.Fairy, PokemonType.Dark), 2.0f},
                    {(PokemonType.Fairy, PokemonType.Fairy), 1.0f}
                }
            );

        public static AttackEffectiveness GetEffectiveness(float modifier) => AttackEffectivenessLookup[modifier];

        private static readonly ReadOnlyDictionary<float, AttackEffectiveness> AttackEffectivenessLookup =
            new ReadOnlyDictionary<float, AttackEffectiveness>(
                new Dictionary<float, AttackEffectiveness>()
                {
                    {0f, AttackEffectiveness.NoEffect},
                    {0.25f, AttackEffectiveness.NotVeryEffective},
                    {0.5f, AttackEffectiveness.NotVeryEffective},
                    {1f, AttackEffectiveness.NormallyEffective},
                    {2f, AttackEffectiveness.SuperEffective},
                    {4f, AttackEffectiveness.SuperEffective}
                }
            );
    }

    public enum AttackEffectiveness
    {
        NoEffect = 0,
        NotVeryEffective = 1,
        NormallyEffective = 2,
        SuperEffective = 3
    }

    public enum MoveTarget
    {
        Self,
        Foe
    }
}