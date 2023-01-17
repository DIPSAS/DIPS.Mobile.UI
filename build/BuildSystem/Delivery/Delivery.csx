#r "nuget:Newtonsoft.Json, 13.0.2"
using Newtonsoft.Json;

public static class Delivery{
    public static async Task<InternalDelivery> GetInternalDelivery(string deliveryFilePath){
        var json = await File.ReadAllTextAsync(deliveryFilePath);
        var appCenterDelivery = JsonConvert.DeserializeObject<AppCenterDelivery>(json);
        return appCenterDelivery.Internal;
    }

    public static async Task<CustomerDelivery> GetCustomerDelivery(string deliveryFilePath, string id) {
        var json = await File.ReadAllTextAsync(deliveryFilePath);
        var appCenterDelivery = JsonConvert.DeserializeObject<AppCenterDelivery>(json);
        return appCenterDelivery.Customers.FirstOrDefault(c => c.Id == id);
    }
}

public enum DeliveryType{
    Android,
    iOS   
}

public class AppCenterDestination{
    public AppCenterDestination(string appGroup, List<string> distributionGroups)
    {
        AppGroup = appGroup;
        DistributionGroups = distributionGroups;
    }
    public string AppGroup { get; }
    public List<string> DistributionGroups { get;}
}

public class AppCenterDelivery{
    public string Description { get; set; }
    public InternalDelivery Internal { get; set; }
    public List<CustomerDelivery> Customers { get; set; }
}

public class AndroidDelivery{
    public string AppGroup { get; set; }
    public string DevDistributionGroup { get; set; }
    public string RcDistributionGroup { get; set; }
}

public class iOSDelivery{
    public string AppGroup { get; set; }
    public string DevDistributionGroup { get; set; }
    public string RcDistributionGroup { get; set; }
}

public class InternalDelivery{
    public AndroidDelivery Android { get; set; }
    public iOSDelivery iOS { get; set; }
}


public class CustomerDelivery{
    public string Name { get; set; }
    public string Id { get; set; }
    public AndroidCustomerDelivery Android { get; set; }
    public iOSCustomerDelivery iOS { get; set; }
    
}

public class AndroidCustomerDelivery{
    public string AppGroup { get; set; }
    public string DistributionGroup { get; set; }
}

public class iOSCustomerDelivery{
    public string AppGroup { get; set; }
    public string DistributionGroup { get; set; }
}