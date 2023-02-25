shader_type canvas_item;

uniform sampler2D dissolve_texture : hint_albedo;
uniform vec4 border_col : hint_color;
uniform float dissolve_texture_scale_X : hint_range(0.1, 10);
uniform float dissolve_texture_scale_Y : hint_range(0.1, 10);
uniform float border_threshold : hint_range(0,1);
uniform float offsetx : hint_range(0,1);
uniform float offsety : hint_range(0,1);
uniform float dissolve_value : hint_range(0,1);

void fragment(){
    //vec2 offset = vec2(offsetx, offsety);
	vec2 uv2 = vec2(fract((UV.x*dissolve_texture_scale_X)+offsetx), fract((UV.y*dissolve_texture_scale_Y)+offsety));
	vec4 main_texture = texture(TEXTURE, uv2);
    vec4 noise_texture = texture(dissolve_texture, uv2);
    
	float bd = float(noise_texture.r - dissolve_value)/float(1.0-dissolve_value);
	bd = clamp(bd, float(0), float(1));
	//float a = main_texture.a;
	if(bd < (border_threshold)){
		 main_texture = border_col;
	}
	main_texture.a *= floor(dissolve_value + min(1, noise_texture.x));
	COLOR = main_texture;
	
	
    
}