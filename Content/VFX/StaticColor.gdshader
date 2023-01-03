shader_type canvas_item;

uniform vec4 override : hint_color;


void fragment(){
    
    COLOR = override;
}