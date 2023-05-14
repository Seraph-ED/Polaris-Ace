extends Sprite


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
export(String, FILE, "*.png") var image_path


# Called when the node enters the scene tree for the first time.
func _ready():
	var texture = ImageTexture.new()
	var image = Image.new()
	image.load(image_path)
	texture.create_from_image(image)
	$Sprite.texture = texture
	
	pass # Replace with function body.W


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
