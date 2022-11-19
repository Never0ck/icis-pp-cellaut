namespace Cellaut.Domain
{
    public class Cell : IEquatable<Cell>, ICloneable
    {
        public Guid Id { get; }

        public Cell()
        {
            Id = Guid.NewGuid();
        }

        private Cell(Guid id)
        {
            Id = id;
        }

        public bool IsAlive { get; set; }

        public Cell Togle()
        {
            var cellClone = (Cell)Clone();
            cellClone.IsAlive = !IsAlive;
            return cellClone;
        }

        public bool Equals(Cell? other)
        {
            return Id == other?.Id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            return new Cell(this.Id) { IsAlive = this.IsAlive };
        }
    }
}
