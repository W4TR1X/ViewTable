
namespace w4TR1x.ViewTable.Interfaces
{
    public interface IEntryCell : IEntryCellStyle
    {
        public string Text { get; }
        public string Identifier { get; }

        public bool IsHidden();

    }
}
