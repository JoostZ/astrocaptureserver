[wxAstroCapture](http://arnholm.org/astro/software/wxAstroCapture/) is a program that allows you to follow a star in a webcam connected to a telescope mount. There are several ways of using this following to guide a telescope. One of these is through a TCP/IP server. This project implements such a server.

The project consists of three components.
  * A generic server that receives the commands from wxAstroCapture and parses it.
  * An ASCOM driver that uses the guiding commands to control the telescope mount
  * A Windows Forms application that makes the other two components in an application

In addition there is a small test client that emulates wxAstroCapture by sending, on command, a message in the same format that wxAstroCapture would do.

Astrocaptureserver is tested with Wxastrocaptureserver and Boxdoerfer's MTS3 ASCOM driver and we managed to do a succesfull 40min autoguiding session.

See ApplicationInstallation for more details about the installation of the application.


---


20 September 2009: The first release of the AstroCaptureServer version 1.0.0.

This has been tested with wxAstroCapture running on the same computer as the server and over a network.

It has only been tested with the PowerflexMTS3 ASCOM driver.

To use it, download AstroCaptureServer\_1\_0\_0.zip and follow the instructions in ApplicationInstallation to install on your PC. Information on how to use the program can be found in the Wiki page AstroCaptureServerUse.