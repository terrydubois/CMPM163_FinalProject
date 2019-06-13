// Original source: https://github.com/hecomi/uCurvedScreen

#ifndef CURVED_SCREEN_CGINC
#define CURVED_SCREEN_CGINC

#include "UnityCG.cginc"

// get width of mesh
inline float CurvedScreenGetWidth()
{
    return length(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x));
}

// change vertices to curve mesh
inline void CurvedScreenVertex(inout float3 v, float radius, float width, float thickness)
{
    float angle = width * v.x / radius;
    v.z *= thickness;
    radius += v.z;
    v.z -= radius * (1.0 - cos(angle));
    v.x = radius * sin(angle) / width;
}

#endif