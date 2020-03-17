// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OutlineShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _DepthInput ("Depth Texture Input", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent"}
        ZTest Greater
        ZWrite Off
        //Cull Front
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert noshadow alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 4.0

        sampler2D _MainTex;
        sampler2D _DepthInput;
        float4 _MainTex_TexelSize;

        struct Input
        {
            float2 uv_MainTex;
            float4 screenPos;
            float depth;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            COMPUTE_EYEDEPTH(o.depth);
            //o.screenUV = ComputeScreenPos(v.vertex);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //float2 dUV = (IN.screenPos.xy / IN.screenPos.w);
            //dUV.x *= (-(_ScreenParams.x / _ScreenParams.y) + (16 / 9));
            ////dUV.x *= 9/16;
            //float otherDepth = tex2D(_DepthInput, dUV).r;
            //otherDepth = DECODE_EYEDEPTH(otherDepth);
            //float inFront = otherDepth - IN.depth; // negative if in front
            //if (-inFront <= 0)
            //    discard;
            // Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //fixed4 c = tex2D (_DepthInput, IN.screenUV.xy / IN.screenUV.w);
            //fixed4 c = tex2D (_MainTex, IN.screenUV) * _Color;
            float2 suv = IN.screenPos.xy / IN.screenPos.w;
            suv.x *= _ScreenParams.x / _MainTex_TexelSize.z;
            suv.y *= _ScreenParams.y / _MainTex_TexelSize.w;
            //suv.x *= _ScreenParams.x;
            //suv.y *= _ScreenParams.y;
            fixed4 c = tex2D(_MainTex, suv);
            o.Albedo = c.xyz * _Color;
            //o.Albedo = float3(IN.screenUV, 0);
            //o.Albedo = float3(inFront, 0, 0);
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.r;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
