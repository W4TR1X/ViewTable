using w4TR1x.ViewTable.Interfaces;
using System;
using System.Linq;
using w4TR1x.ViewTable.Enums;
using System.Collections.Generic;

namespace w4TR1x.ViewTable
{
    public class Cell : ICell
    {
        public IRow Parent { get; set; }

        public TextPositionEnum TextPosition { get; set; }
        public virtual List<ICellStyle> Styles { get; set; }

        public List<ICellValue> Values { get; }
        public string Identifier { get; }


        public bool Hidden { get; set; }


        public string Title { get; set; }

        public string PopupTitle { get; set; }
        public string PopupText { get; set; }

        public int? CustomOrderValue { get; set; }

        private int colSpan = 1;

        public Cell(ICellStyle style = null, string identifier = "", params ICellValue[] values)
        {
            Styles = new List<ICellStyle>();

            for (int i = 0; i < values.Count(); i++)
            {
                Styles.Add(style);
            }

            TextPosition = TextPositionEnum.Center;

            Identifier = (identifier ?? "").Length == 0 ? $"cell_{Guid.NewGuid().ToString().Replace("-", "")}" : identifier;
            Values = values.ToList();
        }

        public int ColSpan
        {
            get
            {
                return colSpan < 1 ? 1 : colSpan;
            }
            set
            {
                colSpan = value > 0 ? value : 1;
            }
        }

        public int RowSpan { get; set; }

        public bool CanPopup()
        {
            return (PopupTitle != null && PopupTitle.Any()) || (PopupText != null && PopupText.Any());
        }
        public virtual bool IsHidden(bool calculating = false)
        {
            bool hiddenCheck()
            {
                return !(Values != null && Values.Where(x => x.Value.ToString() != string.Empty).Any());
            }

            if (calculating)
            {
                return hiddenCheck();
            }
            else
            {
                return Hidden || hiddenCheck();
            }
        }

        public ICellValue GetValue(int renderIndex)
        {
            if (Values.Count > renderIndex)
            {
                return Values[renderIndex] ?? null;
            }

            return Values.FirstOrDefault() ?? null;
        }
        public string GetValueAsString(int renderIndex)
        {
            if (Values.Count > renderIndex)
            {
                return Values[renderIndex].ToString() ?? string.Empty;
            }

            return Values.FirstOrDefault()?.ToString() ?? string.Empty;
        }
        public object GetOrderValue(int renderIndex)
        {
            if (CustomOrderValue.HasValue)
            {
                return CustomOrderValue.Value;
            }

            if (Values.Count > renderIndex)
            {
                return Values[renderIndex].AsOrderValue() ?? null;
            }

            return Values.FirstOrDefault()?.AsOrderValue() ?? null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
