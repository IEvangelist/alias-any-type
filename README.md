# C# 12: Alias Any Type

This is a demo app that shows how to use the "alias any type" feature in C# 12.

## 🛸 UFO Sightings

The app is a simple console app, that randomly generates valid coordinates (latitude and longitude), then using said coordinates retrieves geo-code metadata related to the coordinates. The coordinates are represented in degrees-minutes-seconds format (including cardinality). And when the app is running, distances between the generated coordinates are calculated and reported as UFO sightings.

## 🚀 Running the app

From the .NET CLI, use the following command to run the app:

```
dotnet run --project .\src\Alias.AnyType.csproj
```

When the app is starts, it will output to the console messaging similar to the following:

![UFO Sightings: App start](./assets/app-start.png)

After reading the output, press any key to exit for the app to start watching for UFOs 👽.

![UFO Sightings: App running](./assets/app-run.png)

Press <kbd>Ctrl</kbd> + <kbd>C</kbd> to exit the app.
