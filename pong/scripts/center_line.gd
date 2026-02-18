extends Line2D

var line_segment_length = 30;

func _draw():
	assert(len(points) == 2)
	var start = points[0]
	var end = points[1]
	var segmentStart = start
	var segmentEnd = Vector2(start.x, start.y + line_segment_length)
	while segmentStart.y < end.y:
		draw_line(segmentStart, segmentEnd, Color.WHITE)
		segmentStart.y += 2 * line_segment_length
		segmentEnd.y += 2 * line_segment_length
