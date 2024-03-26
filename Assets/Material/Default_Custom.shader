// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites/Default_Costum"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _AdditiveColor ("Additive Color", Color) = (0,0,0) // ���Z�J���[�̃v���p�e�B�ǉ�
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag_Custom  // �Ăяo�����֐���ς���
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"

            fixed3 _AdditiveColor; // ���Z�J���[�̃����o��`
            // ��{�I�ɂ�UnitySprites.cginc����R�s�[
            sampler2D _MainTex_Custom;
            fixed4 SpriteFrag_Custom(v2f IN) : SV_Target
            {
                // �Ƃ肠�����w��F��������
                fixed4 c = SampleSpriteTexture(IN.texcoord) * _Color;
                // �w��F��RGB�e�l��0.5�Ȃ猳�̐F�A0.5�𒴂���Ɣ��ɋ߂Â��悤�ɒ���
                c.rgb = c.rgb + _AdditiveColor;
                c.rgb *= c.a;
                return c;
            }
        ENDCG
        }
    }
}