// Type: Photon.SocketServer.Mmo.Vector
// Assembly: Photon.SocketServer, Version=2.4.8.1448, Culture=neutral, PublicKeyToken=48c2fa3b6988090e
// Assembly location: D:\Photon\lib\Photon.SocketServer.dll

using System;

namespace Photon.SocketServer.Mmo
{
    public struct Vector : IEquatable<Vector>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        #region IEquatable<Vector> Members

        public bool Equals(Vector other);

        #endregion

        public static Vector operator +(Vector a, Vector b);
        public static Vector operator /(Vector a, int b);
        public static bool operator ==(Vector coordinate1, Vector coordinate2);
        public static bool operator !=(Vector coordinate1, Vector coordinate2);
        public static Vector operator *(Vector a, int b);
        public static Vector operator -(Vector a, Vector b);
        public static Vector operator -(Vector a);
        public static Vector Max(Vector value1, Vector value2);
        public static Vector Min(Vector value1, Vector value2);
        public override bool Equals(object obj);
        public double GetDistance(Vector vector);
        public override int GetHashCode();
        public double GetMagnitude();
        public override string ToString();
    }
}
