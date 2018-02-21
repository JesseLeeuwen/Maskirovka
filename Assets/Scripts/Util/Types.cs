
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

    struct NewsData
    {
        public int value;
        public Catagorie catagorie;
    }
}