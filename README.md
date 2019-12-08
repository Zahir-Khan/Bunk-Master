# Bunk-Master
This was a passion project of mine which was worked on in 2018 while I was in my Junior Year of CS degree.

### File Sturcture - Only files that I have coded are listed

    .
    ├── Bunk_Master.Android               # Files Realted to Android
    |   ├── CustomServiceBinder.cs
    |   ├── JobSchedulerHelper.cs
    |   ├── PeriodicService.cs
    |   └── ServiceConnection.cs
    ├── Bunk_Master.IOS                   # Files Realted to IOS
    └── Bunk_Master                       # Common bussiness logic for IOS/Android
        ├── Helpers
        |   └── ColorValueConverter.cs
        ├── IConverters
        |   ├── IClassNeededConverter.cs
        |   ├── IDateTextConverter.cs
        |   ├── IGetPercentConverter.cs
        |   ├── IGetPresentConverter.cs
        |   ├── IStrToSFColorConverter.cs
        |   └── IndexToColorConverter.cs
        ├── Interfaces
        |   └── ISQLiteService.cs
        ├── Models
        |   ├── BaseItem.cs
        |   ├── ClassModel.cs
        |   ├── DatesModel.cs
        |   ├── DayAbsentModel.cs
        |   ├── OverModel.cs
        |   ├── PercentModel.cs
        |   ├── SettingModel.cs
        |   ├── SumModel.cs
        |   └── TelemetModel.cs
        ├── ViewModels
        |   ├── BaseVM.cs
        |   ├── ClassVM.cs
        |   ├── Database.cs
        |   ├── DatesVM.cs
        |   ├── GraphGen.cs
        |   └── ViewModel.cs
        ├── Views
        |   ├── AddFormview.cs
        |   ├── BindableSKCanvasView.cs
        |   ├── ColorView.cs
        |   ├── GraphInfo.cs
        |   ├── HomeTabPage.cs
        |   ├── OverallPage.cs
        |   ├── PopupViewD.xaml
        |   ├── PopupViewD.xaml.cs
        |   ├── ScheSwitch.cs
        ├── AnalysisView.cs
        ├── App.xaml
        ├── App.xaml.cs
        ├── DiagnosticsVM.cs
        ├── GraphInfo.cs
        └── Scheduler.cs
