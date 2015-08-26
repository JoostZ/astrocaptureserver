This page gives a short summary of the use of AstroCaptureServer

# Introduction #

The best way is to first start the application (See ApplicationInstallation). Make sure that the TCP/IP port used in the server is the same as the port used in wxAstroCapture. Normally that will be the case (port 5618).

Then select the ASCOM driver you want to use.

  * Click on **Select** and select your ASCOM driver from the list in the Dialog box that appears. You probably will have to Setup the driver from that window, otherwise the Ok button will be greyed out.

Next start wxAstroCapture and select the Bridge Link (on Options->Guiding interface tab).

Now you should be able to start Auto Guiding from wxAstroCapture and the server will receive messages (shown in the Message received field) and the telescope should move to keep the selected star in the same position.

See the [wxAstroCapture](http://arnholm.org/astro/software/wxAstroCapture/) website for information on how to use Auto Guiding from wxAstroCapture.