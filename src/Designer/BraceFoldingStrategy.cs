using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace FreeSCADA.Designer
{
    public class BraceFoldingStrategy : AbstractFoldingStrategy
    {
        public char OpeningBrace
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get;
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set;
        }
        public char ClosingBrace
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get;
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public BraceFoldingStrategy()
		{
			this.OpeningBrace = '{';
			this.ClosingBrace = '}';
		}
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override System.Collections.Generic.IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            firstErrorOffset = -1;
            return this.CreateNewFoldings(document);
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public System.Collections.Generic.IEnumerable<NewFolding> CreateNewFoldings(ITextSource document)
        {
            System.Collections.Generic.List<NewFolding> list = new System.Collections.Generic.List<NewFolding>();
            Stack<int> stack = new Stack<int>();
            int num = 0;
            char openingBrace = this.OpeningBrace;
            char closingBrace = this.ClosingBrace;
            for (int i = 0; i < document.TextLength; i++)
            {
                char charAt = document.GetCharAt(i);
                if (charAt == openingBrace)
                {
                    stack.Push(i);
                }
                else
                {
                    if (charAt == closingBrace && stack.Count > 0)
                    {
                        int num2 = stack.Pop();
                        if (num2 < num)
                        {
                            list.Add(new NewFolding(num2, i + 1));
                        }
                    }
                    else
                    {
                        if (charAt == '\n' || charAt == '\r')
                        {
                            num = i + 1;
                        }
                    }
                }
            }
            list.Sort((NewFolding a, NewFolding b) => a.StartOffset.CompareTo(b.StartOffset));
            return list;
        }
    }
}
