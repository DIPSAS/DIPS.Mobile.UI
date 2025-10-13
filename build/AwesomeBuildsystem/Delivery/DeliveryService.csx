#r "nuget:Newtonsoft.Json, 13.0.3"
using Newtonsoft.Json;

public static class DeliveryService{
    public static async Task<PublicDelivery> GetPublicDelivery(string deliveryFilePath){
        var json = await File.ReadAllTextAsync(deliveryFilePath);
        var storeDelivery = JsonConvert.DeserializeObject<Deliveries>(json);
        return storeDelivery.Public;
    }

    public static async Task<Delivery> GetCustomerDelivery(string deliveryFilePath, string id) {
        var json = await File.ReadAllTextAsync(deliveryFilePath);
        var storeDelivery = JsonConvert.DeserializeObject<Deliveries>(json);
        return storeDelivery.Customers.FirstOrDefault(c => c.Id == id);
    }

    public static async Task<Delivery> GetMdmDelivery(string deliveryFilePath)
    {
        var json = await File.ReadAllTextAsync(deliveryFilePath);
        var storeDelivery = JsonConvert.DeserializeObject<Deliveries>(json);
        return storeDelivery.Mdm;
    }

    public static async Task<bool> IsDeliveryFileValid(string deliveryFilePath, string id)
    {
        var json = await File.ReadAllTextAsync(deliveryFilePath);
        var delivery = JsonConvert.DeserializeObject<Deliveries>(json);
        return delivery.Customers.FirstOrDefault(c => c.Id == id) != null || delivery.Mdm.Id == id;
    }
}

public enum DeliveryType{
    Android,
    iOS   
}

public class Deliveries{
    public string Description { get; set; }
    public PublicDelivery Public { get; set; }
    public List<Delivery> Customers { get; set; }
    public Delivery Mdm { get; set; }
}

public class AndroidPublicDelivery{
    /// <summary>
    /// The Google Play dev track
    /// </summary>
    public string DevTrack { get; set; }
}

public class iOSPublicDelivery{
    public string AppleId { get; set; }
    public string DevTestFlight { get; set; }
}

public class PublicDelivery{
    public AndroidPublicDelivery Android { get; set; }
    public iOSPublicDelivery iOS { get; set; }
}


public class Delivery{
    public string Name { get; set; }
    public string Id { get; set; }
    public AndroidDelivery Android { get; set; }
    public iOSDelivery iOS { get; set; }
    
}

public class AndroidDelivery{

    /// <summary>
    /// The Google Play Console Track for the application.
    /// </summary>
    public string Track { get; set; }
}

public class iOSDelivery{
    /// <summary>
    /// The ID of the App Store Connect project. This is found in the App Information part of App Store Connect.
    /// https://appstoreconnect.apple.com/apps/{appleId}/distribution/ios/version/deliverable
    /// </summary>
    public string AppleId { get; set; }
}