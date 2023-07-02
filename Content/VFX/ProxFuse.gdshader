shader_type canvas_item;

uniform vec4 waveCol : hint_color;
uniform float timeOffset : hint_range(-2, 2, 0.02);
uniform float timeOffsetRotate : hint_range(-2, 2, 0.02);
uniform float waves : hint_range(1,5, 1);
uniform float minOpacity : hint_range(0.0,1.0);
uniform float maxOpacity : hint_range(0.0, 1.0);
uniform float borderOpacity : hint_range(0.0, 1.0);
uniform float borderWidth : hint_range(0.0, 0.1, 0.001);

uniform float PI = 3.141592653589793238462643383279;

void fragment(){
    //vec2 offset = vec2(offsetx, offsety);
	vec2 centre = vec2(0.5, 0.5);
	vec2 centreRelative = UV-centre;
    
	float rad = length(centreRelative);
	float angle = atan(centreRelative.x/centreRelative.y);
	
	//float bd = float(noise_texture.r - dissolve_value)/float(1.0-dissolve_value);
	//bd = clamp(bd, float(0), float(1));
	//float a = main_texture.a;
	/*if(bd < (border_threshold)){
		 main_texture = border_col;
	}*/
	
	float trueTime = TIME*timeOffset;
	//float rotateTime = 2.0*PI*mod(TIME*timeOffsetRotate,1.0);
	
	float lrp = 0.5*sin(0.5 * PI * mod((rad+trueTime)* waves * 2.0, 1.0) );
	lrp+=0.5;
	
	//float lrp2 =cos( 0.5* ((rotateTime)-angle) );
	
	
	lrp = fract(lrp);
	
	lrp *= (maxOpacity-minOpacity);
	lrp+=minOpacity;
	
	if(rad>0.5){
		COLOR = vec4(0,0,0,0);
		
	}else if(rad>(0.5-borderWidth)){
		COLOR = waveCol*borderOpacity;
	}else{
		COLOR = waveCol*lrp;
	}
	
	
	
	
    
}