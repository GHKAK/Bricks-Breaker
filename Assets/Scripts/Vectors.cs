using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vectors {
    public static Vector2 topLeft = new Vector2(1, -1).normalized;
    public static Vector2 topRight = new Vector2(1, 1).normalized;
    public static Vector2 downLeft = new Vector2(-1, -1).normalized;
    public static Vector2 downRight = new Vector2(-1, 1).normalized;
} 