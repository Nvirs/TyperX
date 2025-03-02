# TyperX

Typerx is a typing speed test app made with the Avalonia UI framework.

## Features

- **30-second typing test**  
  Type as many random words as you can in 30 seconds.
  
- **Real-time feedback**  
  You get feedback for each word you type:
  - **Jó!:** The word is right.
  - **Hiba!:** The word is wrong.

- **Results display**  
  At the end, you see:
  - **WPM (words per minute)**
  - **Accuracy percentage**

## How It Works

1. **Start the test:**  
   Click the **start** button to begin.

2. **Type the words:**  
   Type the words shown and press **enter** to check your word.

3. **Timer:**  
   The timer goes down from 30 seconds and updates every 100 milliseconds.

4. **Test completion:**  
   The test ends when:
   - The 30-second timer is finished, or
   - All the words are typed.

5. **Results:**  
   When the test ends, your wpm and accuracy are calculated and shown.

## About Avalonia UI

Avalonia UI is a modern user interface framework for .NET. It is similar to WPF and helps developers make nice apps for desktop, mobile, and web.:

- **Cross-platform support:**  
  It runs on Windows, macOS, and Linux.
  
- **Modern architecture:**  
  It uses the MVVM design pattern. This separates the business logic from the user interface.

- **Customizable & themable:**  
  You can change the style to create a unique look.

- **Open source:**  
  It is supported by a strong community.

## Installation & Usage
1. **clone the repository:**
   ```bash
   git clone https://github.com/Nvirs/typerx.git
2. **Navigate to the project directory:**
   ```bash
   cd TyperX
3. **Build and run the project:**
    ```bash
    dotnet run
