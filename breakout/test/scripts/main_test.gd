# GdUnit generated TestSuite
class_name MainTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

# TestSuite generated from
const __source: String = 'res://scripts/main.gd'


func test_paddle_position_does_not_teleport_from_start_position_to_last_position_when_moved_after_reset() -> void:
	# Arrange
	const max_expected_move := 10
	
	var runner = scene_runner("res://scenes/main.tscn")
	var main: BreakoutMain = runner.scene()
	main.start_timeout_override_seconds = 0.1
	main.skip_death_animation = true
	
	var paddle: Paddle = runner.find_child("Paddle")
	var paddle_controller := runner.find_child("PaddleController")
	paddle_controller.set_physics_process(false)
	paddle.horizontal_velocity = 1000
	
	# Act
	await await_millis(500)
	await main.handle_death()
	
	paddle.horizontal_velocity = 10
	await await_millis(500)
		
	# Assert
	print(main.paddle.position)
	print(main.paddle.transform.origin)
	print(main.paddle_start_position)
	
	assert_float(main.paddle.position.x).is_equal_approx(main.paddle_start_position.x, max_expected_move)
	
