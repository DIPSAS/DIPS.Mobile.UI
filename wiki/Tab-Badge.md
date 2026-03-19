Badges are small visual indicators often used in user interfaces to convey additional information, such as notifications, counts, or status updates. They are commonly displayed on tabs, buttons, or icons to draw attention to specific elements or provide contextual information at a glance.

# TabBadgeService
We provide an API called `TabBadgeService` that can be used to set badges on tabs in your application.  

> Our `Shell` component must be used to utilize the API.

The `TabBadgeService` is a utility designed to enhance the user interface by allowing developers to add badges to tabs in their applications. These badges can be used to display notifications, counts, or other indicators directly on a tab, improving user experience and providing contextual information at a glance.  

With `TabBadgeService`, you can:  
- Set a badge on a specific tab.  
- Increment/deincrement the badge count to reflect dynamic data.  
- Adjust the badge color to match your application's theme

This service simplifies the process of managing tab badges, making it easy to integrate and maintain.

## Notes
* Setting the badge's count to 0 will hide it.
* If the count is above 99, it will be displayed as `99+`.

## Usage

Setting second badge count to 1:
```cs
TabBadgeService.SetCount(tabIndex: 1, count: 1);
```

Setting the second badge's color to blue:
```cs
TabBadgeService.SetColor(tabIndex: 1, Colors.Blue);
```