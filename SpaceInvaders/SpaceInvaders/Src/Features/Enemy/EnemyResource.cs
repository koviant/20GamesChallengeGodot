using Godot;
using SpaceInvaders.Core.Features.Enemy;

namespace SpaceInvaders.Features.Enemy;

[GlobalClass]
public partial class EnemyResource : Resource, IEnemyData
{
    [Export]
    public EnemyType Type { get; set; }
}