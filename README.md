<table border=0 cellspacing=0 cellpadding=0 width="100%" style='width:100.0%; border-collapse:collapse; border:none'>
 <tr style='height:140'>
  <td width=140 style='padding:0;vertical-align: middle;'>
  <img width=139 height=139 src="Source/NetworkOnlineMonitor/Resources/AboutInfo Source/image0.png">
  </td>
  <td style='padding:0; vertical-align: middle; font-size:20.0pt; font-weight:bold'>
  The Network Online Monitor
  </td>
 </tr>
</table>

## What it does…

Is your internet connection unreliable? You’ve probably called your
internet provider’s support line and maybe they were able to help you,
maybe they even sent out a tech to look at it. But all too often the
response is “Well, it’s working fine now!”

The Network Online Monitor alerts you to failures in your internet
connection and documents the exact time and length of those failures.
This failure log will help your provider troubleshoot the problem –
after it helps you convince them it’s not your imagination! Network
Online Monitor is designed to be as simple as possible and accomplish
this one purpose accurately and thoroughly with the least effort from
you.

## How it works…

Network Online Monitor (*the app*) uses the “Ping” command to test the
response from three public servers operated by Google, Level 3, and
Cloudflare. (See “What’s a Ping?” below for an explanation.) Each server
is pinged in turn at an interval that you can set – normally five
seconds. By default, the app waits 200 milliseconds (2/10 of a second)
for the server to respond – at least 3 times as long as a typical
broadband internet connection should take.

Network Online Monitor pings one server at a time; if the server
responds, it waits the test interval, then pings the next server. If the
server does not respond, Network Online Monitor quickly tries the next
server, then the next. If any of the servers respond, then your
connection must be working. Only when all three servers fail to respond
does the app determine that your connection is down.

By using three servers, the app ensures that the problem is not just
with the server or with some connection on the way to that server, or
that the server isn’t momentarily slow or congested.

Network Online Monitor can detect failures as short as a few seconds in
length, but you can decide how long a failure must be before it really
counts. A short failure of a second or so is not likely to affect your
use of the net and is not of any real concern. You can set how long a
failure must be before the app alerts you to it and records the failure
in its failure log.

### Is it your ISP that failed?

There are two parts to your connection to the internet. The first part
is your local network that connects your computer to the “gateway”
device that links your network to your Internet Service Provider. That
gateway could be a cable modem, a combination modem/router, or a unified
modem/router/WiFi device. Your local connection to that gateway could be
WiFi or wired and includes the hardware you own.

The second part of your connection is from your location to your ISP’s
facility and then out to the whole world of the internet.

Your ISP may deny responsibility for internet connection failures,
suggesting the problem is in your WiFi or elsewhere in your local
network. When your internet connection fails, NOM tests that your local
network is working by sending a ping to your gateway and recording that
response.

The Network Online Monitor display shows your local network status along
with your internet connection status (Figure 1). If your local network
has lost connection to your gateway, then your internet connection will
also fail (Figure 2). If your gateway device responds to Network Online
Monitor but the target internet servers do not respond, then your local
network is working, but your ISP is not connecting you to the internet
(Figure 3).

#### Figure 1 - Internet connection is working

> <img src="Source/NetworkOnlineMonitor/Resources/AboutInfo Source/image1.png" />

#### Figure 1a - Internet connection is working in notifications area

> <img src="Source/NetworkOnlineMonitor/Resources/AboutInfo Source/image2.png" />

#### Figure 2 - Local network not connecting

> <img src="Source/NetworkOnlineMonitor/Resources/AboutInfo Source/image3.png" />

#### Figure 3 - ISP connection failure

> <img src="Source/NetworkOnlineMonitor/Resources/AboutInfo Source/image4.png" />

#### Figure 4 – Network connection failure in notifications area

> <img src="Source/NetworkOnlineMonitor/Resources/AboutInfo Source/image5.png" />

## Display Details

Most of the elements in the display have tooltips to explain the
element’s purpose.

In the “Ping Tests” section, the display shows the names and IP
addresses of each target internet server. The indicator “light” flashes
yellow when the ping is sent and shows green for a successful response.
The response time of the last ping is shown. When the response time
exceeds the time set for “Wait for Ping Response”, the indicator turns
red to show no response from that server.

