2015/9/9
Currently, using open source NuGet package called Fleck and HTML5 websocket, is able to meet the requirement that web server sent message to browsers.
Plus, client can sent message to server then allow server broadcast to all clients.

Issues:
1. Need to compare with the other open source libs, list the pros and cons before making decision.
2. Clarification about Req. of CPIS location tracking, 
	CPIS should be able to display the latest loc of all mobile user? 
	Is there a need that different browser should display the same info.
3. Architectural decision: Data Flow, 
	

			Web----------4------AppServer-----------3-------------DBServer 
						|
						|
						2
						|
						|
			Mobile-------1-----WebService

	1. The Mobile use the WebService to send msg.
	2. The WebService pass these msg to AppServer.
	3. The AppServer store these msg to DBServer.
	4. The AppServer push the data to the Web.

	The senquence of this Data Flow should be
		1->2->3->4 or 1->2->3&4