# GdUnit generated TestSuite
class_name HealthComponentTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

# TestSuite generated from
const __source: String = 'res://scripts/components/health_component.gd'


func test_decrease_life_count_emits_only_life_lost_signal_when_many_lives_left() -> void:
	# Arrange
	var component := _get_component(5)
	
	# Act
	component.decrease_life_count()
	
	# Assert
	await assert_signal(component).is_emitted(component.life_lost)
	await assert_signal(component).is_not_emitted(component.last_life_left)
	await assert_signal(component).is_not_emitted(component.died)

func test_decrease_life_count_emits_last_life_left_signal_and_life_lost_signal_on_last_life() -> void:
	# Arrange
	var component := _get_component(2)
	
	# Act
	component.decrease_life_count()
	
	# Assert
	await assert_signal(component).is_emitted(component.life_lost)
	await assert_signal(component).is_emitted(component.last_life_left)
	await assert_signal(component).is_not_emitted(component.died)

func test_decrease_life_count_emits_died_signal_and_life_lost_signal_when_no_lives_left() -> void:
	# Arrange
	var component := _get_component(1)
	
	# Act
	component.decrease_life_count()
	
	# Assert
	await assert_signal(component).is_emitted(component.life_lost)
	await assert_signal(component).is_not_emitted(component.last_life_left)
	await assert_signal(component).is_emitted(component.died)

func _get_component(max_life: int) -> HealthComponent:
	var component = HealthComponent.new()
	component.reset(max_life)
	
	return monitor_signals(component)