When the network failure length of the failure exceeds your setting for
“Trigger offline event after…”, the app plays an alert sound and writes
the failure information into its log.

In the “Results” section, the display shows the monitored time (how long
the monitor has been running), the monitor duration, the time since the
last logged network failure, the last network failure duration, and the
network failure count since the monitor has started.

The “Settings” section shows the settings relevant how the network
failure event is determined.

Click the &times; button on the application window title bar to hide it. <span style="color:red">*It
does not close the app!*</span> The app then disappears into your system tray
in the “notifications area”. The app icon is shown in the notification
area – you can hover over the icon to see the monitor duration or upon
network failure, the failure duration. Double-click the icon to restore
the display. Single-click will open a context menu to restore the
display or exit the app. The notification area app icon will turn red
during a network failure. In the Settings, you can choose to have a
“failure alert” sound play, and/or have the app window “pop up”, if a
connection failure exceeds your minimum fault setting. To actually exit
the application, click the red “<span style="color:red">***Exit***</span>” button on the menu bar.

## The Log

This app keeps a log of results in a text file. You can view the current
log at any time by clicking the “Log” menu bar button. The log is opened
in a custom editor window. This window is live and mirrors any changes
in the log file. However, you are also able the make changes and notes
that will also be reflected in the log file.

#### Example Log (Viewing Saved File)

> <img src="Source/NetworkOnlineMonitor/Resources/AboutInfo Source/image6.png" />

1)  The app start time
2)  Current Settings: The target servers to ping.
3)  Current Settings: ping timeout, Interval between pings, and the
    failure duration before notifying and logging. If the settings have
    been changed via the settings dialog, the new settings are written
    to the log.
4)  The WAN internet down event and LAN still connected.
5)  The LAN connection down event. The WAN is down too since it cannot
    be reached through the LAN. These Events do not show up in the log
    until *after* the connection is restored or the app has exited.
6)  A freeform note added via the Log Editor. Warning: Editing the log
    in an external editor while the app is running is possible but may
    cause data loss.
7)  A final summary when the app exits, or log settings have changed.
8)  Log has been closed either by app exit or logging settings changed.

## The Settings

> <img src="Source/NetworkOnlineMonitor/Resources/AboutInfo Source/image7.png" />

Click the *Settings* button in the menu bar to open the Settings window.
There are several settings available:

### Startup Settings…

- *Start when Windows Starts?* – Check the box and the app will
  automatically start when your computer starts. Uncheck the box and you
  can start the app when you want by clicking its desktop icon.

- *Start Minimized in Tray?* – Check the box and the app will be
  minimized in the system tray automatically when it starts.

### Test Settings…

- *Test Interval* – how many seconds between ping tests when the servers
  are responding. Five seconds is the default. It is possible that the
  app will miss a failure that is shorter than the time between tests,
  so if your connection has very frequent failures of just a few seconds
  you might choose a shorter test interval. If you do not have many
  failures, you may want to test less often. Most connection problems
  result in less frequent but longer failures, so five seconds is a good
  choice for most users.

- *Wait for Ping Response* – the length of time the app waits for a
  response after sending a ping. The default setting of 200 milliseconds
  is recommended for normal situations. If you have a slower internet
  connection, such as a satellite, dialup, or mobile connection, or are
  in a remote area where response is typically slow, you can set the
  wait time for up to 2000 milliseconds (2 seconds). To help you find
  the best setting for your situation, set the wait time to 2000
  milliseconds, and observe the ping response times the app displays
  when your connection is working normally. Set the wait time to about
  1.5 times the typical ping response times you see for efficient
  detection of outages.

- *Target Servers* – you can edit the IP Address and Name of any of the
  three servers. Click the Test button to try that server, verifying
  that it responds and checking the response time. The default target
  servers (Google, Level 3, Cloudflare) were selected for their
  performance and extremely high reliability. You should only use a
  different server if you find that one of these servers does not
  respond reliably in your particular situation. Click “Restore
  Defaults” to reset the Target Servers to their original values.

### Alert and Log Settings…

- *Alert and Log Failure If Longer Than* – the minimum failure length
  that will be counted, both for the log and the alert of a failure.
  Five seconds is the default setting.

- *Pop Up on Failure?* – Check the box and the app will pop up from the
  system tray when there is a failure. Uncheck the box and the app will
  continue to log and alert but it will stay minimized during a failure.

