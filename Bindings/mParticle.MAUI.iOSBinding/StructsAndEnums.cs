using System;
using ObjCRuntime;

namespace mParticle.MAUI.iOSBinding {

    [Native]
    public enum MPEnvironment : long
    {
        AutoDetect = 0,
        Development,
        Production
    }

    [Native]
    public enum MPEventType : long
    {
        Navigation = 1,
        Location = 2,
        Search = 3,
        Transaction = 4,
        UserContent = 5,
        UserPreference = 6,
        Social = 7,
        Other = 8,
        AddToCart = 10,
        RemoveFromCart = 11,
        Checkout = 12,
        CheckoutOption = 13,
        Click = 14,
        ViewDetail = 15,
        Purchase = 16,
        Refund = 17,
        PromotionView = 18,
        PromotionClick = 19,
        AddToWishlist = 20,
        RemoveFromWishlist = 21,
        Impression = 22
    }

    [Native]
    public enum MPInstallationType : long
    {
        Autodetect = 0,
        KnownInstall,
        KnownUpgrade,
        KnownSameVersion
    }

    [Native]
    public enum MPLocationAuthorizationRequest : long
    {
        Always = 0,
        WhenInUse
    }

    [Native]
    public enum MPProductEvent : long
    {
        View = 0,
        AddedToWishList,
        RemovedFromWishList,
        AddedToCart,
        RemovedFromCart
    }

    [Native]
    public enum MPSurveyProvider : long
    {
        MPSurveyProviderForesee = 64
    }

    [Native]
    public enum MPATTAuthorizationStatus : long
    {
        MPATTAuthorizationStatusNotDetermined = 0,
        MPATTAuthorizationStatusRestricted = 1,
        MPATTAuthorizationStatusDenied = 2,
        MPATTAuthorizationStatusAuthorized = 3
    }

    [Native]
    public enum MPUserIdentity : long
    {
        MPUserIdentityOther = 0,
        MPUserIdentityCustomerId = 1,
        MPUserIdentityFacebook = 2,
        MPUserIdentityTwitter = 3,
        MPUserIdentityGoogle = 4,
        MPUserIdentityMicrosoft = 5,
        MPUserIdentityYahoo = 6,
        MPUserIdentityEmail = 7,
        MPUserIdentityAlias = 8,
        MPUserIdentityFacebookCustomAudienceId = 9,
        MPUserIdentityOther2 = 10,
        MPUserIdentityOther3 = 11,
        MPUserIdentityOther4 = 12,
        MPUserIdentityOther5 = 13,
        MPUserIdentityOther6 = 14,
        MPUserIdentityOther7 = 15,
        MPUserIdentityOther8 = 16,
        MPUserIdentityOther9 = 17,
        MPUserIdentityOther10 = 18,
        MPUserIdentityMobileNumber = 19,
        MPUserIdentityPhoneNumber2 = 20,
        MPUserIdentityPhoneNumber3 = 21,
        MPUserIdentityIOSAdvertiserId = 22,
        MPUserIdentityIOSVendorId = 23,
        MPUserIdentityPushToken = 24,
        MPUserIdentityDeviceApplicationStamp = 25
    }

    [Native]
    public enum MPKitInstance : long
    {
        UrbanAirship = 25,
        Appboy = 28,
        Tune = 32,
        Kochava = 37,
        ComScore = 39,
        Kahuna = 56,
        Nielsen = 63,
        Foresee = 64,
        Adjust = 68,
        BranchMetrics = 80,
        Flurry = 83,
        Localytics = 84,
        Apteligent = 86,
        Crittercism = 86,
        Wootric = 90,
        AppsFlyer = 92,
        Apptentive = 97,
        Leanplum = 98,
        Primer = 100,
        Apptimize = 105,
        RevealMobile = 112,
        Radar = 117,
        Skyhook = 121,
        Iterable = 1003,
        Button = 1022
    }

    [Native]
    public enum MPILogLevel : long
    {
        None = 0,
        Error,
        Warning,
        Debug,
        Verbose
    }

    [Native]
    public enum MPMessageType : long
    {
        Unknown = 0,
        SessionStart = 1,
        SessionEnd = 2,
        ScreenView = 3,
        Event = 4,
        CrashReport = 5,
        OptOut = 6,
        FirstRun = 7,
        PreAttribution = 8,
        PushRegistration = 9,
        AppStateTransition = 10,
        PushNotification = 11,
        NetworkPerformance = 12,
        Breadcrumb = 13,
        Profile = 14,
        PushNotificatiolongeraction = 15,
        CommerceEvent = 16,
        UserAttributeChange = 17,
        UserIdentityChange = 18
    }

    [Native]
    public enum MPCommerceEventAction : long
    {
        AddToCart = 0,
        RemoveFromCart,
        AddToWishList,
        RemoveFromWishlist,
        Checkout,
        CheckoutOptions,
        Click,
        ViewDetail,
        Purchase,
        Refund
    }

    [Native]
    public enum MPCommerceInstruction : long
    {
        Event = 0,
        Transaction
    }

    [Native]
    public enum MPCommerceEventKind : long
    {
        Unknown = 0,
        Product = 1,
        Promotion,
        Impression
    }

    [Native]
    public enum MPKitReturnCode : long
    {
        Success = 0,
        Fail,
        CannotExecute,
        Unavailable,
        IncorrectProductVersion,
        RequirementsNotMet
    }

    [Native]
    public enum MPPromotionAction : long
    {
        Click = 0,
        View
    }

    //This was added manually.
    [Native]
    public enum MPSegmentMembershipAction : long
    {
        MPSegmentMembershipActionAdd = 1,
        MPSegmentMembershipActionDrop
    }

}


