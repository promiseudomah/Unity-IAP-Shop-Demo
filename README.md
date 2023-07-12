# Unity IAP Shop Demo 🛒🎮

This project is a Unity game with an integrated Unity IAP (In-App Purchases) system. It demonstrates a virtual shop where you can explore and buy different in-game products. You can use this project as a template and easily adjust the settings and keys to fit your own requirements. It serves as a helpful resource to understand how IAP systems work in Unity. The shop connects to your catalog linked with the Google Play Store Developer Console, ensuring a smooth and convenient purchasing experience.

*PS: This project was a great learning experience. Use this if you're new to creating shops with IAPs in Unity 🚀*

## Features 🌟

- Integrated Unity IAP system for in-app purchases
- Browse and purchase products in the virtual shop 🛍️💰
- Connects to your catalog in Google Play Store Developer Console

## Installation 🚀

Follow these steps to utilize the project repository:

### Prerequisites

Before getting started, ensure that you have the following:

- Unity installed on your machine (version 2021.3.9f1 LTS or higher).
- A Google Play Developer account.
- Basic knowledge of Unity and C# programming.

### Step-by-Step Guide

1. **Clone the repository**: https://github.com/promiseudomah/Unity-IAP-Shop-Demo.git

2. **Open the project**: Open the cloned repository in Unity by selecting **File → Open Project** and navigating to the project folder.

3. **Import Unity IAP package**: In Unity, open the Package Manager by selecting **Window → Package Manager**. Search for "Unity IAP" and click on the package to import it into your project.

4. **Set up Google Play Console**:
- Go to the [Google Play Console](https://play.google.com/apps/publish) and sign in using your Google account.
- Create a new application entry for your game by clicking on **Create Application**.
- Fill in the necessary details for your app, such as the title, description, and other required information.
- Configure your payment settings, including pricing and availability.
- Navigate to the **Development Tools → Services & APIs** section and enable the "Google Play Billing Library" for your game.
- Obtain the necessary credentials or keys (e.g., application public key) from the Google Play Console.

5. **Configure Unity IAP settings**:
- In the Unity Editor, go to **Window → Unity IAP → IAP Catalog**.
- Configure your in-app purchase products, prices, and other details according to your game's requirements.
- Ensure that the product IDs in the IAP catalog match the product IDs created in the Google Play Console.

6. **Integrate IAP logic**:
- Open your game's code in Unity and locate the appropriate script or class responsible for handling IAP transactions.
- Implement the logic to retrieve product details from the IAP catalog, display the available products in your virtual shop, handle purchase requests, and grant purchased items to the player.

7. **Build and test**:
- Build your game for the target platform (e.g., Android).
- Install the built APK file on a test device or emulator.
- Test the IAP functionality by making test purchases and verifying that the purchased items are correctly granted to the player.

8. **Publish to Google Play**:
- Once you're satisfied with the functionality and testing, create a signed APK of your game.
- Upload the signed APK to the Google Play Console and follow the platform-specific guidelines for submitting your game to the Google Play Store.

⏹️ End of README
