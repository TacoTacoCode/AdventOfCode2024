namespace Day12
{
    public record Block(char Value)
    {
        private readonly HashSet<Element> _items = [];
        public int Count => _items.Count;
        public IEnumerable<Element> HasTops => _items.Where(x => x.HasOpenDirection(Direction.Up));
        public IEnumerable<Element> HasRights => _items.Where(x => x.HasOpenDirection(Direction.Right));
        public IEnumerable<Element> HasBottoms => _items.Where(x => x.HasOpenDirection(Direction.Down));
        public IEnumerable<Element> HasLefts => _items.Where(x => x.HasOpenDirection(Direction.Left));

        public void AddItem(Element el) => _items.Add(el);
    }
}