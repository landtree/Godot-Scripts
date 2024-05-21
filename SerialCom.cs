//install dot6, then run terminal command in dir with godot C# sln: dotnet add package System.IO.Ports --version 7.0.0
//Set to autoload in project settings
using Godot;
using GodotPlugins.Game;
using System;
using System.IO.Ports;

public partial class SerialCom : Node
{	
	SerialPort serialPort;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		//create an empty string
		string PortName = "";
		//populate connected com ports
		string[] comList = System.IO.Ports.SerialPort.GetPortNames();

		//print out connected ports
		for(int n = 0; n<comList.Length; n++)
		{
			GD.Print(comList[n]);
		}

		//pick port based on amount of
		//connected devices, assume last in line 
		//arduino

		if(comList.Length == 1)
		{
			PortName = comList[0];
		}
		if(comList.Length == 2)
		{
			PortName = comList[1];
		}
		if(comList.Length == 3)
		{
			PortName = comList[2];
		}		

		//print out deteced port
		GD.Print("Port Detected: " + PortName);

		//Set serialPort properties
		serialPort = new SerialPort
		{
			PortName = PortName,
			BaudRate = 9600,
			ReadTimeout = 5,
			DiscardNull = true
		};
			//Try to open Serial
			try
			{
				serialPort.Open();
			}
			catch (System.Exception)
			{
				serialPort.Close();
				throw;	
			}
			finally
			{
				serialPort.Open();
			}



			if (serialPort.IsOpen)
			{
				GD.Print("Connected Port");
			}else
			{
				GD.Print("Can't connect");
			}

			GD.Print("Moving on...");

	}
	public string readSerial(string data)
	{	
		//try to read, ingore timeout errors
		//to prevent a flood of debug errors
		try
		{
			data = serialPort.ReadLine();
		}
		catch (System.Exception)
		{
			//ignore timeout errors
		}
			return data;
	}

	public void Send(string outData)
	{	
		//write data to arduino
		serialPort.WriteLine(outData);
	}
}
