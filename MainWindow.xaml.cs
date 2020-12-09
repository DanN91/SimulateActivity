using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;

namespace SimulateActivity
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			// To stop screen saver and monitor power off event
			// You can combine several flags and specify multiple behaviors with a single call
			SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
		}

		#region Handlers
		private void OnStateChanged(object sender, System.EventArgs e)
		{
			if (WindowState == WindowState.Minimized)
			{
				// To reset or allow those event again you have to call this API with only ES_CONTINUOUS
				SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS); // This will reset as normal
			}
			else
			{
				// To stop screen saver and monitor power off event
				// You can combine several flags and specify multiple behaviors with a single call
				SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
			}
		}
		#endregion

		#region Helpers
		[FlagsAttribute]
		public enum EXECUTION_STATE : uint
		{
			ES_AWAYMODE_REQUIRED = 0x00000040,
			ES_CONTINUOUS = 0x80000000,
			ES_DISPLAY_REQUIRED = 0x00000002,
			ES_SYSTEM_REQUIRED = 0x00000001
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
		#endregion
	}
}
