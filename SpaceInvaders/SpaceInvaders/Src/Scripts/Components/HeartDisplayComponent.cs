using System.Collections.Generic;
using System.Linq;
using Godot;
using static Godot.GD;

namespace SpaceInvaders.Scripts.Components;

[GlobalClass]
public partial class HeartDisplayComponent : Node
{
    private static readonly Texture2D HeartEmpty = Load<Texture2D>("uid://bpjx44406nhxe");
    private static readonly Texture2D HeartFull = Load<Texture2D>("uid://c3xjcawoynrv5");

    private const int HeartOffset = 8;

    private int _maxLifeCount;
    private int _lifeCount;

    public CanvasLayer HudLayer { get; set; }
    public Vector2 HeartStartPosition { get; set; }

    public IReadOnlyList<Sprite2D> EmptyHearts => _emptyHearts;
    public Sprite2D LastFullHeart => _fullHearts.First(s => s.Scale.X > 0);
    
    private readonly List<Sprite2D> _emptyHearts = [];
    private readonly List<Sprite2D> _fullHearts = [];

    private bool SpritesNotCreated => _fullHearts.Count == 0;
    
    public void Reset(int maxLifeCount)
    {
        _lifeCount = maxLifeCount;

        if (_maxLifeCount != maxLifeCount || SpritesNotCreated)
        {
            _maxLifeCount = maxLifeCount;
            CreateSprites();
        }

        ResetAllHeartsScale();
    }

    private void ResetAllHeartsScale()
    {
        foreach (var heart in _fullHearts)
        {
            heart.Scale = Vector2.One;
        }
    }
    
    private void CreateSprites()
    {
        QueueFreeHearts();

        _fullHearts.Clear();
        _emptyHearts.Clear();

        var currentPosition = HeartStartPosition;

        for (int i = 0; i < _maxLifeCount; i++)
        {
            var fullHeart = CreateSprite(HeartFull, currentPosition);
            var emptyHeart = CreateSprite(HeartEmpty, currentPosition);

            currentPosition.X += fullHeart.Texture.GetWidth() + HeartOffset;

            _fullHearts.Add(fullHeart);
            _emptyHearts.Add(emptyHeart);

            HudLayer.AddChild(fullHeart);
            HudLayer.AddChild(emptyHeart);
        }
    }

    private void QueueFreeHearts()
    {
        foreach (var heart in _fullHearts)
        {
            heart.QueueFree();
        }

        foreach (var heart in _emptyHearts)
        {
            heart.QueueFree();
        }
    }

    private Sprite2D CreateSprite(Texture2D texture, Vector2 pos)
    {
        var heart = new Sprite2D
        {
            Texture = texture,
            Position = pos
        };

        return heart;
    }
}