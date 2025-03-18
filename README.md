# ![icon](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/src/Automated_Menu_Ordering_System/Assets/Square44x44Logo.targetsize-24_altform-unplated.png?raw=true) Automated Menu Ordering System

![GitHub](<https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/docs/images/automated_menu_ordering_system (Large).png?raw=true>)

## Table of Contents

- [ Automated Menu Ordering System](#-automated-menu-ordering-system)
  - [Table of Contents](#table-of-contents)
  - [Screenshots](#screenshots)
  - [Introduction](#introduction)
  - [Features](#features)
  - [Technologies](#technologies)
    - [Frontend](#frontend)
    - [Backend](#backend)
  - [Installation](#installation)
    - [Prerequisites](#prerequisites)
      - [Visual Studio 2022](#visual-studio-2022)
      - [Nuget Package Manager](#nuget-package-manager)
    - [Setting up workspace](#setting-up-workspace)
  - [License](#license)

## Screenshots

![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/signin-page.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/customer-place-an-order-dialog-1.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/customer-cart-page.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/customer-home-page-1.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/customer-desserts-page-light.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/filter-dialog-1.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/filter-dialog-2.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/manager-history-page-with-dialog.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/manager-orders-page.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/manager-menu-page.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/admin-products-page.png?raw=true)
![alt text](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/screenshots/settings-page.png?raw=true)

## Introduction

The Automated Menu Ordering System solves issues like long wait times, order mistakes, and reliance on waitstaff by allowing customers to select tables, browse menus, and place orders directly from a table screen. Customer can pay for their food. A manager oversees order fulfillment and overall restaurant operations, while an admin manages the menu, promotional deals, accounts, and branches.

## Features

- **User Authentication:** Secure login and signup for customers and administrators.
- **Dynamic Menu Management:** Admins can easily manage menu items, including names, prices, and descriptions.
- **Order Customization:** Customers can modify their orders with various options like sizes, toppings, and special requests.
- **Order History:** Customers can view past orders and repeat them if needed.
- **Order Tracking:** Customers can track the status of their order, from preparation to delivery.

## Technologies

The Automated Menu Ordering System is built with the following technologies:

### Frontend

- WinUI 3 (for building modern Windows desktop applications)
- XAML (for designing the user interface)

### Backend

- .NET 8 (for building the backend services)
- C# (for writing the application logic)
- PostgreSQL (for managing and storing data)

## Installation

### Prerequisites

Make sure you have the following installed:

#### Visual Studio 2022

- .NET desktop development workload
- Windows application development workload

#### Nuget Package Manager

- Npgsql
- EnvDotNet
- SyncFusion

### Setting up workspace

1. Clone the repository:

   ```bash
   git clone https://github.com/abdxdev/Automated-Menu-Ordering-System
   ```

2. Open the solution file in Visual Studio 2022:

   ```bash
   cd Automated-Menu-Ordering-System/src/
   start Automated_Menu_Ordering_System.sln
   ```

3. Restore the NuGet packages:

   - Right-click on the solution in the Solution Explorer.
   - Select **Restore NuGet Packages**.

4. Set up the environment variables:

   - Create a new file named `.env` in the `Automated-Menu-Ordering-System/src/Automated_Menu_Ordering_System` directory and add the following environment variables:

   ```bash
   DATABASE_CONNECTION='Host=localhost;Port=5432;Username=postgres;Password=your_password;Database=your_database;'
   SYNC_FUSION_LICENSE='your_license_key'
   ```

   _Replace parameters with your PostgreSQL database credentials._

5. Build the solution:

   - Right-click on the solution in the Solution Explorer.
   - Select **Build Solution**.

6. Run the application:

   - Press `F5` to start the application in debug mode.

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/abdxdev/Automated-Menu-Ordering-System/blob/main/LICENSE) file for details.
