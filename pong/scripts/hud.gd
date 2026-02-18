extends CanvasLayer

var left_info_text = "0":
	get: return $InfoLeft.text
	set(value): 
		$InfoLeft.text = value 
		
var right_info_text = "0":
	get: return $InfoRight.text
	set(value):
		$InfoRight.text = value

func reset() -> void:
	left_info_text = "0"
	right_info_text = "0"
	$RestartButton.hide()

func show_restart() -> void:
	$RestartButton.show()
