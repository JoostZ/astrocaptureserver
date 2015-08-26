How to do Autoguiding with wxAstroCapture and Astrocaptureserver

# Introduction #

AstrocaptureServer is Windows software intended to communicate with the Bridge interface of [wxAstrocapture](http://arnholm.org/astro/software/wxAstroCapture/).
AstrocaptureServer serves as a TCP/IP server receiving Bridge Interface guiding commands from wxAstroCapture. It forwards the guiding instruction to any ASCOM supported telescope controller using its ASCOM driver.

# The ASCOM interface #

The ASCOM interface is started from within Astrocaptureserver. Click _Select_ to activate the ASCOM Telescope Chooser window. Choose the type of telescope you have in the drop-down box (its ASCOM driver must be installed on your computer) and click _OK_. The connection with your telescope mount is established and Astrocaptureserver will forward the autoguiding messages to the ASCOM driver when received from wxAstrocapture.

# The TCP/IP interface #

Astrocaptureserver can run on the same computer as wxAstroCapture, but also on another computer in a network. You can for example run wxAstroCapture on Linux and Astrocaptureserver on Windows.
To specify that Astrocaptureserver is running on the same computer as wxAstroCapture, specify localhost for the Bridge interface host in wxAstrocapture. Both programs use TCP/IP port 5618 by default for connections. You can alternatively specify the host as the IP-address or name of another computer in your network where Astrocaptureserver is running.

http://astrocaptureserver.googlecode.com/svn/images/ServerManual/image1.JPG          http://astrocaptureserver.googlecode.com/svn/images/ServerManual/image2.JPG

The left image above shows how to specify the server's TCP/IP address and port in wxAstrocapture. The right image shows where to change the default TCP/IP port in Astrocaptureserver.

To establish communication between Astrocaptureserver (server) and wxAstrocapture (client) the server should be started **first**. After starting autoguiding in wxAstrocapture the connection is established and Astrocaptureserver will receive autoguiding messages. See the image below:

http://astrocaptureserver.googlecode.com/svn/images/ServerManual/image3.JPG

# Autoguiding in wxAstrocapture #

Start wxAstroCapture and make sure the Bridge Link (on Options->Guiding interface tab) is selected.
Connect to the webcam. Optionally activate long exposure mode. Make sure a star is visible in the shown webcam frame.

Now you must determine the proper lower cutoff level in order to have wxAstrocapture recognize the guide star. A good starting value is 20-30 less than the value shown as the level in the exposure meter in the Record Tab. In most cases this will be a proper value. Specify this value as lower cutoff level in _Options-->Guiding-->Lower Cutoff Level_. Click OK.

Next go to the Guide Tab and click _Pick Guide Star_. Click on the star image you want to use as the autoguide star. If the Lower Cutoff level is OK, a small square will be drawn around the guiding star. If the Lower cutoff level is too high, this error message will appear _"Could not lock onto the selected guide star"_. If the Lower cutoff level is too low, the square will be drawn, but it will move around the screen as it cannot recognize the guide star and will interpret noise or hot pixels as guide stars.
Change he lower cutoff level as needed to obtain a lock on the selected guide star.

First, you need to do an autocalibration. This is a one-time calibration that need not be repeated as long as the webcam is not moved and the telescope is not changed. Autocalibration determines:
  1. the angle between the webcam's long axis and the E-W direction.
  1. the scale of the webcam image: arcsec per pixel
Both values are needed to send the proper correction messages to the telescope drives through the Astrocaptureserver.

_(add autocalibration explanation here)_