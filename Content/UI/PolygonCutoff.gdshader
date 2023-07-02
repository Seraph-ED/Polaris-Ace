shader_type canvas_item;

uniform vec4 bg_col : hint_color;

void fragment(){
	if(UV.x>0.0&&UV.y>0.0&&UV.x<1.0&&UV.y<1.0){
		COLOR = bg_col;
	}else{
		COLOR = vec4(0,0,0,0);
	}
	
	
	
	
}