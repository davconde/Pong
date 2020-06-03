#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

float Time;
float TimePeriod;
float Speed;
static const float PI = 3.14159265f;

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 Color;
	Color = tex2D(SpriteTextureSampler, input.TextureCoordinates.xy) * float4(1, 0.05, 0.05, 1);
	Color += tex2D(SpriteTextureSampler, input.TextureCoordinates.xy + (0.01)) * float4(0.05, 1, 0.05, 1);
	Color += tex2D(SpriteTextureSampler, input.TextureCoordinates.xy - (0.01)) * float4(0.05, 0.05, 1, 1);
	Color *= cos(input.TextureCoordinates.x * Time * Speed * 2 * PI / TimePeriod);
	Color *= sin(input.TextureCoordinates.y * Time * Speed * 2 * PI / TimePeriod);
	return Color * 4;

	//return tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};