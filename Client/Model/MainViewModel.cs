using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
	
	public class MainViewModel
	{
		private string _input;
		private string _output;
		public string Input
        {
			get { return _input; }
			set { _input = value; }
        }
		public string Output
		{
			get { return _output; }
			set { _output = value; }
		}

		private string _btnText;
		public string BtnText
        {
			get { return _btnText; }
			set { _btnText = value; }
        }
	}
}
