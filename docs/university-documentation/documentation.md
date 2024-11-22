## Abstract

The Automated Menu Ordering System solves issues like long wait times, order mistakes, and reliance on waitstaff by allowing customers to select tables, browse menus, and place orders directly from a table screen. Customer can pay for their food. A manager oversees order fulfillment and overall restaurant operations, while an admin manages the menu, promotional deals, accounts, and branches.

## Introduction

Many restaurants struggle with long wait times, order errors due to human intervention, and high labor costs, which can negatively impact customer satisfaction and overall efficiency. The reliance on waitstaff for taking orders can lead to delays and mistakes, affecting the dining experience.

The Automated Menu Ordering System addresses these challenges by providing several advantages. It eliminates the need for waitstaff, allowing customers to sit at a table and start browsing the menu using a screen at their table, where they can place orders directly. Customers can customize their orders, view the status of their orders, and pay for their meals through the system. This automated process reduces wait times, minimizes order errors, and enhances the overall dining experience.

The platform includes various functionalities, such as a comprehensive menu display that allows customers to view food items, read descriptions, and check prices. The system enables customers to customize their orders based on preferences, such as portion sizes or ingredient modifications.

The manager oversees order fulfillment and overall restaurant operations, ensuring smooth kitchen functionality and addressing any service issues. Meanwhile, the admin manages the menu and promotional deals, with the ability to update or modify items as necessary to provide customers with the latest offerings. Both the manager and admin must sign up and log in, ensuring secure access and control over their respective areas.

## Problem Statement

Traditional restaurant ordering systems can cause inefficiencies, delays, and errors. Customers experience long waits, incorrect orders, and limited status updates, while manual processing increases mistakes and slows service.

## Objectives

1. **Enhanced Customer Experience:**
   Allow customers to browse menus, place orders, and pay directly from their table.
2. **Streamlined Order Processing:**
   Automatically send orders to the counter, reducing wait times and minimizing errors.
3. **Order Customization & Tracking:**
   Enable customers to customize orders and receive real-time order status updates.
4. **Customer Feedback:**
   Let customers rate their meals and provide feedback.
5. **Waitstaff Assistance:**
   Allow customers to call a waiter for assistance when needed.
6. **Manager Oversight:**
   Provide managers with the ability to oversee and track customer orders at the counter.
7. **Admin Control:**
   Allow admins to manage food products, deals, accounts, and branches.
8. **User-Friendly Interface:**
   Offer light and dark mode options for customer comfort.

## Tools and Technologies

The Automated Menu Ordering System will be developed using the following tools and technologies

### Core Development Tools

