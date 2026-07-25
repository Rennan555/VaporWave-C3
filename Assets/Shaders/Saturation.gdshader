shader_type canvas_item;

uniform float saturation: hint_range(-10.0, 10.0, 0.01) = 1.0 ;

mat4 saturation_matriz(float s){
	vec3 luminance = vec3(0.3086, 0.6094, 0.0820);
	
	float sr = (1.0 - s) * luminance.r;
	float sg = (1.0 - s) * luminance.g;
	float sb = (1.0 - s) * luminance.b;
	
	return mat4 (
		vec4(sr+s, sr, sr, 0),
		vec4(sg, sg+s, sg, 0),
		vec4(sb, sb, sb+s, 0),
		vec4(0, 0, 0, 1)
	);
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec4 color = texture(TEXTURE, UV);
	mat4 matriz = saturation_matriz(saturation);
	COLOR = matriz * color;
}
