Shader "BuildingEffect"
{
	Properties
	{
		_Diffuse("Diffuse", 2D) = "white" {}
		[HideInInspector][IntRange]_Progress("Progress", Range( 0 , 100)) = 100
		_Metalic("Metalic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _Diffuse;
		float _Progress;

		UNITY_INSTANCING_BUFFER_START(BuildingEffect)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Diffuse_ST)
#define _Diffuse_ST_arr BuildingEffect
			UNITY_DEFINE_INSTANCED_PROP(float, _Metalic)
#define _Metalic_arr BuildingEffect
			UNITY_DEFINE_INSTANCED_PROP(float, _Smoothness)
#define _Smoothness_arr BuildingEffect
		UNITY_INSTANCING_BUFFER_END(BuildingEffect)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _Diffuse_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_Diffuse_ST_arr, _Diffuse_ST);
			float2 uv_Diffuse = i.uv_texcoord * _Diffuse_ST_Instance.xy + _Diffuse_ST_Instance.zw;
			o.Normal = UnpackNormal( tex2D( _Diffuse, uv_Diffuse ) );
			o.Albedo = tex2D( _Diffuse, uv_Diffuse ).rgb;
			float _Metalic_Instance = UNITY_ACCESS_INSTANCED_PROP(_Metalic_arr, _Metalic);
			o.Metallic = _Metalic_Instance;
			float _Smoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Smoothness_arr, _Smoothness);
			o.Smoothness = _Smoothness_Instance;
			o.Alpha = 1;
			float temp_output_72_0 = ( ( ( i.vertexColor.r + ( i.vertexColor.g * 100 ) ) + ( i.vertexColor.b * 10000 ) ) * 10.0 );
			float3 temp_cast_1 = (temp_output_72_0).xxx;
			float3 temp_cast_2 = (temp_output_72_0).xxx;
			float3 temp_cast_3 = (saturate( ( 1.0 - ( ( distance( temp_cast_2 , float3( 0,0,0 ) ) - 0.0025 ) / max( 25.0 , 1E-05 ) ) ) )).xxx;
			clip( ( saturate( ( 1.0 - ( ( distance( temp_cast_1 , temp_cast_3 ) - 0.0025 ) / max( 25.0 , 1E-05 ) ) ) ) / 0.01 ) - _Progress );
		}

		ENDCG
	}
	Fallback "Diffuse"
}