1. **[WinUI3](https://learn.microsoft.com/en-us/windows/apps/winui/winui3/):** For building modern Windows apps.
2. **[XAML](https://learn.microsoft.com/en-us/visualstudio/xaml-tools/xaml-overview):** For designing user interfaces.
3. **[C#](https://docs.microsoft.com/en-us/dotnet/csharp/):** For backend development.

### Database and Storage

1. **[PostgreSQL](https://www.postgresql.org/):** For database storage.
2. **[Supabase](https://supabase.io/):** For database hosting and management.
3. **[Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/):** For database queries and management.

### Version Control and Collaboration

1. **[Hackmd](https://hackmd.io/):** For collaborative writing and sharing.
2. **[Online Gannt](https://onlinegantt.com/):** For project management and scheduling.
3. **[GitHub](https://github.com/):** For version control and collaboration.
4. **[WhatsApp](https://www.whatsapp.com/):** For communication and updates.

### Design and Prototyping

1. **[WinUI3 Gallery](https://github.com/microsoft/WinUI-Gallery):** For UI components and controls.
1. **[Figma](https://www.figma.com/):** For wireframing and prototyping.
1. **[Exalidraw](https://excalidraw.com/):** For creating rough diagrams and sketches.
1. **[Adobe Illustrator](https://www.adobe.com/products/illustrator.html):** For illustrations and design.

### Development Environments

1. **[Visual Studio](https://visualstudio.microsoft.com/):** For software development.
2. **[Visual Studio Code](https://code.visualstudio.com/):** For code editing and documentation.

### Documentation

1. **[Microsoft Office Suite](https://www.microsoft.com/en-us/microsoft-365/microsoft-office):** For documentation.

### Hardware Tools

1. **Tablet or Touch Screen:** For customers to browse menus and place orders.
2. **Computer or Laptop:** For managers and admins to oversee operations and manage the system.
3. **Internet Connection:** To enable communication between devices and the central system.

## Vision

Our vision is to create a seamless and efficient dining experience for customers and restaurant staff by automating the ordering process, reducing wait times, and enhancing customer satisfaction.

## Scope and Features

Our Automated Menu Ordering System aims to simplify the dining experience for customers by allowing them to select a table and browse the menu for ordering and customization options as desired. Customers can conveniently monitor their orders. Settle payments directly via the system while also providing feedback post meal and requesting assistance from staff if necessary. Moreover, managers have access to an overview of all orders to effectively oversee restaurant operations and maintain functionality throughout. Administrators have the ability to modify the menu items and manage offers while also taking care of user accounts and supervising branches all from multiple locations. The interface is designed for convenience, with choices between dark mode[^1] and user friendly navigation to ensure an experience[^2], for both customers and employees.

## Requirements

### Functional Requirements

#### Actors

There are three actors

- Admin
- Manager
- Customer

#### Admin Functional Requirements

- The admin shall add menu items, including descriptions and prices.
- The admin shall update menu items, including descriptions and prices.
- The admin shall delete menu items.
- The admin shall create promotional deals.
- The admin shall modify promotional deals.
- The admin shall remove promotional deals.
- The admin shall manage user accounts.
- The admin shall add user accounts.
- The admin shall update user accounts.
- The admin shall delete user accounts for customers.
- The admin shall delete user accounts for managers.
- The admin shall manage multiple restaurant branches.
- The admin shall assign roles to managers.
- The admin shall assign permissions to managers.

#### Customer Functional Requirements

- The customer shall view the menu with detailed item descriptions.
- The customer shall view prices.
- The customer shall view item availability.
- The customer shall place orders directly from a table screen.
- The customer shall customize their orders.
- The customer shall receive real-time updates on order status.
- The customer shall pay for their orders through integrated payment options.
- The customer shall rate their meals.
- The customer shall provide feedback.
- The customer shall call for waiter assistance through the interface.

#### Manager Functional Requirements

- The manager shall track customer orders.
- The manager shall oversee customer orders.
- The manager shall view the history of customer orders.
- The manager shall monitor the availability of menu items.
- The manager shall update inventory levels.
- The manager shall receive notifications for customer call requests.

### Non-Functional Requirements

#### Usability

- The system must provide a consistent interface with familiar icons and labels to ensure ease of use and minimize confusion.
- The system must offer a light and dark mode option, allowing users to toggle based on their preference.
- The system must support region-specific currencies, ensuring users can make payments in their local currency.

#### Reliability

- The system must maintain an uptime of 99.9% during restaurant operating hours to ensure continuous service.
- Daily and incremental backups must be automatically performed to safeguard user data.
- Users must have access to reliable recovery tools in case of system or data failures.
- Data must be securely stored in off-site locations to protect against local incidents.

#### Performance

- The system must provide sub-second response times for all user interactions, including menu navigation and order placement.
- Real-time order updates must be visible to both customers and staff members.
- The system must handle hundreds of concurrent users without performance degradation.
- Backend queries must be optimized for fast data retrieval, ensuring smooth operation during peak usage.
- The system must undergo regular load testing to maintain consistent performance under varying traffic levels.

#### Security

- Strong password policies must be enforced, requiring passwords of at least 8 characters with alphanumeric combinations.
- Payment information must be secured through tokenization to prevent unauthorized access.
- All sensitive user data must be encrypted both during transmission and at rest, providing comprehensive security for personal, account, and payment information.

#### Scalability

- The system must support the seamless addition of new restaurant branches without causing disruptions.
- Cloud-hosted databases, such as PostgreSQL, must be utilized to efficiently handle increased data and traffic.
- Auto-scaling features must be implemented to accommodate traffic spikes during peak times.
- A reliable internet connection must be ensured for real-time order synchronization.

#### Availability

- The system must ensure that databases are hosted in geographically appropriate locations, such as in India, to comply with local regulations, reduce latency, and improve data access speeds for users in the region.
- Regular backups must be stored securely in both primary and secondary locations to ensure data availability in case of a local incident.

#### Maintainability

- The system must have a modular design, allowing individual components like the UI, database, and payment gateways to be updated independently without affecting the entire system.
- Comprehensive user guides and technical documentation must be provided and regularly updated for employees and developers.
- Version control systems, such as GitHub, must be used for managing codebase changes, allowing easy tracking of changes and rollbacks when necessary.

### Business Rules

1. **Order Accuracy**: All customer orders must be double-checked by the system before final submission to minimize order errors.
2. **Promotional Deals**: Only administrators are allowed to create and manage promotional deals, and these promotions must be visible to customers immediately after being activated.
3. **Customer Feedback**: Customers may submit feedback once their order is marked as completed.
4. **Order Processing Time**: Orders must be processed within a maximum time to ensure smooth kitchen operations.
5. **Branch Expansion**: The system must be capable of scaling to accommodate additional restaurant branches without major code changes or performance issues.
6. **Payment Processing**: Payments must be processed in real-time, and the system must support most used payment methods to ensure fast transactions.

### Business Requirements

1. **Reduce Customer Order Errors**: Implement an order verification feature that ensures all customer orders are verified for accuracy before submission, reducing order errors by 10% within the first year.
2. **Manage Promotional Deals**: Build an admin interface that allows authorized personnel to create, activate, and deactivate promotional deals, contributing to a 10% sales increase and Rs. 1,000,000 in revenue within the first year.
3. **Improve Customer Satisfaction**: Add features such as real-time order updates and post-order feedback submission, aiming for a 5% improvement in customer satisfaction ratings.
4. **Increase Order Processing Efficiency**: Develop an order management system that streamlines order processing, reducing the time taken by 20%, which will result in faster kitchen operations and service.
5. **Scalable for Expansion**: Ensure the system can easily scale to support at least 2 additional branches, contributing a potential Rs. 15,000,000 in annual revenue per branch.
6. **Streamline Payment Process**: Implement a fast and secure payment gateway that reduces the payment processing time by 30%, increasing table turnover and adding Rs. 500,000 in yearly revenue.
7. **Boost Customer Retention**: Develop a personalized promotions system and loyalty rewards feature, aiming for an 8% increase in customer retention and Rs. 400,000 in additional yearly revenue from repeat customers.

### Physical Requirements

1. **Table Screens or Tablets**: Each table must have a dedicated device for customers to browse menus, place orders, and make payments.
2. **Laptops/PCs**: Managers and administrators must have laptops or PCs to monitor, manage, and update system operations including order tracking.
3. **Internet Connection**: A high-speed internet connection must be available to ensure smooth data synchronization across all devices.
4. **Server or Cloud Infrastructure**: The system must utilize a cloud-hosted server (e.g., Supabase) to store orders, user accounts, and other system data securely.

### Development Constraints

1. **Windows App Development**: The app must be built using WinUI 3 and the Windows App SDK to ensure modern and high-performance Windows applications[^3].
2. **Fluent Design**: The app must follow Fluent Design System principles, incorporating elements like light, depth, motion, material, and scale for a fluid and intuitive experience[^4].
3. **WinUI 3 Gallery & Template Studio**: Use the WinUI 3 Gallery for controls and Windows Template Studio for predefined templates to speed up development and ensure design consistency[^5][^6].
4. **Windows 11 signature experiences**: Follow the Windows 11 signature experiences guidelines for app design, including Mica, rounded corners, and Acrylic for a modern and cohesive look[^7].
5. **Responsive Layouts**: The app must adapt to different screen sizes and devices using Adaptive Triggers in WinUI to ensure a smooth experience across form factors[^8].
6. **Performance Optimization**: Ensure the app performs efficiently by minimizing startup times and resource usage, using XAML Compiled Bindings for faster data binding[^9].

## Wireframes

### Navigation
/docs/images/wireframes

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Navigation.png)

| Component | Admin                                                   | Manager                                                  | Customer                                                  |
| --------- | ------------------------------------------------------- | -------------------------------------------------------- | --------------------------------------------------------- |
| T1        | As an admin, I shall navigate to the previous page      | As a manager, I shall navigate to the previous page      | As a customer, I shall navigate to the previous page      |
| T2        | As an admin, I shall open and close the navigation pane | As a manager, I shall open and close the navigation pane | As a customer, I shall open and close the navigation pane |
| T3        | As an admin, I shall minimize the window                | As a manager, I shall minimize the window                | As a customer, I shall minimize the window                |
| T4        | As an admin, I shall maximize the window                | As a manager, I shall maximize the window                | As a customer, I shall maximize the window                |
| T5        | As an admin, I shall close the window                   | As a manager, I shall close the window                   | As a customer, I shall close the window                   |
| T6        | As an admin, I shall view the contents of a page        | As a manager, I shall view the contents of a page        | As a customer, I shall view the contents of a page        |
| T7        | As an admin, I shall navigate to different pages        | As a manager, I shall navigate to different pages        | As a customer, I shall navigate to different pages        |
| T8        | As an admin, I shall navigate to settings               | As a manager, I shall navigate to settings               | As a customer, I shall navigate to settings               |

### Settings Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Settings.png)

| Component | Admin                                                                              | Manager                                                                             | Customer                                                                             |
| --------- | ---------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------ |
| T1        | As an admin, I shall use radio buttons to select the theme (Light, Dark, Default). | As a manager, I shall use radio buttons to select the theme (Light, Dark, Default). | As a customer, I shall use radio buttons to select the theme (Light, Dark, Default). |
| T2        | As an admin, I shall have a button to sign out of the application.                 | As a manager, I shall have a button to sign out of the application.                 | As a customer, I shall have a button to sign out of the application.                 |
| T3        | As an admin, I shall have a link to the privacy policy.                            | As a manager, I shall have a link to the privacy policy.                            | As a customer, I shall have a link to the privacy policy.                            |

### Sign In Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Signin.png)

| Component | Admin                                                       | Manager                                                    |
| --------- | ----------------------------------------------------------- | ---------------------------------------------------------- |
| T1        | As an admin, I shall change the role to manager or customer | As a manager, I shall change the role to admin or customer |
| T2        | As an admin, I shall enter the username                     | As a manager, I shall enter the username or table ID       |
| T3        | As an admin, I shall enter the password                     | As a manager, I shall enter the password                   |
| T4        | As an admin, I shall check the "Keep me signed in" box      | As a manager, I shall check the "Keep me signed in" box    |
| T5        | As an admin, I shall go to learn more                       | As a manager, I shall go to learn more                     |
| T6        | As an admin, I shall sign in                                | As a manager, I shall sign in                              |

### Home Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Customer-Home.png)

| Component | Customer                                                                      |
| --------- | ----------------------------------------------------------------------------- |
| T1        | As a customer, I shall see a Call for Help button.                            |
| T2        | As a customer, I shall browse a Carousel of Deals by swiping to left or right |
| T3        | As a customer, I shall scroll through Top Trending Items.                     |
| T4        | As a customer, I shall scroll through Top Rated Items.                        |
| T5        | As a customer, I shall tap to check out a deal.                               |
| T6        | As a customer, I shall tap to view more details of a deal.                    |
| T7        | As a customer, I shall tap to add an item to the cart.                        |
| T8        | As a customer, I shall tap to discover more via links to different pages.     |

### Category Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Customer-Category.png)

| Component | Customer                                                                    |
| --------- | --------------------------------------------------------------------------- |
| T1        | As a customer, I shall scroll through the list of items in the subcategory. |

### Cart Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Customer-Cart.png)

| Component | Customer                                                                                       |
| --------- | ---------------------------------------------------------------------------------------------- |
| T1        | As a customer, I shall see a list of items in my cart that are not yet ordered.                |
| T2        | As a customer, I shall view a list of items that are already ordered.                          |
| T3        | As a customer, I shall access my order history with a list of past orders.                     |
| T4        | As a customer, I shall have a bin icon to remove an item that has not been ordered yet.        |
| T5        | As a customer, I shall have a button to complete my pending order, displaying the total price. |
| T6        | As a customer, I shall have a button to write a review for an ordered item.                    |

### Call for Help Dialog

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Customer-CallForHelpDialog.png)

| Component | Customer                                                      |
| --------- | ------------------------------------------------------------- |
| T1        | As a customer, I shall use cancel button to close the dialog. |

### Add to Cart Dialog

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Customer-AddToCartDialog.png)

| Component | Customer                                                                             |
| --------- | ------------------------------------------------------------------------------------ |
| T1        | As a customer, I shall select the item size (Small, Medium, Large).                  |
| T2        | As a customer, I shall adjust the quantity of the item using plus and minus buttons. |
| T3        | As a customer, I shall select toppings using radio buttons (No, Yes).                |
| T4        | As a customer, I shall tap to select a card of topping.                              |
| T5        | As a customer, I shall choose toppings from multiple selectable cards.               |
| T6        | As a customer, I shall have a button to add the item to my cart.                     |
| T7        | As a customer, I shall have a button to cancel and close the dialog.                 |

### Sign out Dialog

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Customer-SignoutDialog.png)

| Component | Customer                                                       |
| --------- | -------------------------------------------------------------- |
| T1        | As a customer, I shall call the manager to enter the password. |
| T2        | As a customer, I shall enter the manager's password.           |

### Deal Details Dialog

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Customer-DealDetailsDialog.png)

| Component | Customer                                         |
| --------- | ------------------------------------------------ |
| T1        | As a customer, I shall tap to cancel the dialog. |

### Rate Item Dialog

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Customer-RateItemDialog.png)

| Component | Customer                                         |
| --------- | ------------------------------------------------ |
| T1        | As a customer, I shall tap to rate the item.     |
| T2        | As a customer, I shall tap to close the dialog.  |
| T3        | As a customer, I shall tap to submit the rating. |

### Orders Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Manager-Orders.png)

| Component | Manager                                                                                        |
| --------- | ---------------------------------------------------------------------------------------------- |
| T1        | As a manager, I shall use this button to select all items.                                     |
| T2        | As a manager, I shall use this button to deselect all items.                                   |
| T3        | As a manager, I shall use this button close the selected items.                                |
| T4        | As a manager, I shall order the items by any of the columns.                                   |
| T5        | As a manager, I shall search for specific items by any of the columns.                         |
| T6        | As a manager, I shall use this button clear the search results.                                |
| T7        | As a manager, I shall use this button to search for items.                                     |
| T8        | As a manager, I shall change the status of the item to "Ready", "Completed", or "In Progress". |
| T9        | As a manager, I shall see a grid view of all items.                                            |
| T10       | As a manager, I shall select an item                                                           |

### History Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Manager-History.png)

| Component | Manager                                                           |
| --------- | ----------------------------------------------------------------- |
| T1        | As a manager, I shall pick a date to view the history of orders.  |
| T2        | As a manager, I shall use this button to clear the selected date. |

### Menu Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Manager-Menu.png)

| Component | Manager                                                               |
| --------- | --------------------------------------------------------------------- |
| T1        | As a manager, I shall use this button to mark an item as in-stock     |
| T2        | As a manager, I shall use this button to mark an item as out-of-stock |

### Accounts Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Admin-Accounts.png)

| Component | Admin                                                               |
| --------- | ------------------------------------------------------------------- |
| T1        | As an admin, I shall use this button to add a new account.          |
| T2        | As an admin, I shall use this button to remove an existing account. |
| T3        | As an admin, I shall use this button to update an existing account. |
| T4        | As an admin, I shall use this button to write a custom query.       |

### Branches Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Admin-Branches.png)

| Component | Admin                                                              |
| --------- | ------------------------------------------------------------------ |
| T1        | As an admin, I shall use this button to add a new branch.          |
| T2        | As an admin, I shall use this button to remove an existing branch. |
| T3        | As an admin, I shall use this button to update an existing branch. |

### Tables Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Admin-Tables.png)

| Component | Admin                                                             |
| --------- | ----------------------------------------------------------------- |
| T1        | As an admin, I shall use this button to add a new table.          |
| T2        | As an admin, I shall use this button to remove an existing table. |
| T3        | As an admin, I shall use this button to update an existing table. |

### Products Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Admin-Products.png)

| Component | Admin                                                               |
| --------- | ------------------------------------------------------------------- |
| T1        | As an admin, I shall use this button to add a new product.          |
| T2        | As an admin, I shall use this button to remove an existing product. |
| T3        | As an admin, I shall use this button to update an existing product. |

### Deals Page

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Admin-Deals.png)

| Component | Admin                                                            |
| --------- | ---------------------------------------------------------------- |
| T1        | As an admin, I shall use this button to add a new deal.          |
| T2        | As an admin, I shall use this button to remove an existing deal. |
| T3        | As an admin, I shall use this button to update an existing deal. |
| T4        | As an admin, I shall use this button to edit the deal items.     |

### Deal Items Dialog

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Admin-DealItemsDialog.png)

| Component | Admin                                                                          |
| --------- | ------------------------------------------------------------------------------ |
| T1        | As an admin, I shall use this button to add a new item to the deal.            |
| T2        | As an admin, I shall use this button to remove an existing item from the deal. |
| T3        | As an admin, I shall use this button to close the dialog.                      |

### Write custom query Dialog

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/wireframes/Admin-WriteCustomQueryDialog.png)

| Component | Admin                                                      |
| --------- | ---------------------------------------------------------- |
| T1        | As an admin, I shall write a custom query.                 |
| T2        | As an admin, I shall use this button to execute the query. |
| T3        | As an admin, I shall use this button to close the dialog.  |

## Use Cases

### Use Case 1: Sign In

#### Primary Actors

- Admin
- Manager

#### Goal

To allow the admin, manager, or customer to sign in to the system.

#### Precondition

The user must have a valid account and is on the sign-in page.

#### Postcondition

The user is must authenticated and granted access to the system based on their role.

#### Trigger

The user enters their credentials and clicks the sign-in button.

#### Basic Flow

- The system displays the sign-in page with option to select the role (admin, manager, or customer).
- The user selects the role (admin, manager, or customer).
- The user enters their username and password.
- If the user selects the customer role, the system asks for the table ID.
- The user clicks the sign-in button.
- If the user is an admin, the system logs in the user and redirects to the admin dashboard.
- If the user is a manager, the system logs in the user and redirects to the manager dashboard.

#### Alternative Flow

- None

#### Quality

- Users should sign in quickly and securely, even with multiple sign-ins happening at once.
- If credentials are incorrect, show a helpful, clear error message.
- The user experience should be smooth and responsive on any device.

#### Exception Flow

- If the user enters invalid credentials, the system displays an error message and prompts the user to try again.

### Use Case 2: Sign Out

#### Primary Actors

- Admin
- Manager
- Customer

#### Goal

To allow the admin, manager, or customer to sign out of the system.

#### Precondition

The user is must logged in to the system and is on the settings page.

#### Postcondition

The user is must logged out of the system and redirected to the sign-in page.

#### Trigger

The user clicks the sign-out button.

#### Basic Flow

- The user goes to the settings page.
- The user clicks the sign-out button.
- If the user is a customer, the system asks for manager's password to confirm sign out.
- The user enters the manager's password.
- The system logs out the user and redirects to the sign-in page.

#### Alternative Flow

- If the user is an admin or manager, the system logs out the user and redirects to the sign-in page.

#### Quality

- Signing out should be quick and simple for users.
- The system should handle multiple sign-outs smoothly without issues.

#### Exception Flow

- If the user cancels the sign-out process, the system remains on the settings page.
- If the user enters an incorrect password, the system displays an error message and prompts the user to try again.
- If the user is a customer, then he needs manager permission to sign-out.

### Use Case 3: Browse Menu

#### Primary Actor

Customer

#### Goal

To allow the customer to browse the restaurant's menu.

#### Precondition

The customer is must seated at the table with the system in customer mode.

#### Postcondition

The customer must view the menu items and select items to view details.

#### Trigger

The customer selects one of the main menu categories.

#### Basic Flow

- The customer selects a category from the navigation pane.
- The customer can scroll through the list and view details of each item.

#### Alternative Flow

- If the customer needs assistance, they can call for help using the system.

#### Quality

- The interface should be easy to use, clear to read, and accessible to everyone.
- Customers should be able to switch between categories and view items effortlessly.
- The system should work smoothly, even with many people browsing at the same time.

#### Exception Flow

- If an item is out of stock, the system displays a notification to inform the customer.
- Customers can call for help if they have any issues with the menu.

### Use Case 4: Browse Deals and Promotions

#### Primary Actor

Customer

#### Goal

To allow the customer to view available deals and promotions.

#### Precondition

The customer is must seated at the table with the system in customer mode.

#### Postcondition

The customer can view details of available deals and promotions.

#### Trigger

The customer clicks on the "Home" button from the navigation pane.

#### Basic Flow

- The user navigates to the deals and promotions section in homepage.
- The user can scroll through the carousel of deals and promotions.
- The user can taps on a deal to view more details.

#### Alternative Flow

- The customer can call for help if they have any issues with the deals or promotions.

#### Quality

- Promotional details should be clear, with easy options to learn more or go straight to checkout.
- Real-time promotions should be shown accurately, with no inconsistencies.
- Customers should be able to add promotional items to their cart without any issues.

#### Exception Flow

None

### Use Case 5: Add Item to Cart

#### Primary Actor

Customer

#### Goal

To allow the customer to add an item to the cart.

#### Precondition

The customer must have selected an item from the menu.

#### Postcondition

The item is must added to the cart.

#### Trigger

The customer selects the "+" button to open the add to cart dialog.

#### Basic Flow

- The customer selects an item from the menu by tapping on '+' icon.
- The customer selects the size and quantity of the item.
- The customer chooses any additional toppings or customizations.
- The customer confirms the order by clicking the "Add to Cart" button.

#### Alternative Flow

- The customer can cancel the order by clicking the "Cancel" button.

#### Quality

- Choosing size, quantity, and toppings should be easy and responsive.
- Items should appear in the cart instantly, with no lag.
- Even if multiple customers add items at the same time, there should be no issues or conflicts.

#### Exception Flow

- If the customer tries to add an item that is out of stock, the system displays an error message.
- Customers can call for help if they have any issues with their order.

### Use Case 6: Place Order

#### Primary Actor

Customer

#### Goal

To allow the customer to place an order.

#### Precondition

The customer must have items in the cart.

#### Postcondition

The order is must sent to the manager's interface for processing.

#### Trigger

The customer clicks the "Pay price" button to complete the order.

#### Basic Flow

- The customer navigates to the cart page.
- The customer reviews the order and clicks the "Pay price" button.
- The system processes the payment and sends the order to the manager.
- The customer receives a confirmation by placing the order in "Orders" section.

#### Alternative Flow

- The customer can remove items from the cart before placing the order.

#### Quality

- Customer orders should be processed quickly and securely.
- After placing an order, customers should get clear confirmation and an estimated wait time.
- Customers should easily edit or remove items before finalizing their order.

#### Exception Flow

- If there are no items in the cart, the system displays a message indicating no items to order.
- If the payment fails, the system displays an error message and prompts the customer to try again.
- Customers can call for help if they have any issues with their order.

### Use Case 7: View Order Status

#### Primary Actor

Customer

#### Goal

To allow the customer to view the status of their order.

#### Precondition

The customer must have placed an order.

#### Postcondition

The customer can track the progress of their order.

#### Trigger

The customer clicks on the "Orders" section.

#### Basic Flow

- The system displays the list of current and past orders.
- The system shows the status of each order (e.g., estimated time, ready, completed).

#### Alternative Flow

None

#### Quality

- Customers should get real-time updates on their order status.
- The system should handle many users checking their orders at the same time without any issues.

#### Exception Flow

- If there are no current orders, the system displays a message indicating no orders in progress.
- Customers can call for help if they have any issues with their order.

### Use Case 8: Call for Help

#### Primary Actor

Customer

#### Goal

To allow the customer to call for help from the waitstaff.

#### Precondition

The customer must need assistance.

#### Postcondition

The waitstaff is must notified of the customer's request for help.

#### Trigger

The customer clicks on the "Call for Help" button.

#### Basic Flow

- The customer taps on the "Call for Help" button.
- The system displays the call for help dialog.
- The customer waits for the waitstaff to respond.

#### Alternative Flow

None

#### Quality

- Waitstaff should be notified quickly to ensure a prompt response.
- The system should manage multiple help requests smoothly, without delays or mix-ups.

#### Exception Flow

- If the waitstaff does not respond promptly, the customer can try calling again.
- If the issue is urgent, the customer can call the restaurant directly.
- If the customer's issue is resolved, they can cancel the call for help.

### Use Case 9: Provide Feedback

#### Primary Actor

Customer

#### Goal

To allow the customer to provide feedback on their dining experience.

#### Precondition

The manager must have changed the order status to "Completed."

#### Postcondition

The feedback is must submitted for review.

#### Trigger

The customer clicks on the "Write a Review" button.

#### Basic Flow

- The customer taps on the "Rate this item" button.
- The system displays the review dialog with option to rate the meal.
- The customer selects a star rating.
- The customer taps on the "Submit" button to submit the feedback.

#### Alternative Flow

None

#### Quality

- Feedback should be securely submitted and stored.
- Star ratings and text fields should be user-friendly and responsive.

#### Exception Flow

- The customer can cancel the review process if they change their mind.

### Use Case 10: Manage Orders

#### Primary Actor

Manager

#### Goal

To allow the manager to oversee and manage customer orders.

#### Precondition

The manager is must logged in to the system.

#### Postcondition

The manager can track order status such as in-progress, ready, completed or, close the order.

#### Trigger

The manager clicks on the "Orders" section from the navigation pane.

#### Basic Flow

- The manager navigates to the orders page.
- The manager changes the status of an order (e.g., from in-progress to ready).
- The manager closes an order once it is completed.

#### Alternative Flow

None

#### Quality

- The system should show orders in real time, so managers can track them without delays.
- Managers should be able to change order statuses smoothly, with updates for customers happening right away.
- The system should manage multiple orders at the same time without any data conflicts.

#### Exception Flow

None

### Use Case 11: Order History

#### Primary Actor

Manager

#### Goal

To allow the manager to view the history of customer orders.

#### Precondition

The manager is must logged in to the system.

#### Postcondition

The manager can access past orders for reporting and analysis.

#### Trigger

The manager clicks on the "History" section from the navigation pane.

#### Basic Flow

- The manager navigates to the order history page.
- The manager can view details of each order, including items, total price, and customer feedback.

#### Alternative Flow

None

#### Quality

- The order history should be thorough, giving managers access to all relevant details.
- The system should load order history data quickly, even when dealing with large amounts of information.

#### Exception Flow

- If there are no past orders, the system displays a message indicating no history available.

### Use Case 12: Manage Availability of Menu Items

#### Primary Actor

Manager

#### Goal

To allow the manager to monitor and update the availability of menu items.

#### Precondition

The manager is must logged in to the system.

#### Postcondition

The manager can update the availability of menu items by marking them as in-stock or out-of-stock.

#### Trigger

The manager clicks on the "Menu" section from the navigation pane.

#### Basic Flow

- The manager navigates to the menu page.
- The manager can update the availability status of menu items (e.g., in stock, out of stock).

#### Alternative Flow

None

#### Quality

- The system should let managers update menu item availability instantly, with no delays.
- The interface should be user-friendly, making it easy to view and change the status of multiple items quickly.
- Availability updates should be shown right away for customers browsing the menu.

#### Exception Flow

None

### Use Case 13: Manage Menu

#### Primary Actor

Admin

#### Goal

To allow the admin to manage the restaurant's menu.

#### Precondition

The admin is must logged in to the system.

#### Postcondition

The admin can add, edit, or delete menu items.

#### Trigger

The admin clicks on the "Products" section from the navigation pane.

#### Basic Flow

- The admin navigates to the menu management page.
- The admin can add a new item to the menu.
- The admin can edit the details of an existing item.
- The admin can delete an item from the menu.

#### Alternative Flow

None

#### Quality

- The system should enable admins to manage the menu efficiently, with changes showing up in real time.
- The interface should allow for fast navigation, sorting, and searching of menu items.

#### Exception Flow

- If the admin tries to update or delete an item that is associated with an active order, the system displays a warning message.
- If the admin tries to delete an item that is part of a promotional deal, the system displays a warning message.
- If the admin tries to add an item that already exists in the menu, the system displays an error message.

### Use Case 14: Manage Promotional Deals

#### Primary Actor

Admin

#### Goal

To allow the admin to manage promotional deals.

#### Precondition

The admin is must logged in to the system.

#### Postcondition

The admin can create, edit, or delete promotional deals.

#### Trigger

The admin clicks on the "Deals" section from the navigation pane.

#### Basic Flow

- The admin navigates to the deals and promotions page.
- The admin can create a new deal by specifying the details.
- The admin can edit the details of an existing deal.
- The admin can delete a deal from the list.

#### Alternative Flow

None

#### Quality

- Promotions should update instantly in the customer interface.
- Protects deals linked to active orders from deletion and provides clear warnings.
- The system should handle multiple users managing deals at the same time without any issues.

#### Exception Flow

- If the admin tries to delete a deal that is associated with an active order, the system displays a warning message.
- If the admin tries to create a deal with the same name as an existing deal, the system displays an error message.

### Use Case 15: Manage User Accounts

#### Primary Actor

Admin

#### Goal

To allow the admin to manage user accounts.

#### Precondition

The admin is must logged in to the system.

#### Postcondition

The admin can add, edit, or delete user accounts.

#### Trigger

The admin clicks on the "Accounts" section from the navigation pane.

#### Basic Flow

- The admin navigates to the user account management page.
- The admin can add a new user account by specifying the details such as name, role, and password.
- The admin can edit the details of an existing user account.
- The admin can delete a user account from the list.

#### Alternative Flow

None

#### Quality

- The system should enable user account management with as few steps as possible, making onboarding quick and easy.
- Account details should be handled securely, with role-based access enforced throughout the system.

#### Exception Flow

- If the admin tries to delete an account that is associated with a branch, the system displays a warning message.
- If the admin tries to create an account with the same username as an existing account, the system displays an error message.

### Use Case 16: Manage Branches

#### Primary Actor

Admin

#### Goal

To allow the admin to manage multiple restaurant branches.

#### Precondition

The admin is must logged in to the system.

#### Postcondition

The admin can add, edit, or delete branch information.

#### Trigger

The admin clicks on the "Branches" section from the navigation pane.

#### Basic Flow

- The admin navigates to the branch management page.
- The admin can add a new branch by specifying the details such as branch name, address, and contact information.
- The admin can edit the details of an existing branch.
- The admin can delete a branch from the list.

#### Alternative Flow

None

#### Quality

- The system should allow for efficient management of multiple branches, with updates happening in real time.
- Branch data should be easy to edit and manage, featuring clear options for sorting and searching.

#### Exception Flow

- If the admin tries to delete a branch that is associated with active orders or user accounts, the system displays a warning message.
- If the admin tries to add a branch with the same name or address as an existing branch, the system displays an error message. The admin is logged in to the system.

### Use Case 17: Manage Table Assignments

#### Primary Actor

Admin

#### Goal

To allow the admin to manage table assignments.

#### Precondition

The admin is must logged in to the system.

#### Postcondition

The admin can assign tables to specific branches.

#### Trigger

The admin clicks on the "Tables" section from the navigation pane.

#### Basic Flow

- The admin navigates to the table assignment page.
- The admin can assign a table to a specific branch.
- The admin can reassign a table to a different branch.
- The admin can remove a table assignment.

#### Alternative Flow

None

#### Quality

- The table assignment process should be fast and easy to use, with updates happening in real time.
- The system should handle table assignments and reassignments promptly, with no delays.
- It should prevent assigning tables to branches that don’t exist.

#### Exception Flow

- If the admin tries to assign a table that is already assigned, the system displays a warning message.
- If the admin tries to remove a table assignment that is associated with active orders, the system displays a warning message.
- If the admin tries to assign a table to a branch that does not exist, the system displays an error message.

### Use Case 18: Change Theme

#### Primary Actors

- Admin
- Manager
- Customer

#### Goal

To allow users to change the theme of the application.

#### Precondition

The user is must logged in to the system.

#### Postcondition

The user's theme preference must be updated.

#### Trigger

The user clicks on the "Settings" section from the navigation pane.

#### Basic Flow

- The user navigates to the settings page.
- The user can select the desired theme (e.g., light, dark, default).

#### Alternative Flow

- Managers and admins can change the theme for the entire system.

#### Quality Requirements

- Immediate and final application of theme changes.
- Theme changes should not affect application functionality or performance.
- Prevents conflicts with ongoing sessions during theme changes.

#### Exception Flow

None

### Use Case 19: View Privacy Policy

#### Primary Actors

- Admin
- Manager
- Customer

#### Goal

To allow users to view the privacy policy of the application.

#### Precondition

The user is must logged in to the system.

#### Postcondition

The user can access the privacy policy.

#### Trigger

The user clicks on the "Settings" section from the navigation pane.

#### Basic Flow

- The user navigates to the settings page.
- The user clicks on the privacy policy link to view the policy.

#### Alternative Flow

None

#### Quality Requirements

- The system should ensure that the privacy policy is displayed in a secure, read-only format.

#### Exception Flow

None

## User Stories

### Admin

#### Sign In

- As an admin,\
  I want to securely sign in to the system,\
  So that I can access the admin interface and manage the restaurant's operations.

#### Sign Out

- As an admin,\
  I want to sign out of the system,\
  So that I can log out and return to the sign in page.

#### Manage Menu

- As an admin,\
  I want to add menu items,\
  So that I can keep the restaurant's menu updated for customers.
- As an admin,\
  I want to edit menu items,\
  So that I can keep the restaurant's menu updated for customers.
- As an admin,\
  I want to delete menu items,\
  So that I can keep the restaurant's menu updated for customers.

#### Manage Promotional Deals

- As an admin,\
  I want to create promotional deals,\
  So that I can keep up with current promotional deals.
- As an admin,\
  I want to edit promotional deals,\
  So that I can keep up with current promotional deals.
- As an admin,\
  I want to delete promotional deals,\
  So that I can keep up with current promotional deals.

#### Manage User Accounts

- As an admin,\
  I want to add user accounts,\
  So that I can manage access to the system for managers and other admins.
- As an admin,\
  I want to edit user accounts,\
  So that I can manage access to the system for managers and other admins.
- As an admin,\
  I want to delete user accounts,\
  So that I can manage access to the system for managers and other admins.

#### Manage Branches

- As an admin,\
  I want to add branch information,\
  So that I can manage multiple restaurant branches.
- As an admin,\
  I want to edit branch information,\
  So that I can manage multiple restaurant branches.
- As an admin,\
  I want to delete branch information,\
  So that I can manage multiple restaurant branches.

#### Manage Table Assignments

- As an admin,\
  I want to manage table assignments,\
  So that I can assign tables to specific branches.
- As an admin,\
  I want to reassign tables,\
  So that I can reassign tables to different branches.
- As an admin,\
  I want to remove table assignments,\
  So that I can manage table assignments efficiently.

#### Change Theme

- As an admin,\
  I want to change the theme of the application,\
  So that I can customize the interface to my preference and enhance my user experience.

#### View Privacy Policy

- As an admin,\
  I want to view the privacy policy of the application,\
  So that I can understand how my data is handled and ensure my privacy is protected.

### As a Manager

#### Sign In as Manager

- As a manager,\
  I want to sign in to the system,\
  So that I can access the manager interface and oversee restaurant operations.

#### Sign In as Customer

- As a manager,\
  I want to sign in as a customer,\
  So that I can set up a table for dining and place orders.

#### Sign Out

- As a manager,\
  I want to sign out of the system,\
  So that I can securely log out after completing my tasks.

#### Manage Orders

- As a manager,\
  I want to manage customer orders,\
  So that I can track order status and ensure timely processing.

#### Order History

- As a manager,\
  I want to view the history of customer orders,\
  So that I can analyze past data and identify trends.

#### Manage Availability of Menu Items

- As a manager,\
  I want to monitor and update the availability of menu items,\
  So that I can ensure accurate information for customers.

#### Change Theme

- As a manager,\
  I want to change the theme of the application,\
  So that I can customize the interface to my preference and enhance my user experience.

#### View Privacy Policy

- As a manager,\
  I want to view the privacy policy of the application,\
  So that I can understand how my data is handled and ensure my privacy is protected.

### Customer

#### Browse Menu

- As a customer,\
  I want to view the restaurant's menu,\
  So that I can select items to order for my meal.

#### Browse Deals

- As a customer,\
  I want to view available deals,\
  So that I can add deal items to my cart for purchase.

#### Browse Deal Details

- As a customer,\
  I want to view details of a specific deal,\
  So that I can learn more about the deal and decide if I want to order it.

#### Add Item to Cart

- As a customer,\
  I want to add an item to my cart,\
  So that I can place an order for the selected item.

#### Place Order

- As a customer,\
  I want to place an order for the items in my cart,\
  So that I can complete my meal purchase and receive my order.

#### View Order Status

- As a customer,\
  I want to view the status of my order,\
  So that I can track the progress of my meal preparation.

#### Call for Help

- As a customer,\
  I want to call for help from the waitstaff,\
  So that I can get assistance with my order or any other issues.

#### Provide Feedback

- As a customer,\
  I want to provide feedback on my dining experience,\
  So that I can share my thoughts and help improve the restaurant's service.

#### Change Theme

- As a customer,\
  I want to change the theme of the application,\
  So that I can customize the interface to my preference and enhance my user experience.

#### View Privacy Policy

- As a customer,\
  I want to view the privacy policy of the application,\
  So that I can understand how my data is handled and ensure my privacy is protected.

## Story Boards

### Admin Story Boards

The following storyboards illustrate the user interactions for the admin interface, including managing accounts, branches, tables, products, deals, and custom queries.

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/storyboards/Admin-Storyboards.svg)

### Manager Story Boards

The following storyboards illustrate the user interactions for the manager interface, including managing orders, viewing history, and changing item status.

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/storyboards/Manager-Storyboards.svg)

### Customer Story Boards

The following storyboards illustrate the user interactions for the customer interface, including browsing the menu, adding items to the cart, and placing orders.

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/storyboards/Customer-Storyboards.svg)

### Common Story Boards

The following storyboards illustrate common interactions across all user roles, including signing in, signing out and settings.

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/storyboards/Common-Storyboards.svg)

### Combined Story Boards

The following storyboards illustrate the combined interactions for all user roles, including admin, manager, and customer interfaces.

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/storyboards/Combined-Storyboards.svg)

## Proposed Methodology/System

We are using the **Agile methodology** with an **iterative and incremental approach** for the development of the Automated Menu Ordering System. This ensures user feedback, continuous testing, and improvement throughout the process.

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/other-diagrams/AgileMethodology.png)

### Key Steps

1. **Define Scope and Requirements**: Clearly outline project goals, gather requirements, and create user stories and use cases.
2. **Plan the Product**: Develop a product roadmap and prioritize features based on importance.
3. **Brainstorm Ideas**: Collaborate on ideas, features, and design concepts to shape the product.
4. **Develop in Cycles**: Build the product in short iterations, focusing on delivering essential features first.
5. **Deliver the Product**: Release versions of the product in stages, gather feedback, and refine accordingly.
6. **Review Incrementally**: Evaluate each product increment, collect feedback, and adjust for the next iteration.
7. **Plan the Next Iteration**: Use feedback and new requirements to plan upcoming development cycles.
8. **Ensure Product Acceptance**: Verify that the product aligns with user needs and expectations.
9. **Release the Final Product**: Launch the completed product to users, ensuring it meets all requirements.

## Related Work

### Ziosk Tabletop Tablets

Ziosk tabletop tablets are used in restaurants to provide customers with digital menus, ordering, and payment options. Customers can browse the menu, customize their orders, and pay directly from the tablet. This system has been successful in improving order accuracy, reducing wait times, and increasing customer satisfaction[^10][^11].

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/pics/ZioskTabletopTablets.png)

