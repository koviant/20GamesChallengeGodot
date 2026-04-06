using Godot;
using GodotUtilities;
using SpaceInvaders.Core.Features.Main;
using SpaceInvaders.Core.Features.Player;

namespace SpaceInvaders.Scripts.Scenes;

[Scene]
public partial class Game : Node2D, IMainView
{
	[Node] private Player _player;
	[Node] private CanvasLayer _hud;

	private Vector2 VisibleRectSize => GetViewport().GetVisibleRect().Size;
	
	public override void _Ready()
	{ 
		WireNodes();
		
		_player.HeartDisplayComponent.HudLayer = _hud;
		_player.HeartDisplayComponent.HeartStartPosition = new Vector2(100, VisibleRectSize.Y - 200);
	}


	public IPlayerView Player => _player;
}