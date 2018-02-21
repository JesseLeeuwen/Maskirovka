
namespace Maskirovka
{
    public struct Change
    {
        public Vector3 reputation;
        public Country countryA;
        public Country countryB;
    }

    public enum Catagorie
    {
        A, B, C
    }

    public enum SelectableType
    {
        News, Country
    }

    struct NewsData
    {
        public int value;
        public Catagorie catagorie;
    }
}