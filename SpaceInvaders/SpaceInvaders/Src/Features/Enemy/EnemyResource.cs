using Godot;
using SpaceInvaders.Core.Features.Enemy;
using SpaceInvaders.Core.Features.Enemy.Interfaces;

namespace SpaceInvaders.Features.Enemy;

[GlobalClass]
public partial class EnemyResource : Resource, IEnemyData
{
    [Export] public EnemyType Type { get; set; }
}