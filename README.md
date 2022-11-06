<a name="readme-top"></a>
<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Andreas1331/ragemp-wheelnav"></a>

<h3 align="center">RageMP wheelnav C# wrapper</h3>

  <p align="center">
    This is a C# wrapper for RageMP to create CEF wheel menus using the wheelnav.js library.
    <br />
    <a href="https://github.com/Andreas1331/ragemp-wheelnav"><strong>Explore the code</strong></a>
    <br />
    <br />
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#about-the-project">About The Project</a></li>
    <li><a href="#demonstration">Demonstration</a></li>
    <li><a href="#issues">Issues</a></li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>


<!-- ABOUT THE PROJECT -->
## About The Project

The wheelnav.js library by Software Tailoring eases development of interactive wheels that are highly customizable. I wrote a C# wrapper for the GTA:V mod RageMP to control when to display the wheel and hooked server-side actions to each slice in the wheel. This was initially created as a feature for a larger gamemode available at my GitHub. 

My idea was to have one primary wheel from where subwheels could spring from. It is not necessary to create subwheels, but you can simply fill it with slices with actions hooked. The wheel will automatically close and destroy itself when the user releases the designated key that you supply when instantiating the primary wheel. 

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- DEMONSTRATION -->
## Demonstration
This is a showcase from my roleplay gamemode that used raycasting to grab the vehicle which the player was looking at. Using the handle of the vehicle we can create a wheel dedicated to controlling the vehicle.
<p align="center"><img src="images/wheelnav-demo.gif" width="75%"/></p>

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ISSUES -->
## Issues

The id system for the wheels got a bit messy, and the idea of having the programmer set the ids himself can cause issues... So if you choose to use this for anything I'd suggest creating a factory for wheels that handles ids internally to avoid it.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

To get started move the GenericWheel-Client folder to your client_packages and ensure you have included the ```GenericWheel-Client/js/index.js``` in your own index.js. You'd probably also need to include the server-side files in your namespace. 

Finally to create your first wheel, please follow the steps below.

1) We first have to instantiate our primary wheel, and we supply it with id 0.
The id will be used by subwheels to reference which wheel they will return to.

```
var wheel = new PrimaryInteractionWheel(0, "My wheel", keyToBind: "c");
```

2) Next, create a subwheel by instantiating an ```InteractionWheel```. We then add this subwheel to the list of subwheels on our primary wheel. The primary wheel now knows of the subwheel and will be able to navigate to it.
```
var extraWheel = new InteractionWheel(1, "Walkingstyle");
wheel.AddSubWheel(extraWheel);
```

3) Now that we have our primary and subwheel set up we will create two slices for our primary wheel. One will be a simple action slice that takes an ```Action``` which will be invoked if the user clicks on the slice. The other slice is used to open subwheels. We supply that slice with the id of our subwheel.
```
var slices = new List<object>()
    {
        new WheelSliceAction("icon.play", () => player.SendChatMessage("You pressed on a play icon slice")),
        new WheelSliceSubMenu("icon.list", extraWheel.ID)
    };
wheel.Slices = slices;
```

4) We will now add a few slices to our subwheel. Notice that we also add a ```WheelSliceSubMenu``` to our subwheel itself in order to give the user the opportunity to navigate back to our primary wheel.
```
var extraSlices = new List<object>()
    {
        new WheelSliceAction("Normal", () => player.SetSharedData("walkingStyle", "Normal")),
        new WheelSliceAction("Brave", () => player.SetSharedData("walkingStyle", "Brave")),
    };
extraSlices.Add(new WheelSliceSubMenu("icon.arrowleft", wheel.ID));
extraWheel.Slices = extraSlices;
```

5) Finally we just need to call the display method of our primary wheel to show the user our wheel.
```
wheel.Display(player);
```

As soon as the user releases the c-key the menu will destroy itself. So this means for the library to properly function you'd probably only show a wheel if a user actively holds down a key. Meaning if the user at some point holds the c-key you let the server-side know. I suggest taking a look at my gamemode.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Prerequisites

Make sure to have the following at hand, or you will have to rewrite to javascript.
* RageMP server with C# backend

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

Andreas  - **Website to be inserted**

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [The wheelnav.js library for making this possible](http://wheelnavjs.softwaretailoring.net/)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
