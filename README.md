# Counter Prankster - Prank your Friends!

![Project Logo](https://github.com/jbasurtod/counter_prankster/blob/master/lawlwpf/img/counter_prankster.jpg)

**Description:** This is just a fun project I made to counter prank my friends. The story is the following: My friends love to prank you if you leave your laptop logged in Windows, and they do stuff like write on your chats (Slacker, Whatsapp, etc.)
So I made this app that does the following:
- Once you click on Start, it minimizes and stays hidden.
- At the first keydown pressed, it takes a picture with the webcam (works on PCs with webcams).
- Then, it shows the photo taken with the webcam and a GOTCHA! message to the victim for 5 seconds.
- After the 5 seconds, it logs you out of Windows.
- The photo is saved in the Documents folder.

## Features

- It uses GlobalKeyboardHooks to setup the keyboard event that triggers the webcam picture taken. Props to [Stasonix](https://gist.github.com/Stasonix) and [this Stackoverflow thread](https://stackoverflow.com/questions/604410/global-keyboard-capture-in-c-sharp-application) for the ideas.
- A class named PictureMaster that handles the photo taken by the webcam, adding some text, a picture of the laughing kid meme and saves it to the Documents folder.
- Hiding the window until prompted to come back and show the Photo taken by the webcam.
- Overall, a fun project that taught me how to use Keyboard Hooks.

## Installation

Follow these steps to set up the project locally:

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/jbasurtod/counter_prankster.git

## License

This project is licensed under the MIT License.