### McDonald’s Self-Service Kiosks

Offers self-service kiosks for ordering food. Customers can browse the menu, customize their orders, and pay directly at the kiosk. This system has improved order accuracy and reduced wait times for customers. Though they are usually positioned in the restaurant rather than at tables[^12].

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/pics/McDonaldsSelfServiceKiosks.png)

### Chili’s Grill & Bar

Offers a tablet-based ordering system for customers. The system allows customers to browse the menu, place orders, and pay directly from the tablet. This company has seen increased customer satisfaction and faster service times with this technology. Our system is also inspired by this concept of digital ordering[^13].

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/pics/ChilisTabletOrdering.png)

## Team Members Individual Tasks

### Rabia Nadeem

- Data Gathering
- Case Study
- Functional Requirements
- Business Requirements
- Non-Functional Requirements
- Physical Requirements
- Development Requirements

### Abaid Ullah

- Database Design
- Abstract
- Introduction
- Problem Statement
- Objectives
- Vision
- Use Cases
- User Stories
- Proposed Methodology System

### Abdul Rahman

- GUI Design
- Wireframes
- Use Cases
- Application Flow
- Story Boards
- Timeline/Gantt chart
- Resource Gathering
- Database Connectivity
- Application Development

## Timeline/Gantt chart

The project timeline is divided into several phases, including planning, design, development, testing, and documentation. Each phase has specific tasks and milestones to ensure the project progresses smoothly and meets deadlines.

