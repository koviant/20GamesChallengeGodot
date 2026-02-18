extends CanvasLayer

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	reset()

func reset() -> void:
	$ScoreLeft.text = "0"
	$ScoreRight.text = "0"

func update_left_score(score: int) -> void:
	$ScoreLeft.text = str(score)

func update_right_score(score: int) -> void:
	$ScoreRight.text = str(score)