- *Log File Option* – New File Each Run (the default), Add to Existing
  File, or None.

    - *New File Each Run* – A new file is created each time the app starts.
      Each log file is named with the date and time the app was started so
      that they will appear in your directory in chronological order. The
      file name is in the form of NetworkOnlineMonitorLog 2020-08-10
      13.42.43.txt. In this example, the date is August 10, 2020 1:42:43 pm.
    
    - *Add to Existing File* – Each new log is added to the same single
      file. The file name is always *NetworkOnlineMonitorLog.txt*. If that
      file exists in the folder where you have chosen to save the log file,
      the app will keep adding to it. If the file doesn’t exist, i.e., it’s
      been deleted, moved, or renamed, the app will start a new file. If the
      file size is greater than 100MB, the file is renamed to
      *NetworkOnlineMonitorLog 2020-08-10 13.42.43.txt* where the date part
      of the filename is the date the file was first opened.
    
    - *None* – All logging disabled. This may automatically occur if the log


- *Log File Location* – the folder where the logs will be stored. Click
  the select folder icon button to browse to the folder you want. The
  log for the current run will be automatically flushed and closed and a
  new log started. The default is your Documents folder.

- *Failure Alert Sound* – Choose the sound the app makes when a failure
  has occurred (i.e., a failure lasting longer than failure trigger).
  Pick from the list of system sounds or provide your own. You can
  preview the sound and adjust its volume. The default is to not play
  any sound. Allowed audio file formats are: \*.aac \*.adt \*.adts
  \*.aif \*.aifc \*.aiff \*.au \*.flac \*.m4a \*.mka \*.mp2 \*.mp3
  \*.snd \*.wav \*.wax \*.wma.

- *Reconnect Sound* – Choose the sound the app makes when your internet
  reconnects after a failure. Pick from the list of system sounds or
  provide your own. You can preview the sound and adjust its volume. The
  default is to not play any sound. Allowed audio file formats are:
  \*.aac \*.adt \*.adts \*.aif \*.aifc \*.aiff \*.au \*.flac \*.m4a
  \*.mka \*.mp2 \*.mp3 \*.snd \*.wav \*.wax \*.wma.

## About/Help

Click the *About* button on the menu bar to view this Help file.

## Combine Settings for “Invisible” Operation

Network Online Monitor can do its job without showing itself or alerting
the user to its operation in any way. Choose these settings:

- Start when Windows Starts? – checked.
- Start Minimized in Tray? – checked.
- Pop Up on Failure – unchecked.
- Choose Failure Alert Sound – None.
- Choose Reconnect Sound – None.

With this combination of settings, the user need never be aware of
Network Online Monitor. This is useful in a support situation where you
are adding Network Online Monitor to a computer you aren’t personally
using.

## What’s a Ping?

“Ping” is a command available on all kinds of computers that tests
whether another computer on the network will respond to your computer.
It’s named after the sound of submarine sonar systems – they send out a
“ping” sound which bounces off their target and they listen for that
echo, locating their target. The internet “ping” works in a similar way.
You name your target, an internet server, and “ping” it. The ping
command and response looks like this (in a command window):

<pre><div style='font-size:10.0pt; line-height: 100%;'>C:\> ping google.com

Pinging google.com [142.251.46.206] with 32 bytes of data:
Reply from 142.251.46.206: bytes=32 time=16ms TTL=116
Reply from 142.251.46.206: bytes=32 time=11ms TTL=116
Reply from 142.251.46.206: bytes=32 time=12ms TTL=116
Reply from 142.251.46.206: bytes=32 time=22ms TTL=116

Ping statistics for 142.251.46.206:
    Packets: Sent = 4, Received = 4, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = 11ms, Maximum = 22ms, Average = 15ms
</div></pre>

A ping command generates four requests and the server replies four
times. Each response is timed in thousandths of a second (ms =
milliseconds). Here we see that the server at google.com responded in
about 31/1000 or 3/100 of a second. The internet is fast! – when
everything is working.

## Installing

Copy the executable to any folder and start it. You should also go into
its settings to configure it.

## Build

Built with Visual Studio 2019, C#, and .NET Framework 4.8. There are no
external dependencies. It consists of a single monolithic executable.
