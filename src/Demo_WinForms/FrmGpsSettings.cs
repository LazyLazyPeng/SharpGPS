// Copyright 2007 - Morten Nielsen
//
// This file is part of SharpGps.
// SharpGps is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpGps is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpGps; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharpGPS.Demo
{
	public partial class FrmGpsSettings : Form
	{
		string[] ports;
		public FrmGpsSettings()
		{
			InitializeComponent();
			ports = System.IO.Ports.SerialPort.GetPortNames();
			cmbPorts.DataSource = ports;
			LoadFromRegistry();
		}
		private void LoadFromRegistry()
		{
			//TODO: Obtain these values from the registry
			string port = "COM4";
			string BaudRate = "4800";

			for (int i = 0; i < ports.Length; i++)
			{
				if(port==ports[i]) cmbPorts.SelectedIndex = i;
			}
			int baudrate=4800;
			if (int.TryParse(BaudRate, out baudrate))
				tbBaudRate.Text = baudrate.ToString();
			else
				tbBaudRate.Text = "4800";
		}
		public string SerialPort
		{
			get { return cmbPorts.SelectedValue.ToString(); }
		}
		public int BaudRate
		{
			get { return int.Parse(tbBaudRate.Text); }
		}

		public void DisableConfig()
		{
			EnableDisable(false);
		}
		public void EnableConfig()
		{
			EnableDisable(true);
		}
		private void EnableDisable(bool enable)
		{
			cmbPorts.Enabled = enable;
			tbBaudRate.Enabled = enable;
		}
		
		protected override void OnClosing(CancelEventArgs e)
		{
			//TODO: Save settings in registry

			//Prevent disposal of dialog
			e.Cancel = true;
			base.OnClosing(e);
			this.Hide();
		}		
	}
}