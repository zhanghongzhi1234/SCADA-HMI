using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace FreeSCADA.Designer
{
	public class AppCompletionDatas
	{
		private System.Collections.Generic.List<MyCompletionData> comlist;
		private System.Collections.Generic.List<MyCompletionData> blankcomlist;
		public MyCompletionData[] List
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				return this.comlist.ToArray();
			}
		}
		public MyCompletionData[] BlankList
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				return this.blankcomlist.ToArray();
			}
		}
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		public AppCompletionDatas()
		{
			this.comlist = new System.Collections.Generic.List<MyCompletionData>();
			this.blankcomlist = new System.Collections.Generic.List<MyCompletionData>();
			/*MyCompletionData item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30240), eYgJk0MPml23SOq7Fh.eyj01t0av(30266));
			this.comlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30320), eYgJk0MPml23SOq7Fh.eyj01t0av(30348));
			this.comlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30378), eYgJk0MPml23SOq7Fh.eyj01t0av(30416));
			this.comlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30432), eYgJk0MPml23SOq7Fh.eyj01t0av(30468));
			this.comlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30484), eYgJk0MPml23SOq7Fh.eyj01t0av(30520));
			this.comlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30550), eYgJk0MPml23SOq7Fh.eyj01t0av(30590));
			this.comlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30620), eYgJk0MPml23SOq7Fh.eyj01t0av(30688));
			this.comlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30718), eYgJk0MPml23SOq7Fh.eyj01t0av(30766));
			this.comlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30796), eYgJk0MPml23SOq7Fh.eyj01t0av(30840));
			this.comlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30870), eYgJk0MPml23SOq7Fh.eyj01t0av(30886));
			this.blankcomlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30902), eYgJk0MPml23SOq7Fh.eyj01t0av(30912));
			this.blankcomlist.Add(item);
			item = new MyCompletionData(eYgJk0MPml23SOq7Fh.eyj01t0av(30922), eYgJk0MPml23SOq7Fh.eyj01t0av(30938));
			this.blankcomlist.Add(item);*/
		}
	}
}
