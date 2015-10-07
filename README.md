# Cordova Windows Phone (Universal) WNS Azure Notifications Hub with Tags Demo

*Note that at the moment tags only work with Windows devices but iOS and Android tag support will be added shortly to the plugin*

##Run this demo
###1. Create a Notification Hub
- In the Azure Portal, create a new Notification Hub (under Service Bus, then new "Notification Hub")
- Once created, take note of the name of the notification hub. Also, navigate to that notification hub and click the "View Connection String" 
and take note of the "DefaultListenSharedAccessSignature" and "DefaultFullSharedAccessSignature" connection strings.

###2. Create an App
- Clone this repo locally
- Create a new app in [Windows Dev Centre](https://dev.windows.com)
- One done, go to Services | Push notifications and click the "Live Services site" link.
- Take note of the Package SID, Application Identity, Client Id and Client Secret.
- In AzurePushNotificationDemo project:
	- Open the config.xml and:
		- Click the "Windows" tab and set the following:
			- Display Name can be anything you like
			- Package Name MUST be the "Name" attribute of your application identity you got in the store in the step above.
			- Open the config.xml in a text editor and change "vs:publisherId" attribute to your application identity you got in the store in the step above (include the "CN=" text).
- Change the "Solution Platforms" dropdown to "Windows Phone (Universal)".
- Open www/scripts/index.js set the value of the "connectionString" to the "DefaultListenSharedAccessSignature" connection string above and set the "notificationHubPath" variable
to the name of your notifcation hub from above.
- You can also change the name of your notification hub (defaults to "test") by changing the "notificationHubPath" variable.
- You can also change the tags you will register for (defaults to "tag_a", "tag_b", "tab_c") by changing the "tags" variable.

###3. Update the Notification Hub WNS
Open your notification hub in the Windows Azure portal. Change to the configure tab and in the "windows notification settings" set PACKAGE SID to the value of your app in step 2 and
set CLIENT SECRET to the secret you got for your app in step 2. Save those updates.

###4. Run the App
>Due to an issue seeing toasts when the Visual Studio 2015 debugger being attached to an emulator or a device it is best to deploy the app and then detach the debugger before you send messages. With the debugger attached you  will still see messages in the VS Javascript debug console, but you will not see toast notifications.

- Select the AzureNotificationHubsDemo project in the solution.
- Change the "Solution Platforms" dropdown to "Windows Phone (Universal)".
- Select Build | Deploy AzureNotificationHubsDemo to put it on your emulator or device.
- On the phone, open the AzureNotificationHubsDemo app - doing this once registers the phone for notifications. Close the app.
- In the AzureNotificationHubsDemoSender project, open the App.config and set the "connectionstring" to the "DefaultFullSharedAccessSignature" you got in step 1 above. Also set
the "path" to the name of the notification hub you also got in step 1.
- Run the AzureNotificationHubsDemoSender project and "Hello, World" will appear as a toast in your mobile device.


##Steps to recreate this project
###Create a Notification Hub
- In the Azure Portal, create a new Notification Hub (under Service Bus, then new "Notification Hub")
- Once created, take note of the name of the notification hub. Also, navigate to that notification hub and click the "View Connection String" 
and take note of the "DefaultListenSharedAccessSignature" and "DefaultFullSharedAccessSignature" connection strings.

###Create a store app
- Create a new app in [Windows Dev Centre](https://dev.windows.com)
- Once done, go to Services | Push notifications and click the "Live Services site" link.
- Take note of the Package SID, Application Identity, Client Id and Client Secret.

###Create the Cordova App in VS 2015
- Open the config.xml and:
	- Click the "Windows" tab and set the following:
		- Display Name can be anything you like
		- Package Name MUST be the "Name" attribute of your application identity you got in the store in the step above.
	- Change the "Solution Platforms" dropdown to "Windows Phone (Universal)" and Build the project. This will add "Windows" as a platform.
	- Open the config.xml in a text editor and find the section "vs:platformSpecificValues". Add "vs:publisherId" as a child element of "vs:platformSpecificWidget" and set this to the "Publisher" attribute of your application identity you got in the store in the step above (include the "CN=" text).
	- Add an element as follows: <preference name="WindowsToastCapable" value="true" />
- Close config.xml and click on it again to open the visual editor. Change to "Plugins", click "Custom" and check the "Git" option. Enter the following repository https://github.com/ParamountVentures/cordova-plugin-azure-notificationhub.git and click "Add".

- Open index.js in the scrips folder and add the following code at the bottom of the onDeviceReady() method (setting connectionString and notificationHubPath):

        var connectionString = "ENTER YOUR DefaultListenSharedAccessSignature VALUE FROM STEP 1";
        var notificationHubPath = "ENTER YOU NOTIFICATION HUB NAME";

        var hub = new WindowsAzure.Messaging.NotificationHub(notificationHubPath, connectionString);


        hub.registerApplicationAsync().then(function (result) {
            console.log("Registration successful: " + result.registrationId);
        });

        hub.onPushNotificationReceived = function (msg) {
            console.log('onPushNotificationReceived:' + JSON.stringify(msg));
        };

###The sender is based on this sample:
https://azure.microsoft.com/en-gb/documentation/articles/notification-hubs-windows-store-dotnet-get-started/#_send-notification-from-your-back-end