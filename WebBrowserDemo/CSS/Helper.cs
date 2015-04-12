using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CascadingStyleSheets
{
    public static class Helper
    {
        public static Cursor CursorConvert(CursorEnum cursor)
        {
            switch (cursor)
            {
                case CursorEnum.None:
                    return Cursors.None;

                case CursorEnum.No:
                    return Cursors.No;

                case CursorEnum.Arrow:
                    return Cursors.Arrow;

                case CursorEnum.AppStarting:
                    return Cursors.AppStarting;

                case CursorEnum.Cross:
                    return Cursors.Cross;

                case CursorEnum.Help:
                    return Cursors.Help;

                case CursorEnum.IBeam:
                    return Cursors.IBeam;

                case CursorEnum.SizeAll:
                    return Cursors.SizeAll;

                case CursorEnum.SizeNESW:
                    return Cursors.SizeNESW;

                case CursorEnum.SizeNS:
                    return Cursors.SizeNS;

                case CursorEnum.SizeNWSE:
                    return Cursors.SizeNWSE;

                case CursorEnum.SizeWE:
                    return Cursors.SizeWE;

                case CursorEnum.UpArrow:
                    return Cursors.UpArrow;

                case CursorEnum.Wait:
                    return Cursors.Wait;

                case CursorEnum.Hand:
                    return Cursors.Hand;

                case CursorEnum.Pen:
                    return Cursors.Pen;

                case CursorEnum.ScrollNS:
                    return Cursors.ScrollNS;

                case CursorEnum.ScrollWE:
                    return Cursors.ScrollWE;

                case CursorEnum.ScrollAll:
                    return Cursors.ScrollAll;

                case CursorEnum.ScrollN:
                    return Cursors.ScrollN;

                case CursorEnum.ScrollS:
                    return Cursors.ScrollS;

                case CursorEnum.ScrollW:
                    return Cursors.ScrollW;

                case CursorEnum.ScrollE:
                    return Cursors.ScrollE;

                case CursorEnum.ScrollNW:
                    return Cursors.ScrollNW;

                case CursorEnum.ScrollNE:
                    return Cursors.ScrollNE;

                case CursorEnum.ScrollSW:
                    return Cursors.ScrollSW;

                case CursorEnum.ScrollSE:
                    return Cursors.ScrollSE;

                case CursorEnum.ArrowCD:
                    return Cursors.ArrowCD;
                default:
                    return Cursors.Arrow;
            }
        }
    }
}
