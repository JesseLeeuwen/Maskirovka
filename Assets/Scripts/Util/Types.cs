using UnityEngine;

namespace Maskirovka
{
    public enum Catagorie
    {
        A, B, C
    }

    public enum SelectableType
    {
        News, Country
    }

    public struct NewsData
    {
        public int value;
        public Catagorie catagorie;
    }

    [System.Serializable]
    public struct Neighbour
    {
        public Country neighbour;
        public Vector3 reputation;
    }

    public struct Change
    {
        public Vector3 reputation;
        public Country countryA;
        public Country countryB;
    }
}