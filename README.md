JenkinsConfig
=============

Tools used to configure the [Jenknis](http://jenkins-ci.org/) continuous integration server.


### SetStream.exe
Used to set the AccuRev stream for a job. 

#### Usage Text:
usage: SetStream.exe &lt;xml file path&gt; &lt;stream name&gt;  
example: SetStream.exe config.xml v8.5.1  
example: SetStream.exe c:\temp\config.xml "Temp Fix"  

#### Example: 

Note: I setup at least two jobs.

![ScreenShot](https://raw.github.com/Digitalroot/JenkinsConfig/Screenshots/design.png)

The first one is a configure job which changes the settings of the second job.  
Use a few batch command steps in my configue job.

REM Delete XML files.  
del config.xml  
del config.xml.1  

REM Download current config file.  
c:\bin\wget.exe "http://myserver:8080/job/DemoJob/config.xml"  

REM Set Stream Version  
c:\bin\SetStream.exe "config.xml" "%STREAM_NAME%"  

REM Upload config  
c:\bin\wget.exe --post-file=config.xml "http://myserver:8080/job/DemoJob/config.xml"  

The first job then starts the second job.  
