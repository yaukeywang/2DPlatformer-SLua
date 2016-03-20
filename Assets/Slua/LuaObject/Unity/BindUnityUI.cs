using System;
using System.Collections.Generic;
namespace SLua {
	[LuaBinder(1)]
	public class BindUnityUI {
		public static Action<IntPtr>[] GetBindList() {
			Action<IntPtr>[] list= {
			};
			return list;
		}
	}
}
