using System.Drawing;
using Color = System.Drawing.Color;

namespace SampleProject.CellStyles
{
    public static class CellStyle
    {
        //AboveHunderedPercent
        public static ConditionalCellStyle AboveHunderedPercent => _aboveHunderedPercent;
        static ConditionalCellStyle _aboveHunderedPercent = new ConditionalCellStyle() { PositiveZero = 100 };

        //text-bold
        public static BasicStyle TextBold => _textBold;
        static BasicStyle _textBold = new BasicStyle(
          textBold: true, htmlClasses: "text-bold");

        //text-muted
        public static BasicStyle TextMuted => _textMuted;
        static BasicStyle _textMuted = new BasicStyle(
          fontColor: Color.FromArgb(108, 117, 125), htmlClasses: "text-muted");

        //part
        public static BasicStyle PartOrderOld => _partOrderOld;
        static BasicStyle _partOrderOld = new BasicStyle(
            bgColor: Color.FromArgb(253, 244, 246), htmlClasses: "cell-order-part-old");

        public static BasicStyle PartOrderNow => _partOrderNow;
        static BasicStyle _partOrderNow = new BasicStyle(
            bgColor: Color.FromArgb(217, 225, 247), textBold: true, htmlClasses: "cell-order-part-current");

        public static BasicStyle PartOrderNext => _partOrderNext;
        static BasicStyle _partOrderNext = new BasicStyle(
            bgColor: Color.FromArgb(217, 225, 247), htmlClasses: "cell-order-part-next");

        public static BasicStyle PartOrderNew => _partOrderNew;
        static BasicStyle _partOrderNew = new BasicStyle(
            htmlClasses: "cell-order-part-new");

        public static BasicStyle PartStock => _partStock;
        static BasicStyle _partStock = new BasicStyle(
            bgColor: Color.FromArgb(227, 247, 217), htmlClasses: "cell-stock-part");

        public static BasicStyle PartStockTotal => _partStockTotal;
        static BasicStyle _partStockTotal = new BasicStyle(
            bgColor: Color.FromArgb(227, 247, 217), textBold: true, htmlClasses: "cell-stock-part-total");

        //customer
        public static BasicStyle CustomerOrderOld => _customerOrderOld;
        static BasicStyle _customerOrderOld = new BasicStyle(
            Color.FromArgb(217, 37, 80), Color.FromArgb(253, 244, 246), htmlClasses: "cell-order-customer-old");

        public static BasicStyle CustomerOrderNow => _customerOrderNow;
        static BasicStyle _customerOrderNow = new BasicStyle(
            fontColor: Color.FromArgb(217, 37, 80), textBold: true, htmlClasses: "cell-order-customer-current");

        public static BasicStyle CustomerOrderNext => _customerOrderNext;
        static BasicStyle _customerOrderNext = new BasicStyle(
            fontColor: Color.FromArgb(217, 37, 80), htmlClasses: "cell-order-customer-next");

        public static BasicStyle CustomerOrderNew => _customerOrderNew;
        static BasicStyle _customerOrderNew = new BasicStyle(
            htmlClasses: "cell-order-customer-new");

        public static BasicStyle CustomerStock => _customerStock;
        static BasicStyle _customerStock = new BasicStyle(
            fontColor: Color.FromArgb(8, 160, 26), htmlClasses: "cell-stock-customer");

        public static BasicStyle CustomerStockTotal => _customerStockTotal;
        static BasicStyle _customerStockTotal = new BasicStyle(
            fontColor: Color.FromArgb(8, 160, 26), textBold: true, htmlClasses: "cell-stock-customer-total");
    }
}