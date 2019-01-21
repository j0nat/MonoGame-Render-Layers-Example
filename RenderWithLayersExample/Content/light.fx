float screenWidth, screenHeight;

float ambientIntensity;
float4 ambientColor;

float4 lightColor;
float2 lightPosition;
float lightIntensity, lightRadius;

sampler colorMap = sampler_state
{
	Texture = <RenderTarget>;
};

float4 PixelShaderFunction(float4 position : SV_Position, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
	float2 pixelPosition = float2(screenWidth * texCoord.x, screenHeight * texCoord.y);
	float2 direction = float2(lightPosition.x - pixelPosition.x, lightPosition.y - pixelPosition.y);
	float distance = saturate(1 / length(direction) * lightRadius);

	return float4(tex2D(colorMap, texCoord)) * (float4(ambientColor * ambientIntensity) + float4(distance * lightColor * lightIntensity));
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_5_0 PixelShaderFunction();
    }
}