![image](https://raw.githubusercontent.com/abdbbdii/Automated-Menu-Ordering-System/refs/heads/main/docs/images/other-diagrams/GanttChart.png)

## Data Gathering Approach

### First-hand Experience

- Restaurants will be visited to observe current systems.
- Managers and staff will be consulted to understand challenges.
- Notes will be taken on tools or software used for managing operations.

### Surveys and Interviews

- Surveys will be created for restaurant owners, staff, and customers to gather perspectives.
- Customer feedback surveys will be used to identify common preferences and complaints related to service, timing, and technology.

### Secondary Research

- Existing systems and case studies on restaurant management technologies will be analyzed.
- Software solutions, their features, and reviews will be examined to identify areas for improvement.

### Observation and Testing

- On-site tests will be conducted to assess system efficiency and document bottlenecks.
- Observations will be made during peak and off-peak hours to capture activity levels.

### Competitive Analysis

- Restaurants that have successfully implemented technology will be compared.
- Tools such as mobile apps and online reservation systems will be analyzed to inform solution design.

### User Feedback and Iterative Development

- Feedback loops will be established with staff and customers during prototyping.
- Ongoing feedback will guide solution design and functionality improvements.

## References

[^1]: [Dark mode vs. Light mode](https://learn.microsoft.com/en-us/windows/apps/desktop/modernize/ui/apply-windows-themes#dark-mode-vs-light-mode)
[^2]: [Navigation design basics for Windows apps](https://learn.microsoft.com/en-us/windows/apps/design/basics/navigation-basics)
[^3]: [Windows App SDK Documentation](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/)
[^4]: [Design basics for Windows apps](https://learn.microsoft.com/en-us/windows/apps/design/basics/)
[^5]: [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery)
[^6]: [Template Studio](https://github.com/microsoft/TemplateStudio)
[^7]: [Windows 11 signature experiences](https://learn.microsoft.com/en-us/windows/apps/design/signature-experiences/signature-experiences)
[^8]: [Responsive design techniques](https://learn.microsoft.com/en-us/windows/apps/design/layout/responsive-design)
[^9]: [WinUI performance optimization](https://learn.microsoft.com/en-us/windows/apps/performance/winui-perf)
[^10]: [Why Pay At The Table Tablet Is The Holy Grail?](https://www.ziosk.com/blogs/why-pay-at-the-table-tablet-is-the-holy-grail)
[^11]: [Why Ziosk?](https://www.ziosk.com/why-ziosk)
[^12]: [The Benefits of McDonald’s Self Ordering Kiosks](https://www.wavetec.com/blog/mcdonalds-leveraging-self-service-technologies/)
[^13]: [Chili’s reunites with Ziosk as its tabletop payment supplier](https://www.restaurantbusinessonline.com/technology/chilis-reunites-ziosk-its-tabletop-payment-supplier)