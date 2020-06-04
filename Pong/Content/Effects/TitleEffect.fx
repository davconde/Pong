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
	float CycleVar = cos(PI / 2 * Time);
	float AngularFreq = 2 * PI / TimePeriod;
	Color = tex2D(SpriteTextureSampler, input.TextureCoordinates.xy) * float4(1, 0.05, 0.05, 1);
	Color += tex2D(SpriteTextureSampler, input.TextureCoordinates.xy + 0.05 * CycleVar * cos(AngularFreq * (input.TextureCoordinates.x / Speed + Time))) * float4(0.05, 1, 0.05, 1);
	Color += tex2D(SpriteTextureSampler, input.TextureCoordinates.xy - 0.05 * CycleVar * sin(AngularFreq * (input.TextureCoordinates.y / Speed + Time))) * float4(0.05, 0.05, 1, 1);

	float tsw = Time * Speed * AngularFreq;
	Color *= cos(tsw * (input.TextureCoordinates.x - PI / 2 * input.TextureCoordinates.y ));
	return Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};