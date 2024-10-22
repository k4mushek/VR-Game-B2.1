Shader "DepthMask" {

    SubShader {
        //render AFTER regular geometry but BEFORE masked geometry and transparent things
        Tags {"Queue" = "Geometry-10"}

        Lighting Off

        //this is default but just in case
        ZTest LEqual
        ZWrite On

        //don’t write anything besides depth buffer
        ColorMask 0

        Pass{}
    }
}
