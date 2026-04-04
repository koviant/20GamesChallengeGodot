using Godot;

namespace SpaceInvaders.Scripts;

public partial class Main : Node2D
{
	private ColorRect _player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{ 
		_player = GetNode<ColorRect>("Player");
		_player.Position = new Vector2(100, 100);
		_player.Size = new Vector2(100, 100);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionPressed("ui_up"))
		{
			_player.Position = _player.Position with { Y = _player.Position.Y - 10 };
		} 
		
		if (Input.IsActionPressed("ui_down"))
		{
			_player.Position = _player.Position with { Y = _player.Position.Y + 10 };
		} 
		
		if (Input.IsActionPressed("ui_left"))
		{
			_player.Position = _player.Position with { X = _player.Position.X - 10 };
		} 
		
		if (Input.IsActionPressed("ui_right"))
		{
			_player.Position = _player.Position with { X = _player.Position.X + 10 };
		}
	}
}