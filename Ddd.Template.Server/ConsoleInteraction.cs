﻿using System;
using Ddd.Template.Contracts.Commands;
using Ddd.Template.Contracts.Events;
using NServiceBus;

namespace Ddd.Template.Server
{
	internal class ConsoleInteraction : IWantToRunAtStartup,
										IHandleMessages<IMessage>,
										IHandleMessages<Command>,
										IHandleMessages<Event>
	{
		public IBus Bus { get; set; }

		public void Run()
		{
			if (!Environment.UserInteractive)
				return;

			Console.Title = ".:: DDD.Template.Server ::.";

			while (true)
			{
				Console.WriteLine("To exit, press 'Q'");
				var cmd = Console.ReadKey().Key.ToString().ToLower();
				Console.WriteLine("");

				switch (cmd)
				{
					case "q":
						Environment.Exit(0);
						break;
				}
			}
		}

		public void Stop()
		{
			Console.WriteLine("Shutting down...");
		}

		public void Handle(IMessage message)
		{
			Handle(message, "Message");
		}

		public void Handle(Command message)
		{
			Handle(message, "Command");
		}

		public void Handle(Event message)
		{
			Handle(message, "Event");
		}

		private static void Handle(object message, string type)
		{
			if (!Environment.UserInteractive)
				return;

			Console.WriteLine("{0} - {1} received, Type: '{2}'", DateTime.Now, type, message.GetType().Name);
		}
	}
}
